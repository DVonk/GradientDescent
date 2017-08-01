using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueTrigger5 : MonoBehaviour
{
    public AudioSource audioSource;
    public Color panelColor;
    public GameObject faderPanel;
    public GameObject shootingStarSpawner;
    public Text dialogueText;
    public Image dialoguePanel;

    public string startText = "";
    public bool  started = false;

    public bool jumpPressed = false;
    bool playingCouroutine = false;

    public List<string> levelStartStrings = new List<string>();



    private void Start()
    {


        StartCoroutine("ShowStartText");

    }



    private void Update()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) && !playingCouroutine && started)
        {

            jumpPressed = true;
        }

        if (!started)
            jumpPressed = false;
    }

    IEnumerator ShowStartText()
    {
        yield return new WaitForSeconds(10f);

        started = true;
        while (dialoguePanel.color.a < 0.4f)
        {
            dialoguePanel.color += new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }

        dialogueText.text = startText;


        while (true)
        {


            playingCouroutine = false;
            //Dialogue
            if (levelStartStrings.Count > 0)
            {


                while (dialogueText.color.a < 1)
                {
                    dialogueText.color += new Color(0, 0, 0, Time.deltaTime);
                    yield return null;
                }


                if (jumpPressed)
                {
                    playingCouroutine = true;

                    SFXManager.instance.SetVolume(0.4f);
                    SFXManager.instance.PlaySFX("Accept");


                    while (dialogueText.color.a > 0)
                    {
                        dialogueText.color -= new Color(0, 0, 0, Time.deltaTime);
                        yield return null;
                    }


                    dialogueText.text = levelStartStrings[0];
                    levelStartStrings.RemoveAt(0);

                    jumpPressed = false;
                }
            }

            else
            {
                while (dialoguePanel.color.a > 0f)
                {
                    dialoguePanel.color -= new Color(0, 0, 0, Time.deltaTime);
                    yield return null;
                }

                dialogueText.text = "";

                StartCoroutine(FadeOutBGM());
                yield return StartCoroutine(FadeOutPanel());

                SceneManager.LoadScene("Results");


                break;
            }

            yield return null;

        }
    }


    IEnumerator FadeOutPanel()
    {
        Image img = faderPanel.GetComponent<Image>();
        img.color = panelColor;

        while (img.color.a < 1)
        {
            img.color += new Color(0, 0, 0, Time.deltaTime / 10);
            yield return null;
        }


    }

    public IEnumerator FadeOutBGM()
    {
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= Time.deltaTime / 10;
            yield return null;
        }

    }

}

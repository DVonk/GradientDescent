using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Manager : MonoBehaviour
{
    public bool skipDialogue = false;
    public GameObject shootingStarSpawner;
    public GameObject liveCounter;
    public Text dialogueText;
    PlayerController player;
    public Text scoreText;
    public Button yesButton;
    public Button noButton;

    public Image faderPanel;

    public bool jumpPressed = false;
    bool playingCouroutine = false;

    public List<string> levelStartStrings = new List<string>();
    public List<string> yesDialogue = new List<string>();
    public List<string> noDialogue = new List<string>();

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (!skipDialogue)
        {
            player.frozen = true;
            StartCoroutine("ShowStartText");

            faderPanel.gameObject.SetActive(true);
        }
        else
        {
            BGMManager.instance.PlayBGM("BGM1");
            scoreText.text = "0 / 5";
            scoreText.gameObject.SetActive(true);
            shootingStarSpawner.SetActive(true);
            liveCounter.SetActive(true);
        }

    }

    private void Update()
    {

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1") && !playingCouroutine)
        {
            jumpPressed = true;
        }
    }

    IEnumerator ShowStartText()
    {
        while (true)
        {
            playingCouroutine = false;
            //Dialogue
            if (levelStartStrings.Count > 0)
            {
                if (levelStartStrings[0][0].Equals('_'))
                {
                    dialogueText.text = levelStartStrings[0].Replace('_', ' ');
                    while (dialogueText.color.a < 1)
                    {
                        dialogueText.color += new Color(0, 0, 0, Time.deltaTime);
                        yield return null;
                    }

                    if(!yesButton.gameObject.activeSelf)
                    {
                        yesButton.gameObject.SetActive(true);
                        noButton.gameObject.SetActive(true);

                        yield return new WaitForSeconds(0.5f);
                        yesButton.interactable = true;
                        noButton.interactable = true;
                    }

                    if (GameManager.instance.chosenAnswer != 0)
                    {
                        if (GameManager.instance.chosenAnswer == 1)
                        {
                            levelStartStrings.AddRange(yesDialogue);
                            GameManager.instance.chosenAnswers[0] = 1;
                        }
                        else if (GameManager.instance.chosenAnswer == 2)
                        {
                            levelStartStrings.AddRange(noDialogue);                        
                            GameManager.instance.chosenAnswers[0] = 2;                     
                        }

                        SFXManager.instance.SetVolume(0.4f);
                        SFXManager.instance.PlaySFX("Accept");
                        levelStartStrings.RemoveAt(0);
                        GameManager.instance.chosenAnswer = 0;
                        jumpPressed = false;
                        yesButton.gameObject.SetActive(false);
                        noButton.gameObject.SetActive(false);
                        yesButton.interactable = false;
                        noButton.interactable = false;

                        while (dialogueText.color.a > 0)
                        {
                            dialogueText.color -= new Color(0, 0, 0, Time.deltaTime);
                            yield return null;
                        }



                        dialogueText.text = levelStartStrings[0];
                        levelStartStrings.RemoveAt(0);

      
                    }
                }
                else
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
            }
            else
            {
         
                dialogueText.text = "";
                yield return StartCoroutine("FadeOutPanel");
                player.frozen = false;
 
                BGMManager.instance.PlayBGM("BGM1");
        
                scoreText.gameObject.SetActive(true);
                GameManager.instance.roomActive = true;

                break;
            }

            yield return null;
        }
    }

    IEnumerator FadeOutPanel()
    {
        while (faderPanel.color.a > 0)
        {
            faderPanel.color -= new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }

        faderPanel.gameObject.SetActive(false);
        shootingStarSpawner.SetActive(true);
        liveCounter.SetActive(true);
    }
}

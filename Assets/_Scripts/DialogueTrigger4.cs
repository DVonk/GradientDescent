using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueTrigger4 : MonoBehaviour
{
    public GameObject particleSystemToDeactivate;

    public Color panelColor;
    public BoxCollider2D boxToDespawn;
    public GameObject faderPanel;
    public bool skipDialogue = false;
    public GameObject shootingStarSpawner;
    public Text dialogueText;
    public Image dialoguePanel;
    PlayerController player;
    public Text scoreText;
    public Button yesButton;
    public Button noButton;
    public string startText = "";
    bool started = false;
    public Color questionColor;
    bool questionColorSet = false;

    public bool jumpPressed = false;
    bool playingCouroutine = false;

    List<string> levelStartStrings = new List<string>();
    public List<string> beforeQuestionStrings = new List<string>();
    public List<string> yesDialogue = new List<string>();
    public List<string> noDialogue = new List<string>();
    public List<string> noYesDialogue = new List<string>();

    public int choice = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            particleSystemToDeactivate.SetActive(false);

            player = collision.GetComponent<PlayerController>();

            if (!skipDialogue)
            {
                levelStartStrings.Add("What has kept you so long? Oh, I see...");

                started = true;
                if (GameManager.instance.chosenAnswers[0] == 1)
                    levelStartStrings.Add("You chose to help someone without knowing who it was.");
                else
                    levelStartStrings.Add("You said no to someone before knowing who it was.");

                if (GameManager.instance.chosenAnswers[1] == 1)
                    levelStartStrings.Add("You gave a stranger one of your orbs.");
                else
                    levelStartStrings.Add("You didn't give the stranger a single orb.");

                if (GameManager.instance.chosenAnswers[2] == 1)
                    levelStartStrings.Add("Against her will you chose to rescue the sister.");
                else
                    levelStartStrings.Add("You let the sister die, as she asked you to.");

                if (GameManager.instance.chosenAnswers[3] == 1)
                    levelStartStrings.Add("And you helped the killer survive.");
                else
                    levelStartStrings.Add("And you chose to leave the killer to his fate.");


                levelStartStrings.AddRange(beforeQuestionStrings);

                player.frozen = true;
                StartCoroutine("ShowStartText");
            }
            else
            {
                BGMManager.instance.PlayBGM("BGM1");
                scoreText.gameObject.SetActive(true);
                shootingStarSpawner.SetActive(true);

            }
        }
    }


    private void Update()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) && !playingCouroutine && started)
        {
            jumpPressed = true;
        }
    }

    IEnumerator ShowStartText()
    {

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
                if (levelStartStrings[0][0].Equals('_'))
                {
                    dialogueText.text = levelStartStrings[0].Replace('_', ' ');

                    if (!questionColorSet)
                    {
                        Color currentColor = questionColor;
                        currentColor.a = 0;
                        dialogueText.color = currentColor;
                        questionColorSet = true;
                    }

                    while (dialogueText.color.a < 1)
                    {
                        dialogueText.color += new Color(0, 0, 0, Time.deltaTime);
                        yield return null;
                    }


                    if (!yesButton.gameObject.activeSelf)
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
                            GameManager.instance.chosenAnswers[4] = 1;
                        }
                        else if (GameManager.instance.chosenAnswer == 2)
                        {
                            if (GameManager.instance.chosenAnswers[0] == 2 && GameManager.instance.chosenAnswers[1] == 2 && GameManager.instance.chosenAnswers[2] == 2
                                && GameManager.instance.chosenAnswers[3] == 2)
                                levelStartStrings.AddRange(noDialogue);
                            else
                                levelStartStrings.AddRange(noYesDialogue);

                            GameManager.instance.chosenAnswers[4] = 2;
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


                        Color theColor = Color.white;
                        theColor.a = 0;
                        dialogueText.color = theColor;
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
                while (dialoguePanel.color.a > 0f)
                {
                    dialoguePanel.color -= new Color(0, 0, 0, Time.deltaTime);
                    yield return null;
                }

                dialogueText.text = "";
                player.frozen = false;

                scoreText.gameObject.SetActive(true);
                GameManager.instance.roomActive = true;
                GetComponent<BoxCollider2D>().enabled = false;

                if (GameManager.instance.chosenAnswers[4] == 1)
                {
                    GameManager.instance.lives = 0;
                    GameManager.instance.RefreshLives();

                    FindObjectOfType<PlayerController>().GetComponent<Animator>().SetTrigger("dead");
                    StartCoroutine("IncreaseLight");
                }
                else if (GameManager.instance.chosenAnswers[1] == 2 && GameManager.instance.chosenAnswers[2] == 2
                         && GameManager.instance.chosenAnswers[3] == 2)
                {
                    boxToDespawn.enabled = false;
                }
                else
                {
                    GameManager.instance.lives = 0;
                    GameManager.instance.RefreshLives();

                    FindObjectOfType<PlayerController>().GetComponent<Animator>().SetTrigger("dead");
                    StartCoroutine("IncreaseLight");
                }


                GetComponent<BoxCollider2D>().enabled = false;


                break;
            }

            yield return null;
        }
    }

    IEnumerator IncreaseLight()
    {
        Light light = transform.parent.GetChild(1).gameObject.GetComponent<Light>();

        SFXManager.instance.PlaySFX("LightOn");
        yield return new WaitForSeconds(1f);
        SFXManager.instance.PlaySFX("FinalLightOn");

        while (light.intensity <= 124f)
        {
            light.range += Time.deltaTime * 6f;
            light.intensity += Time.deltaTime * 12f;
            yield return null;
        }

        faderPanel.gameObject.SetActive(true);

        yield return StartCoroutine(FadeOutPanel());

        while (FindObjectOfType<SFXPlayer>() != null)
        {
            yield return null;
        }

        if (GameManager.instance.chosenAnswers[1] == 1 && GameManager.instance.chosenAnswers[2] == 1
            && GameManager.instance.chosenAnswers[3] == 1)
            SceneManager.LoadScene("Ending");
        else
            SceneManager.LoadScene("EndingNeutral");
    }


    IEnumerator FadeOutPanel()
    {
        Image img = faderPanel.GetComponent<Image>();
        img.color = panelColor;

        while (img.color.a < 1)
        {
            img.color += new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }


    }

}

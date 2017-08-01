using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger3 : MonoBehaviour
{
    public GameObject particleSystemToActivate;
    public GameObject particleSystemToDeactivate;
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
    public List<string> levelStartStringsYes = new List<string>();
    public List<string> levelStartStringsNo = new List<string>();
    public List<string> yesDialogue = new List<string>();
    public List<string> noDialogue = new List<string>();

    public int choice = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            particleSystemToDeactivate.SetActive(false);
            Color col = GameManager.instance.scoreColorNo;
            col.a = 0f;
            scoreText.color = col;
            player = collision.GetComponent<PlayerController>();

            if (!skipDialogue)
            {
                started = true;
                if (GameManager.instance.chosenAnswers[choice] == 1)
                    levelStartStrings.AddRange(levelStartStringsYes);
                else
                    levelStartStrings.AddRange(levelStartStringsNo);
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
                        dialogueText.color +=  new Color(0, 0, 0, Time.deltaTime);
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
                            GameManager.instance.chosenAnswers[3] = 1;
                        }
                        else if (GameManager.instance.chosenAnswer == 2)
                        {
                            levelStartStrings.AddRange(noDialogue);
                            GameManager.instance.chosenAnswers[3] = 2;
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

                particleSystemToActivate.SetActive(true);

                scoreText.gameObject.SetActive(true);
                GameManager.instance.roomActive = true;

                if (GameManager.instance.chosenAnswers[3] == 1)
                {
                    GameManager.instance.lives -= 1;
                    GameManager.instance.RefreshLives();
                    SFXManager.instance.PlaySFX("LightOn");
                    transform.parent.GetComponent<Animator>().SetTrigger("npcLives");
                    transform.parent.GetChild(1).gameObject.SetActive(true);
                }

  
                GetComponent<BoxCollider2D>().enabled = false;


                break;
            }

            yield return null;
        }
    }


}

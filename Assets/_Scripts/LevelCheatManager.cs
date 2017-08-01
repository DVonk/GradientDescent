using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCheatManager : MonoBehaviour
{
    public GameObject chooseText;
    public GameObject liveCounter;
    PlayerController player;
    public Image faderPanel;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        faderPanel.gameObject.SetActive(true);
        player.frozen = true;
        player.falling = true;

  

    }

    private void Update()
    {

    }



    IEnumerator FadeOutPanel()
    {
        while (faderPanel.color.a > 0)
        {
            faderPanel.color -= new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }

        faderPanel.gameObject.SetActive(false);
        liveCounter.SetActive(true);

        player.frozen = false;
    }

    public void SetAscension()
    {
        chooseText.SetActive(false);
        GameManager.instance.lives = 5;
        GameManager.instance.RefreshLives();

        GameManager.instance.chosenAnswers[0] = 1;
        GameManager.instance.chosenAnswers[1] = 1;
        GameManager.instance.chosenAnswers[2] = 1;
        GameManager.instance.chosenAnswers[3] = 1;

        StartCoroutine(FadeOutPanel());
    }

    public void SetYouWin()
    {
        chooseText.SetActive(false);
        GameManager.instance.lives = 10;
        GameManager.instance.RefreshLives();
        GameManager.instance.chosenAnswers[0] = 2;
        GameManager.instance.chosenAnswers[1] = 2;
        GameManager.instance.chosenAnswers[2] = 2;
        GameManager.instance.chosenAnswers[3] = 2;

        StartCoroutine(FadeOutPanel());
    }

    public void SetDeath()
    {
        chooseText.SetActive(false);
        GameManager.instance.lives = 5;
        GameManager.instance.RefreshLives();
        GameManager.instance.chosenAnswers[0] = 1;
        GameManager.instance.chosenAnswers[1] = 2;
        GameManager.instance.chosenAnswers[2] = 1;
        GameManager.instance.chosenAnswers[3] = 2;

        StartCoroutine(FadeOutPanel());

    }
}

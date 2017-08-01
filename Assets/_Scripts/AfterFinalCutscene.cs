using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AfterFinalCutscene : MonoBehaviour
{
    public Button thanks;
    public GameObject faderPanel;
    public Color faderPanelColor;

    public Text congratsText;
    public Text congratsText2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        congratsText.gameObject.SetActive(true);

        faderPanel.SetActive(true);
        

        StartCoroutine("FadeOutPanel");
    }

    IEnumerator Congrats()
    {   
        while (congratsText.color.a < 1)
        {
            congratsText.color += new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(5.5f);

        while (congratsText2.color.a < 1)
        {
            congratsText2.color += new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(7f);
        thanks.gameObject.SetActive(true);
    }

    IEnumerator FadeOutPanel()
    {
        Image img = faderPanel.GetComponent<Image>();
        img.color = faderPanelColor;

        while (img.color.a < 1)
        {
            img.color += new Color(0, 0, 0, Time.deltaTime/3);
            yield return null;
        }

        StartCoroutine("Congrats");

        BGMManager.instance.PlayBGM("Troll");
        BGMManager.instance.StopLoop();

    }


    public void ThanksButton()
    {
        SFXManager.instance.PlaySFX("Accept");
        StartCoroutine("Thanks");
        thanks.gameObject.SetActive(false);

    }

    IEnumerator Thanks()
    {
        StartCoroutine(BGMManager.instance.FadeOutCour(3f));

        while (congratsText.color.a >= 0)
        {
            congratsText.color -= new Color(0, 0, 0, Time.deltaTime/3);
            congratsText2.color -= new Color(0, 0, 0, Time.deltaTime/3);
            yield return null;
        }

        SceneManager.LoadScene("Results");
    }
}

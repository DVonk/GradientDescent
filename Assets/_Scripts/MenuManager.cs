using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject faderPanel;
    public AudioSource audioSource;

    private void Start()
    {

        faderPanel.SetActive(true);
        StartCoroutine("FadeInPanel");
    }

    public void FadeToGame()
    {
        SFXManager.instance.SetVolume(0.3f);
        SFXManager.instance.PlaySFX("Accept");
        StartCoroutine("FadeOutPanel");
        StartCoroutine("FadeOutBGM");
    }


    IEnumerator FadeInPanel()
    {
 
        Image img = faderPanel.GetComponent<Image>();

        while (img.color.a > 0)
        {
            img.color -= new Color(0, 0, 0, Time.deltaTime/3);
            yield return null;
        }

        faderPanel.SetActive(false);

    }

    IEnumerator FadeOutPanel()
    {
        faderPanel.SetActive(true);
        Image img = faderPanel.GetComponent<Image>();

        while (img.color.a < 1)
        {
            img.color += new Color(0, 0, 0, Time.deltaTime / 3);
            yield return null;
        }

        SceneManager.LoadScene("Game");

    }


    IEnumerator FadeOutBGM()
    {
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= Time.deltaTime / 3f;
            yield return null;
        }

    }
}

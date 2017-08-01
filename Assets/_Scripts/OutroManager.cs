using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutroManager : MonoBehaviour
{
    public GameObject faderPanel;
    public AudioSource audioSource;

    private void Start()
    {
        faderPanel.SetActive(true);

        StartCoroutine("FadeInBGM");
        StartCoroutine("FadeInPanel");
    }


    IEnumerator FadeInPanel()
    {
        yield return new WaitForSeconds(3f);
        Image img = faderPanel.GetComponent<Image>();

        while (img.color.a > 0)
        {
            img.color -= new Color(0, 0, 0, Time.deltaTime/3);
            yield return null;
        }

    }

    IEnumerator FadeInBGM()
    {
        yield return new WaitForSeconds(5f);


        audioSource.Play();

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

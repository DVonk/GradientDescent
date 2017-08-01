using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public Sprite playerSprite;
    public GameObject player;
    public GameObject faderPanel;
    public AudioSource audioSource;

    private void Start()
    {
        faderPanel.SetActive(true);
        StartCoroutine("FadeInPanel");
    }


    IEnumerator FadeInPanel()
    {
        yield return new WaitForSeconds(2f);
        Image img = faderPanel.GetComponent<Image>();

        while (img.color.a > 0)
        {
            img.color -= new Color(0, 0, 0, Time.deltaTime/15);
            yield return null;
        }

    }
    public void PlayerLives()
    {
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerSprite;
        player.GetComponent<Animator>().SetTrigger("npcLives");
    }

    public void PlayLightSFX()
    {
        SFXManager.instance.SetVolume(0.1f);
        SFXManager.instance.PlaySFX("LightOn");
    }

    public void PlayBGM()
    {
        BGMManager.instance.PlayBGM("Ending");
    }

    public void StopSpawn()
    {
        FindObjectOfType<ShootingStarSpawner>().enabled = false;
    }

    public void ChangeScene()
    {
        StartCoroutine("FadeOutBGM");
        StartCoroutine("FadeOutPanel");

    }

    public IEnumerator FadeOutBGM()
    {
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= Time.deltaTime / 10;
            yield return null;
        }

        SceneManager.LoadScene("Results");
    }

    public IEnumerator FadeOutPanel()
    {
        Image img = faderPanel.GetComponent<Image>();

        while (img.color.a < 1)
        {
            img.color += new Color(0, 0, 0, Time.deltaTime / 10);
            yield return null;
        }
    }
}

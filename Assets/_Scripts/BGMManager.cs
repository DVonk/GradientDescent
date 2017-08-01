using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public BGMManager instance = null;

    AudioSource audioSource;

    public List<AudioClip> clips;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM(string name)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == name)
            {
                audioSource.clip = clip;
                audioSource.Play();
                break;
            }
        }
    }

    public void Stop()
    {

        audioSource.Stop();
    }

    public void StopLoop()
    {
        audioSource.loop = false;
    }

    public void FadeOut(float time)
    {
        StartCoroutine("FadeOutCour", time);
    }

    public void FadeIn(float time)
    {
        StartCoroutine("FadeInCour", time);
    }

    public IEnumerator FadeOutCour(float time)
    {
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= Time.deltaTime / time;
            yield return null;
        }

    }

    IEnumerator FadeInCour(float time)
    {
        while (audioSource.volume < 0.8f)
        {
            audioSource.volume += Time.deltaTime / time;
            yield return null;
        }

    }

    public void CrossFade(string bgm)
    {
        StartCoroutine("CrossCour", bgm);

    }

    IEnumerator CrossCour(string bgm)
    {

        yield return StartCoroutine("FadeOutCour", 3f);
        PlayBGM(bgm);
        audioSource.loop = false;
        StartCoroutine("FadeInCour", 5f);
    }
}

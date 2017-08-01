using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    static public SFXManager instance = null;

    public GameObject playerPrefab;
    float volume = 0.4f;

    public List<AudioClip> clips;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

    }

    public void PlaySFX(string name)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == name)
            {
                GameObject thePlayer = Instantiate(playerPrefab);
                AudioSource source = thePlayer.GetComponent<AudioSource>();
                source.clip = clip;
                source.volume = volume;
                source.Play();
                DontDestroyOnLoad(thePlayer);
                Destroy(thePlayer, clip.length);
                break;
            }
        }
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
    }
}

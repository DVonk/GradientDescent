using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
    }

    private void OnEnable()
    {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        while (text.color.a < 1)
        {
            text.color += new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }

    }

    IEnumerator FadeOut()
    {
     
        while (text.color.a > 0)
        {
            text.color -= new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }

    }
}

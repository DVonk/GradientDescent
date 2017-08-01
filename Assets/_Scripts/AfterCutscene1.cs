using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterCutscene1 : MonoBehaviour
{
    public BoxCollider2D col;
    public GameObject shootingStarSpawner;
    public string bgmToPlay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            col.enabled = true;
            BGMManager.instance.FadeIn(6f);
            BGMManager.instance.PlayBGM(bgmToPlay);
            GameManager.instance.ToggleScoreVisible();
            collision.gameObject.GetComponent<PlayerController>().falling = false;
            shootingStarSpawner.SetActive(true);
            Destroy(gameObject);
        }

    }
}

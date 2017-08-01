using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStarter : MonoBehaviour
{
    public BoxCollider2D boundary;
    public bool turnOffLight;
    public string bgmToPlay = "";


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (bgmToPlay.Length == 0)
                BGMManager.instance.FadeOut(10f);
            else
                BGMManager.instance.CrossFade(bgmToPlay);

            collision.GetComponent<PlayerController>().falling = true;
            GameManager.instance.ToggleScoreVisible();

            foreach (ShootingStarSpawner obj in FindObjectsOfType<ShootingStarSpawner>())
            {
                obj.gameObject.SetActive(false);
            }

            if (boundary != null)
                boundary.enabled = true;

            if (turnOffLight)
                collision.transform.GetChild(1).gameObject.SetActive(false);



            Destroy(gameObject);
        }
    }
}

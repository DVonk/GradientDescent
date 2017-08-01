using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public bool noScoreTextRoom = false;
    public float yOffset;
    public bool followerRoom = false;
    public bool onlyFollowY = false;

    public bool roomDone = false;
    public GameObject gate;

    public int requiredScore = 1;
    public int currentScore = 0;

    public bool activateSpawner = false;
    public GameObject spawnerToActivate;

    public float spawnerFrequency = 0f;
    public GameObject spawner;
    public bool destroyAfterEnter = false;


    private void Start()
    {
        yOffset = FindObjectOfType<CameraFollow>().yOffset;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (activateSpawner)
                if (!spawnerToActivate.activeSelf)
                    spawnerToActivate.SetActive(true);

            if (spawnerFrequency != 0f)
                spawner.GetComponent<ShootingStarSpawner>().frequency = spawnerFrequency;

            if (!followerRoom)
            {
                Camera.main.GetComponent<CameraFollow>().following = false;
                Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + yOffset, -10);
            }
            else
            {
                Camera.main.GetComponent<CameraFollow>().following = true;
                if (onlyFollowY)
                {
                    Camera.main.transform.position = FindObjectOfType<PlayerController>().transform.position;
                    Camera.main.GetComponent<CameraFollow>().onlyFollowY = true;
                }


            }

            GameManager.instance.SetRoom(this);
        }

        if (destroyAfterEnter)
        {
            Destroy(gameObject);
        }
    }

    public void OpenGate()
    {
        if (gate != null)
        {
            SFXManager.instance.SetVolume(0.8f);
            SFXManager.instance.PlaySFX("GateDown");
            gate.GetComponent<Animator>().SetTrigger("Destroy");
        }

    }
}

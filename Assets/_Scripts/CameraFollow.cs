using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float yOffset;
    public bool following = false;
    public bool onlyFollowY = false;

    GameObject player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        Vector3 newPos = new Vector3(transform.position.x, player.transform.position.y + yOffset, -10);

        transform.position = newPos;
    }

    private void LateUpdate()
    {
        if (following)
        {
            if (!onlyFollowY)
                transform.position = player.transform.position + Vector3.back * 10;
            else
                transform.position = new Vector3(transform.position.x, player.transform.position.y, -10);
        }
    }
}

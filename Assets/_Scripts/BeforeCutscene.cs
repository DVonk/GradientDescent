using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeCutscene : MonoBehaviour
{
    public BoxCollider2D box;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            box.enabled = true;
        }
    }
}

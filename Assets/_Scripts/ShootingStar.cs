using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : MonoBehaviour
{
    public bool leftToRight = false;
    public float speed;
    bool goingUp = true;
    int upTicks = 0;
    public int maxTicks = 2000;
    public GameObject pickupBurst;
    public bool hardTurn = false;
    private Vector3 pos;
    public float frequency = 20.0f;
    public float magnitude = 0.5f;

    private void Start()
    {
        pos = transform.position;
    }

    private void Update()
    {
        if (hardTurn)
        {
            if (goingUp)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + Time.deltaTime * speed) + speed * Time.deltaTime * Vector2.left;
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - Time.deltaTime * speed) + speed * Time.deltaTime * Vector2.left;
            }
        }
        else
        {
            if (!leftToRight)
                pos += -transform.right * Time.deltaTime * speed;
            else
                pos += transform.right * Time.deltaTime * speed;
            transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        }


        upTicks++;

        if (upTicks >= maxTicks)
        {
            goingUp = !goingUp;
            upTicks = 0;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Destroyer")
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.IncreaseScore(1);
            SFXManager.instance.SetVolume(0.3f);
            SFXManager.instance.PlaySFX("Pickup" + GameManager.instance.currentPickupSFX);
            GameManager.instance.currentPickupSFX++;
            if (GameManager.instance.currentPickupSFX == 8)
                GameManager.instance.currentPickupSFX = 1;
            GameObject pickupObj = Instantiate(pickupBurst, transform.position, Quaternion.identity);
            Destroy(pickupObj, 2f);
            Destroy(gameObject);
        }
    }

}

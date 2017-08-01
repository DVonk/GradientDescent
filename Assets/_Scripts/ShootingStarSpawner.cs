using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarSpawner : MonoBehaviour
{
    public bool leftToRight = false;
    public GameObject starPrefab;
    public float frequency;
    public bool hardTurn = false;

    float timer;

    private void Awake()
    {
        timer = frequency;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            float xPos;
            float yPos = Random.Range(3.5f, 7f) + transform.position.y;
            if (!leftToRight)
              xPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, 0)).x + Random.Range(0f, 3f);
            else
              xPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - Random.Range(0f, 3f);

            Vector2 position = new Vector2(xPos, yPos);
            GameObject starObj = Instantiate(starPrefab, position, Quaternion.identity);
            ShootingStar star = starObj.GetComponent<ShootingStar>();
            star.frequency = Random.Range(1f, 3f);
            star.leftToRight = leftToRight;
            if (hardTurn)
                star.hardTurn = true;
            timer = frequency;
        }
    }

}

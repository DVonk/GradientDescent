using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public float runSpeed = 2f;
    public float jumpForce;

    public bool falling = false;

    float move;
    float jump;
    bool grounded = false;
    public bool frozen = false;

    Rigidbody2D rb2d;
    public GameObject line;
    TrailRenderer trail;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        trail = line.GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        Invoke("ActivateTrail", 0.2f);

    }

    public void ActivateTrail()
    {
        trail.Clear();
        line.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (!frozen)
            rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);
    }

    private void Update()
    {
        //Movement
        move = Input.GetAxisRaw("Horizontal");
        if (!falling)
            jump = Input.GetAxisRaw("Vertical");
        else
            jump = 0f;

        if (Input.GetKey(KeyCode.LeftShift))
            move *= runSpeed;
        else
            move *= speed;

        if (jump != 0 && !frozen)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y + jumpForce * jump);
        }

        //Line
        if (falling)
        {
            if (trail.time > 0f)
                trail.time = Mathf.Clamp(Mathf.Lerp(trail.time, 0.0f, 0.025f), 0, 1.5f);
            else
                trail.Clear();
        }
        else if (grounded)
        {
            if (trail.time > 0f)
                trail.time = Mathf.Clamp(Mathf.Lerp(trail.time, 0.0f, 0.1f), 0, 1.5f);
            else
                trail.Clear();

        }
        else
            trail.time = 1.5f;



    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            grounded = true;
        }

    }


    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            grounded = false;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour {
    float heightMin = -1.0f;
    float heightMax = 4.0f;
    float jumpSpeed = 7f;
    public float moveSpeed;
    private Rigidbody rb;
    public bool grounded;
    public AudioSource jump;



    void Start()
    {
        grounded = true;
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * moveSpeed);


        if (grounded)
        {
            if (Input.GetButtonDown("Jump") || Input.GetAxis("JumpJoy") == -1)
            {
                rb.velocity += new Vector3(0, 9.5f, 0) + (movement);
                grounded = false;
                jump.Play();
            }

        }

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            grounded = true;
        }
    }

}

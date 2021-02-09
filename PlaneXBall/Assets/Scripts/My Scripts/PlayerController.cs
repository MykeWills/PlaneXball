using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {

    [Header("Audio")]
    AudioSource audioSrc;
    public AudioClip jump;
    public AudioClip boost;
    public AudioClip bump;
    public AudioClip cameraSwitch;
    [Space]
    [Header("BallSettings")]
    public float moveSpeed;
    public bool ShotBall;
    public bool ShotBallUp;
    public float ShotForce = 20f;
    public float ShotUpForce = 200f;
    public float jumpForce = 6f;
    [Space]
    [Header("Assign Camera")]
    public bool MoveCameraUp;
    public bool MoveCameraDown;
    public bool MoveCameraLeft;
    public bool MoveCameraRight;
    public bool MoveCameraTop;

    public GameObject CameraUp;
    public GameObject CameraDown;
    public GameObject CameraLeft;
    public GameObject CameraRight;
    public GameObject CameraTop;
    public GameObject WarpSpeed;
    private Rigidbody rb;
    bool grounded;
    bool FacingUp;
    bool FacingDown;
    bool FacingLeft;
    bool FacingRight;
    bool FacingTop;

    void Start()
    {
        grounded = true;
        ShotBall = false;
        ShotBallUp = false;
        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            audioSrc.PlayOneShot(cameraSwitch);
            MoveCameraDown = false;
            MoveCameraLeft = false;
            MoveCameraRight = false;
            MoveCameraUp = false;
            MoveCameraTop = true;

            FacingLeft = false;
            FacingDown = false;
            FacingRight = false;
            FacingUp = false;
            FacingTop = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            audioSrc.PlayOneShot(cameraSwitch);
            MoveCameraTop = false;
            MoveCameraDown = false;
            MoveCameraLeft = false;
            MoveCameraRight = false;
            MoveCameraUp = true;

            FacingTop = false;
            FacingLeft = false;
            FacingDown = false;
            FacingRight = false;
            FacingUp = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            audioSrc.PlayOneShot(cameraSwitch);
            MoveCameraTop = false;
            MoveCameraLeft = false;
            MoveCameraRight = false;
            MoveCameraUp = false;
            MoveCameraDown = true;

            FacingTop = false;
            FacingLeft = false;
            FacingRight = false;
            FacingUp = false;
            FacingDown = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            audioSrc.PlayOneShot(cameraSwitch);
            MoveCameraTop = false;
            MoveCameraRight = false;
            MoveCameraUp = false;
            MoveCameraDown = false;
            MoveCameraLeft = true;

            FacingTop = false;
            FacingDown = false;
            FacingRight = false;
            FacingUp = false;
            FacingLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            audioSrc.PlayOneShot(cameraSwitch);
            MoveCameraTop = false;
            MoveCameraLeft = false;
            MoveCameraUp = false;
            MoveCameraDown = false;
            MoveCameraRight = true;

            FacingTop = false;
            FacingLeft = false;
            FacingDown = false;
            FacingUp = false;
            FacingRight = true;
        }
        if (MoveCameraUp)
        {
            CameraDown.SetActive(false);
            CameraLeft.SetActive(false);
            CameraRight.SetActive(false);
            CameraTop.SetActive(false);
            CameraUp.SetActive(true);
        }
        else if (MoveCameraDown)
        {
            CameraUp.SetActive(false);
            CameraLeft.SetActive(false);
            CameraRight.SetActive(false);
            CameraTop.SetActive(false);
            CameraDown.SetActive(true);
        }
        else if (MoveCameraLeft)
        {
            CameraUp.SetActive(false);
            CameraDown.SetActive(false);
            CameraRight.SetActive(false);
            CameraTop.SetActive(false);
            CameraLeft.SetActive(true);
        }
        else if (MoveCameraRight)
        {
            CameraUp.SetActive(false);
            CameraDown.SetActive(false);
            CameraLeft.SetActive(false);
            CameraTop.SetActive(false);
            CameraRight.SetActive(true);
        }
        else if (MoveCameraTop)
        {
            CameraUp.SetActive(false);
            CameraDown.SetActive(false);
            CameraLeft.SetActive(false);
            CameraRight.SetActive(false);
            CameraTop.SetActive(true);
        }
        else
        {
            CameraDown.SetActive(false);
            CameraLeft.SetActive(false);
            CameraRight.SetActive(false);
            CameraTop.SetActive(false);
            CameraUp.SetActive(true);

            FacingLeft = false;
            FacingDown = false;
            FacingRight = false;
            FacingTop = false;
            FacingUp = true;
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (MoveCameraUp)
        {
            FacingLeft = false;
            FacingDown = false;
            FacingRight = false;
            FacingUp = true;
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * moveSpeed);

            if (grounded)
            {
                WarpSpeed.SetActive(false);
                ShotForce = 20;
                if (Input.GetButtonDown("Jump") || Input.GetAxis("JumpJoy") == -1)
                {
                    rb.velocity += new Vector3(0, jumpForce, 0) + (movement);
                    grounded = false;
                    audioSrc.PlayOneShot(jump);
                }
            }
        }
        else if (MoveCameraTop)
        {
            FacingLeft = false;
            FacingDown = false;
            FacingRight = false;
            FacingUp = false;
            FacingTop = true;
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * moveSpeed);

            if (grounded)
            {
                WarpSpeed.SetActive(false);
                ShotForce = 20;
                if (Input.GetButtonDown("Jump") || Input.GetAxis("JumpJoy") == -1)
                {
                    rb.velocity += new Vector3(0, jumpForce, 0) + (movement);
                    grounded = false;
                    audioSrc.PlayOneShot(jump);
                }
            }
        }
        else if (MoveCameraDown)
        {
            FacingLeft = false;
            FacingRight = false;
            FacingUp = false;
            FacingTop = false;
            FacingDown = true;
            Vector3 movement = new Vector3(-moveHorizontal, 0.0f, -moveVertical);
            rb.AddForce(movement * moveSpeed);

            if (grounded)
            {
                WarpSpeed.SetActive(false);
                ShotForce = 20;
                if (Input.GetButtonDown("Jump") || Input.GetAxis("JumpJoy") == -1)
                {
                    rb.velocity += new Vector3(0, jumpForce, 0) + (movement);
                    grounded = false;
                    audioSrc.PlayOneShot(jump);
                }
            }
        }
        else if (MoveCameraLeft)
        {
            FacingDown = false;
            FacingRight = false;
            FacingUp = false;
            FacingTop = false;
            FacingLeft = true;

            Vector3 movement = new Vector3(moveVertical, 0.0f, -moveHorizontal);
            rb.AddForce(movement * moveSpeed);

            if (grounded)
            {
                WarpSpeed.SetActive(false);
                ShotForce = 20;
                if (Input.GetButtonDown("Jump") || Input.GetAxis("JumpJoy") == -1)
                {
                    rb.velocity += new Vector3(0, jumpForce, 0) + (movement);
                    grounded = false;
                    audioSrc.PlayOneShot(jump);
                }
            }

        }
        else if (MoveCameraRight)
        {
            FacingLeft = false;
            FacingDown = false;
            FacingUp = false;
            FacingTop = false;
            FacingRight = true;
            Vector3 movement = new Vector3(-moveVertical, 0.0f, moveHorizontal);
            rb.AddForce(movement * moveSpeed);

            if (grounded)
            {
                WarpSpeed.SetActive(false);
                ShotForce = 20;
                if (Input.GetButtonDown("Jump") || Input.GetAxis("JumpJoy") == -1)
                {
                    rb.velocity += new Vector3(0, jumpForce, 0) + (movement);
                    grounded = false;
                    audioSrc.PlayOneShot(jump);
                }
            }
           
        }
        else
        {
            FacingLeft = false;
            FacingDown = false;
            FacingRight = false;
            FacingTop = false;
            FacingUp = true;
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * moveSpeed);

            if (grounded)
            {
                WarpSpeed.SetActive(false);
                ShotForce = 20;
                if (Input.GetButtonDown("Jump") || Input.GetAxis("JumpJoy") == -1)
                {
                    rb.velocity += new Vector3(0, jumpForce, 0) + (movement);
                    grounded = false;
                    audioSrc.PlayOneShot(jump);
                }
            }
        }
        if (ShotBall)
        {
            audioSrc.PlayOneShot(boost);
            if (FacingUp)
            {
                rb.AddForce(Vector3.forward * ShotForce, ForceMode.VelocityChange);
            }
            else if (FacingDown)
            {
                rb.AddForce(Vector3.back * ShotForce, ForceMode.VelocityChange);
            }
            else if (FacingLeft)
            {
                rb.AddForce(Vector3.right * ShotForce, ForceMode.VelocityChange);
            }
            else if (FacingRight)
            {
                rb.AddForce(Vector3.left * ShotForce, ForceMode.VelocityChange);
            }
            else
            {
                rb.AddForce(Vector3.forward * ShotForce, ForceMode.VelocityChange);
            }
            ShotBall = false;
        }
        if (ShotBallUp)
        {
            audioSrc.PlayOneShot(bump);
            rb.AddForce(Vector3.up * ShotUpForce, ForceMode.VelocityChange);
            ShotBallUp = false;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            grounded = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CameraUp"))
        {
            MoveCameraDown = false;
            MoveCameraLeft = false;
            MoveCameraRight = false;
            MoveCameraTop = false;
            MoveCameraUp = true;
        }
        if (other.gameObject.CompareTag("CameraDown"))
        {
            MoveCameraLeft = false;
            MoveCameraRight = false;
            MoveCameraUp = false;
            MoveCameraTop = false;
            MoveCameraDown = true;
        }
        if (other.gameObject.CompareTag("CameraLeft"))
        {
            MoveCameraRight = false;
            MoveCameraUp = false;
            MoveCameraDown = false;
            MoveCameraTop = false;
            MoveCameraLeft = true;
        }
        if (other.gameObject.CompareTag("CameraRight"))
        {
            MoveCameraLeft = false;
            MoveCameraUp = false;
            MoveCameraDown = false;
            MoveCameraTop = false;
            MoveCameraRight = true;
        }
        if (other.gameObject.CompareTag("CameraTop"))
        {
            MoveCameraLeft = false;
            MoveCameraUp = false;
            MoveCameraDown = false;
            MoveCameraRight = false;
            MoveCameraTop = true;
        }
        if (other.gameObject.CompareTag("Shoot"))
        {
            ShotBall = true;
        }
        if (other.gameObject.CompareTag("SuperShoot"))
        {
            WarpSpeed.SetActive(true);
            ShotForce = 250;
            ShotBall = true;
        }

        if (other.gameObject.CompareTag("ShootUp"))
        {
            ShotBallUp = true;
        }
    }
}

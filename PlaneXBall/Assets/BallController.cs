using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
   

    [Header("ControllerSettings")]
    public bool EnabledGamepad;
    public float moveSpeed;
    public float LookSensitivity;
    public float GamepadSensitivity;
    float SensitivityMultiplier = 75f;
    public bool InvertedCameraY;
    public bool InvertedCameraX;


    private Rigidbody rb;
    GameObject BallObject;
    GameObject Camera;
    private Vector3 offset;
    float degreesPerSecond = -65.0f;
    public static bool CameraUp;
    public static bool CameraRight;
    public static bool CameraDown;
    public static bool CameraLeft;

   

    // Use this for initialization
    void Start()
    {
        BallObject = GameObject.Find("PlayerNew/Ball/BallObject");
        Camera = GameObject.Find("PlayerNew/Ball/Camera");
        rb = BallObject.GetComponent<Rigidbody>();
        offset = Camera.transform.position - BallObject.transform.position;
    }
    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float AxisY = Camera.transform.rotation.eulerAngles.y;
       

        //Move Right
        if (AxisY > 225f && AxisY < 315f)
        {
            CameraUp = false;
            CameraDown = false;
            CameraLeft = false;
            CameraRight = true;
            Vector3 movement = new Vector3(-moveVertical, 0.0f, moveHorizontal);
            rb.AddForce(movement * moveSpeed);
        }
        //Move Down
        if (AxisY > 135f && AxisY < 225f)
        {
            CameraUp = false;
            CameraDown = true;
            CameraLeft = false;
            CameraRight = false;
            Vector3 movement = new Vector3(-moveHorizontal, 0.0f, -moveVertical);
            rb.AddForce(movement * moveSpeed);
        }
        // Move Left
        if (AxisY > 45f && AxisY < 135f)
        {
            CameraUp = false;
            CameraDown = false;
            CameraLeft = true;
            CameraRight = false;
            Vector3 movement = new Vector3(moveVertical, 0.0f, -moveHorizontal);
            rb.AddForce(movement * moveSpeed);
        }
        //Move Forward
        if(AxisY < 45f && AxisY >= 0 || AxisY <=360 && AxisY > 315f)
        {
            CameraUp = true;
            CameraDown = false;
            CameraLeft = false;
            CameraRight = false;
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * moveSpeed);
        }
    }

    private void LateUpdate()
    {
        float MouseX = Input.GetAxisRaw("Mouse X");
        Camera.transform.position = BallObject.transform.position + offset;
        offset = Quaternion.AngleAxis(degreesPerSecond * MouseX / LookSensitivity, Vector3.up) * offset;
        
        float MouseY = Input.GetAxisRaw("Mouse Y");
        Camera.transform.position = BallObject.transform.position + offset;
        if (InvertedCameraY)
        {
            if (CameraLeft)
            {
                offset = Quaternion.AngleAxis(degreesPerSecond * MouseY / LookSensitivity / 2, Vector3.forward) * offset;
            }
            else if (CameraRight)
            {
                offset = Quaternion.AngleAxis(degreesPerSecond * MouseY / LookSensitivity / 2, Vector3.back) * offset;
            }
            else if (CameraUp)
            {
                offset = Quaternion.AngleAxis(degreesPerSecond * MouseY / LookSensitivity / 2, Vector3.left) * offset;
            }
            else
            {
                offset = Quaternion.AngleAxis(degreesPerSecond * MouseY / LookSensitivity / 2, Vector3.right) * offset;
            }
        }
        else
        {
            if (CameraLeft)
            {
                offset = Quaternion.AngleAxis(degreesPerSecond * MouseY / LookSensitivity / 2, Vector3.back) * offset;
            }
            else if (CameraRight)
            {
                offset = Quaternion.AngleAxis(degreesPerSecond * MouseY / LookSensitivity / 2, Vector3.forward) * offset;
            }
            else if (CameraUp)
            {
                offset = Quaternion.AngleAxis(degreesPerSecond * MouseY / LookSensitivity / 2, Vector3.right) * offset;
            }
            else
            {
                offset = Quaternion.AngleAxis(degreesPerSecond * MouseY / LookSensitivity / 2, Vector3.left) * offset;
            }
        }


        if (EnabledGamepad)
        {
            float LookX = Input.GetAxisRaw("Joy X");
            Camera.transform.position = BallObject.transform.position + offset;
            offset = Quaternion.AngleAxis(degreesPerSecond * LookX / LookSensitivity, Vector3.up) * offset;

            float LookY = Input.GetAxisRaw("Joy Y");
            Camera.transform.position = BallObject.transform.position + offset;
            if (InvertedCameraY)
            {
                if (CameraLeft)
                {
                    offset = Quaternion.AngleAxis(degreesPerSecond * LookY / LookSensitivity, Vector3.forward) * offset;
                }
                else if (CameraRight)
                {
                    offset = Quaternion.AngleAxis(degreesPerSecond * LookY / LookSensitivity, Vector3.back) * offset;
                }
                else if (CameraUp)
                {
                    offset = Quaternion.AngleAxis(degreesPerSecond * LookY / LookSensitivity, Vector3.left) * offset;
                }
                else
                {
                    offset = Quaternion.AngleAxis(degreesPerSecond * LookY / LookSensitivity, Vector3.right) * offset;
                }
            }
            else
            {
                if (CameraLeft)
                {
                    offset = Quaternion.AngleAxis(degreesPerSecond * LookY / LookSensitivity, Vector3.back) * offset;
                }
                else if (CameraRight)
                {
                    offset = Quaternion.AngleAxis(degreesPerSecond * LookY / LookSensitivity, Vector3.forward) * offset;
                }
                else if (CameraUp)
                {
                    offset = Quaternion.AngleAxis(degreesPerSecond * LookY / LookSensitivity, Vector3.right) * offset;
                }
                else
                {
                    offset = Quaternion.AngleAxis(degreesPerSecond * LookY / LookSensitivity, Vector3.left) * offset;
                }
            }


        }

        Camera.transform.LookAt(BallObject.transform);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement: MonoBehaviour
{
    [Header("Other Settings")]
    public GameObject target;
    float speed = 1.0f;
    public bool ReverseDirection;
    public bool LinearlyStraight;
    public bool OrbitingTarget;
    public bool LinearlyTarget;


    [Header("Other Settings")]
    public bool Orbit;
    public bool X;
    public bool Y;
    public bool Z;
    private Vector3 distance;
    public float degreesPerSecond = 30.0f;
    public float DistanceX = 0.0f;
    public float DistanceY = 0.0f;
    public float DistanceZ = 0.0f;




    

    // Use this for initialization
    void Start()
    {
        distance = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (X)
        {
            distance.x = DistanceX;
        }
        else if (Y)
        {
            distance.y = DistanceY;
        }
        else if (Z)
        {
            distance.z = DistanceZ;
        }

        if (ReverseDirection)
        {
            if (LinearlyStraight)
            {
                gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else if (OrbitingTarget)
            {
                gameObject.transform.Translate(-transform.right * speed * Time.deltaTime);
                gameObject.transform.LookAt(target.transform);
            }
            else if (LinearlyTarget)
            {
                gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
                gameObject.transform.LookAt(target.transform);
            }
            else if (Orbit)
            {
                distance = Quaternion.AngleAxis(-degreesPerSecond * Time.deltaTime, Vector3.up) * distance;
                transform.position = target.transform.position + distance;
                gameObject.transform.LookAt(target.transform);
            }

        }
        else
        {
            if (LinearlyStraight)
            {
                gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if (OrbitingTarget)
            {
                gameObject.transform.Translate(transform.right * speed * Time.deltaTime);
                gameObject.transform.LookAt(target.transform);
            }
            else if (LinearlyTarget)
            {
                gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
                gameObject.transform.LookAt(target.transform);
            }
            else if (Orbit)
            {
                
                distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.up) * distance;
                transform.position = target.transform.position + distance;
                gameObject.transform.LookAt(target.transform);
            }
        }
    }
}

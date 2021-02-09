using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public GameObject player;
    Transform playerPosition;
    private Vector3 offset;

    void Start()
    {

        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        playerPosition = player.transform;
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.transform.position + offset;
        transform.LookAt(playerPosition);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFall : MonoBehaviour {
    public float fallingSpeed;
    public bool falling;
    Vector3 OriginalPos;


    // Use this for initialization
    void Start ()
    {
        falling = false;
        OriginalPos = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (falling)
        {
            transform.Translate(Vector3.up * -fallingSpeed * Time.deltaTime);
            if (gameObject.transform.position.y <= -5)
            {
                falling = false;
            }
        }
        else
        {
            transform.Translate(Vector3.up * fallingSpeed * 2 * Time.deltaTime);
            if (gameObject.transform.position.y >= OriginalPos.y)
            {
                gameObject.transform.position = OriginalPos;
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            falling = true;
        }
    }

}

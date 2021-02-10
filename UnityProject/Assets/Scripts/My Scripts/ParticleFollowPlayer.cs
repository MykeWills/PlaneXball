using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPlayer : MonoBehaviour {
    GameObject Ball;
    bool ballFound;

    private Vector3 OffSet;
	void Start () {
        StartCoroutine(FindPlayerBall());
        if (ballFound)
        {
            OffSet = transform.position - Ball.transform.position;
        }
    }
   
    // Update is called once per frame
    void LateUpdate () {
        StartCoroutine(FindPlayerBall());
        if (ballFound)
        {
            transform.position = Ball.transform.position + OffSet;
        }
        
    }
    IEnumerator FindPlayerBall()
    {
        Ball = GameObject.Find("/GameControl/Player/Character/Ball/");
        if (Ball == null)
        {
            yield return null;
        }
        else
        {
            Ball = GameObject.Find("/GameControl/Player/Character/Ball/");
            ballFound = true;
        }
    }
}

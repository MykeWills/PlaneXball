using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherFollow : MonoBehaviour {

    GameObject BallObject;
    private Vector3 offset;
    // Use this for initialization
    void Start () {
        BallObject = GameObject.Find("/GameControl/PlayerNew/Ball/");
        offset = transform.position - BallObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void LateUpdate()
    {
      
        transform.position = BallObject.transform.position + offset;
    }
    IEnumerator FindBall()
    {
        BallObject = GameObject.Find("/GameControl/PlayerNew/Ball/");
        if (BallObject == null)
        {
            yield return null;
        }
        else
        {
            BallObject = GameObject.Find("/GameControl/PlayerNew/Ball/");
           
        }
    }
}

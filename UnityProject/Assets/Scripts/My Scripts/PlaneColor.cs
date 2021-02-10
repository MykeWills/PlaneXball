using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneColor : MonoBehaviour {
    public float speed = 1.0f;

    float timeStamp;

    void Start()
    {
     
    }

    void Update()
    {

        Color newColor;


        newColor.r = (Mathf.Sin(Time.time * speed) + 2f) / 1f;
        newColor.g = (Mathf.Sin(Time.time * speed) + 2f) / 1f;
        newColor.b = (Mathf.Sin(Time.time * speed) + 2f) / 1f;
        newColor.a = (Mathf.Sin(Time.time * speed) + 2f) / 1f;

        GetComponent<Renderer>().material.color = newColor;


    }
}
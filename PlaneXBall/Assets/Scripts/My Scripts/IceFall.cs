using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFall : MonoBehaviour {
    public float fallingSpeed;
    public float FallDistance;
    public float fadePerSecond;
    MeshRenderer FadeMaterial;
    bool active;
    public float FallTime;
    public float Timer;
    public bool falling;
    float timeStamp;
    public float respawnTime = 1.0f;
    Vector3 OriginalPos;
    public BoxCollider BoxCollision;
    BoxCollider BoxTrigger;
    AudioSource audioSrc;
    public AudioClip IceEnterSound;
    public AudioClip IceExitSound;

    // Use this for initialization
    void Start () {
        active = true;
        falling = false;
        OriginalPos = gameObject.transform.position;
        FadeMaterial = GetComponent<MeshRenderer>();
        audioSrc = GetComponent<AudioSource>();
        BoxTrigger = GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        if (Timer < 0)
        {
            falling = true; ;
            Timer = 0;
        }
        if (falling)
        {
            BoxCollision.enabled = false;
            BoxTrigger.enabled = false;
            var color = FadeMaterial.material.color;
            FadeMaterial.material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
            transform.Translate(Vector3.up * -fallingSpeed * Time.deltaTime);
        }
        else
        {
            var color = FadeMaterial.material.color;
            FadeMaterial.material.color = new Color(color.r, color.g, color.b, 1);
        }

        if (transform.localPosition.y < FallDistance)
        {
            falling = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;

        }
        if (active)
        {
            if (gameObject.GetComponent<MeshRenderer>().enabled == false)
            {
                timeStamp = Time.time;
                active = false;
            }
        }
        else
        {
            if ((Time.time - timeStamp) >= respawnTime)
            {
                
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                BoxCollision.enabled = true;
                BoxTrigger.enabled = true;
                gameObject.transform.position = OriginalPos;

                active = true;


            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Timer += FallTime;
            audioSrc.clip = IceEnterSound;
            audioSrc.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSrc.clip = IceExitSound;
            audioSrc.Play();
            falling = true;
        }
    }

}

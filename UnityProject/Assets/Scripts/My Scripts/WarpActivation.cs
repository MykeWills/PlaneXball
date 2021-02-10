using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpActivation : MonoBehaviour {

    TextureScroll ScrollingTexture;
    MeshRenderer Texture;
    public GameObject Glow;
    public int KeyCount;
    public bool Green;
    public bool Red;
    public bool Blue;
    public bool Portal;
    ParticleSystem Particles;
    AudioSource audioSrc;
    public AudioClip WarpAvailable;
    bool PlaySound1;
    bool PlaySound2;
    bool PlaySound3;
    // Use this for initialization
    void Start()
    {
        Glow.SetActive(false);
        Texture = GetComponent<MeshRenderer>();
        ScrollingTexture = GetComponent<TextureScroll>();
        Texture.enabled = false;
        ScrollingTexture.enabled = false;
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = WarpAvailable;
        if (Portal)
        {
            Particles = GetComponent<ParticleSystem>();
            Particles.Stop();
        }

    }
	// Update is called once per frame
	void Update () {

        KeyCount = PlayerRespawnNew.KeyCount;
        if (KeyCount >= 2 && Green)
        {
            if (!PlaySound1)
            {
                audioSrc.Play();
                PlaySound1 = true;
            }
            Texture.enabled = true;
            ScrollingTexture.enabled = true;
            Glow.SetActive(true);
        }
        if (KeyCount >= 4 && Red)
        {
            if (!PlaySound2)
            {
                audioSrc.Play();
                PlaySound2 = true;
            }
            Texture.enabled = true;
            ScrollingTexture.enabled = true;
            Glow.SetActive(true);
        }
        if (KeyCount >= 6 && Blue)
        {
            if (!PlaySound3)
            {
                audioSrc.Play();
                PlaySound3 = true;
            }
            Texture.enabled = true;
            ScrollingTexture.enabled = true;
            Glow.SetActive(true);
        }
        if (KeyCount >= 8 && Portal)
        {
            Particles.Play();
            if (!PlaySound3)
            {
                audioSrc.Play();
                PlaySound3 = true;
            }
            Texture.enabled = true;
            ScrollingTexture.enabled = true;
            Glow.SetActive(true);
        }
    }
}

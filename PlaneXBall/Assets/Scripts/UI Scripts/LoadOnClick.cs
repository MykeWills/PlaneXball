using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour
{
    public GameObject SceneLoader;
    public GameObject MainMenuCanvas;
    public GameObject MusicPlayer;
    AudioSource audioSrc;

    private void Start()
    {
        audioSrc = MusicPlayer.GetComponent<AudioSource>();

    }
    public void LoadByIndex(int SceneIndex)
    {
        audioSrc.Stop();
        SceneLoader.SetActive(true);
        SceneManager.LoadScene(SceneIndex);
        MainMenuCanvas.SetActive(false);
    }
}



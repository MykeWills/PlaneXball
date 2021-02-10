using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool loadScene = false;
    int scene;
    public int levelCounter;
    [SerializeField]
    private Text loadingText;
    AudioSource audioSrc;
    public AudioClip Level1Music;
    public AudioClip Level2Music;
    public AudioClip Level3Music;
    public AudioClip MainMenuMusic;
    public GameObject Player;
    public GameObject SceneLoad;
    public GameObject MusicPlayer;
    public GameObject MainMenuCanvas;
  

    private void Start()
    {
        audioSrc = MusicPlayer.GetComponent<AudioSource>();
        Player.SetActive(false);
    }


    void Update()
    {
        levelCounter = PlayerRespawnNew.Level;

        if(levelCounter == 0)
        {
            scene = 2;
            audioSrc.clip = Level1Music;
            
        }
        if (levelCounter == 1)
        {
            scene = 4;
            audioSrc.clip = Level2Music;
        }
        if (levelCounter == 2)
        {
            scene = 6;
            audioSrc.clip = Level3Music;
        }
        if (loadScene == false)
        {
            loadScene = true;
            loadingText.text = "Loading...";
            StartCoroutine(LoadNewScene());
        }
        if (loadScene == true)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }
    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
      
        while (!async.isDone)
        {
            yield return null;
        }
        if (async.isDone)
        {
            Player.SetActive(true);
            SceneLoad.SetActive(false);
            SceneManager.LoadScene(scene);
            audioSrc.Play();
            loadScene = false;
        }
    }
}
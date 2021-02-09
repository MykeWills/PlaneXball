using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerRespawnNew : MonoBehaviour {
    // Static Variables//
    public static int score;
    public static int lives;
    public static int JewelCount;
    public static int KeyCount;
    public static int Level;
    // Weather //
    [Header("Level Weather")]
    Vector3 WeatherOffset;
    public GameObject Ash;
    public GameObject Snow;
    public GameObject Rain;
    GameObject Weather;

    // CheatMode //
    private string[] cheatCode = new string[5];
    int index;
    public static bool cheat;

    // MainMenu Variables //
    [Space]
    [Header("Game Control")]
    public GameObject Player;
    public GameObject SceneLoader;
    public GameObject MainMenuCanvas;

    // Jewel Collection //
    [Space]
    [Header("Jewel Break Prefabs")]
    GameObject expl;
    public GameObject explosionGreenPrefab;
    public GameObject explosionRedPrefab;
    public GameObject explosionBluePrefab;
    public GameObject explosionYellowPrefab;
    public GameObject explosionWhitePrefab;

    // Level Positions //
    GameObject StartOne;
    GameObject StartTwo;
    GameObject StartThree;
    GameObject StartFour;
    GameObject Level1StartPosition;
    GameObject Level2StartPosition;
    GameObject Level3StartPosition;
    public static Vector3 LastCheckPoint;
    bool ShutOffLevel1Finder = false;
    bool ShutOffLevel2Finder = false;
    bool ShutOffLevel3Finder = false;
    bool FoundPosition;

    [Space]
    [Header("Game UI")]
    // Interface Variables //
    public Text counterText;
    public Text checkPointText;
    float fadeTimer;


    [Space]
    [Header("Game Audio")]
    // Audio Variables //
    AudioSource audioSrc;
    AudioSource MusicSrc;
    public AudioClip KeySound;
    public AudioClip collectSound;
    public AudioClip collectSpecial;
    public AudioClip teleportSound;
    public AudioClip deathSound;
    public AudioClip overSound;
    public AudioClip levelWarp;
    public AudioClip cheater;
    public AudioClip MainMenuMusic;
    public GameObject MusicPlayer;

    // Game Variables

    float timer;
    float SceneTimer;
    bool win = false;
    bool LoadLevel = false;
    public static bool LoadStart;
    bool End = false;
    Rigidbody rb;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
        MusicSrc = MusicPlayer.GetComponent<AudioSource>();

        FoundPosition = false;
        LoadStart = false;
        LastCheckPoint = gameObject.transform.position;
        checkPointText.text = "";

        cheatCode = new string[5] { "c", "h", "e", "a", "t" };
        index = 0;
        
        Weather = Ash;
        WeatherOffset = Weather.transform.position - gameObject.transform.position;
        if (Level == 0)
        {
            StartGame();
        }
        
    }

    // Update is called once per frame
    void Update () {

        StartCoroutine(FindStartPosition());

        // ===================================Timers============================ //
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        if (timer >= 0.0f && cheat == true)
        {
            //SceneTimer = 0;
            score = 99999999;
            timer = 300;
            lives = 99;
            JewelCount = 999;
            KeyCount = 8;
            counterText.text = "CHEATMODE!";
            counterText.color = new Color(0f, 1f, 0f, Mathf.Sin(Time.time * 5));
        }
        else
        {
            if (timer < 0)
            {
                MusicSrc.clip = MainMenuMusic;
                MusicSrc.Play();
                rb.constraints = RigidbodyConstraints.FreezeAll;
                timer = 0;
                win = false;
                gameOver();
            }
            else if (timer > 0)
            {
                timer -= Time.deltaTime;
                counterText.text = string.Format("Time: {0:0}:{1:00}", minutes, seconds);
            }
            if (timer <= 10)
            {
                counterText.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
            }
            else if (timer >= 11)
            {
                counterText.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        if (fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
            checkPointText.text = "CheckPoint";
        }
        if (fadeTimer < 0)
        {
            fadeTimer = 0;
            checkPointText.text = "";
        }
        if (SceneTimer > 0)
        {
            SceneTimer -= Time.deltaTime;
        }
        if (SceneTimer < 0)
        {
            if (Level == 1 && LoadLevel)
            {
                ContinueGame();
                SceneManager.LoadScene(3);
                rb.constraints = RigidbodyConstraints.None;
                SceneLoader.SetActive(true);
                Player.SetActive(false);
                LoadLevel = false;
            }
            if (Level == 2 && LoadLevel)
            {
                ContinueGame();
                SceneManager.LoadScene(5);
                rb.constraints = RigidbodyConstraints.None;
                SceneLoader.SetActive(true);
                Player.SetActive(false);
                LoadLevel = false;
            }
            if (LoadStart)
            {
                FoundPosition = false;
                ShutOffLevel1Finder = false;
                ShutOffLevel2Finder = false; ;
                SceneManager.LoadScene(0);
                rb.constraints = RigidbodyConstraints.None;
                MainMenuCanvas.SetActive(true);
                StartGame();
                MusicSrc.clip = MainMenuMusic;
                MusicSrc.Play();
                Weather.SetActive(false);
                Player.SetActive(false);
                LoadStart = false;
            }
            SceneTimer = 0;
        }

        //==============Find First Level Position=====================//
        if (Level == 0 && !ShutOffLevel1Finder)
        {
            FoundPosition = false;
            ShutOffLevel1Finder = true;
        }
        if (Level == 0 && !FoundPosition)
        {
            StartCoroutine(WaitForLevel1Position());
        }

        //==============Find Second Level Position=====================//
        if (Level == 1 && !ShutOffLevel2Finder)
        {
            FoundPosition = false;
            ShutOffLevel2Finder = true;
        }
        if (Level == 1 && !FoundPosition)
        {
            StartCoroutine(WaitForLevel2Position());
        }

        //==============Find Third Level Position=====================//
        if (Level == 2 && !ShutOffLevel3Finder)
        {
            FoundPosition = false;
            ShutOffLevel3Finder = true;
        }
        if (Level == 2 && !FoundPosition)
        {
            StartCoroutine(WaitForLevel3Position());
        }
	}
    void LateUpdate()
    {

        Weather.gameObject.transform.position = gameObject.transform.position + WeatherOffset;

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCode[index]))
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
        
        if (index == cheatCode.Length)
        {
            
            if (Input.GetKey(KeyCode.Return))
            {
                audioSrc.clip = cheater;
                audioSrc.Play();
                cheat = true;

            }
            if (Input.GetKey(KeyCode.Alpha1))
            {
                Level = 0;
                SceneManager.LoadScene(1);
                ContinueGame();
                SceneLoader.SetActive(true);
                Player.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                Level = 1;
                SceneManager.LoadScene(3);
                ContinueGame();
                SceneLoader.SetActive(true);
                Player.SetActive(false);
            }
        }
    }
    IEnumerator FindStartPosition()
    {
        StartOne = GameObject.Find("/StartPositions/StartOne/");
        if (StartOne == null)
        {
            yield return null;
        }
        else
        {
            StartOne = GameObject.Find("/StartPositions/StartOne/");
        }

        StartTwo = GameObject.Find("/StartPositions/StartTwo/");
        if (StartTwo == null)
        {
            yield return null;
        }
        else
        {
            StartTwo = GameObject.Find("/StartPositions/StartTwo/");
        }

        StartThree = GameObject.Find("/StartPositions/StartThree/");
        if (StartThree == null)
        {
            yield return null;
        }
        else
        {
            StartThree = GameObject.Find("/StartPositions/StartThree/");
        }

        StartFour = GameObject.Find("/StartPositions/StartFour/");
        if (StartFour == null)
        {
            yield return null;
        }
        else
        {
            StartFour = GameObject.Find("/StartPositions/StartFour/");
        }
    }

    IEnumerator WaitForLevel1Position()
    {
        Level1StartPosition = GameObject.Find("/Level1StartPosition/");
        if (Level1StartPosition == null)
        {
            yield return null;
        }
        else
        {
            Level2StartPosition = GameObject.Find("/Level1StartPosition/");
            
            gameObject.transform.position = Level1StartPosition.transform.position;
            LastCheckPoint = gameObject.transform.position;
            FoundPosition = true;
        }

    }
    IEnumerator WaitForLevel2Position()
    {
        Level2StartPosition = GameObject.Find("/Level2StartPosition/");
        if (Level2StartPosition == null)
        {
            yield return null;
        }
        else
        {
            Level2StartPosition = GameObject.Find("/Level2StartPosition/");

            gameObject.transform.position = Level2StartPosition.transform.position;
            LastCheckPoint = gameObject.transform.position;
            FoundPosition = true;
        }

    }
    IEnumerator WaitForLevel3Position()
    {
        Level3StartPosition = GameObject.Find("/Level3StartPosition/");
        if (Level3StartPosition == null)
        {
            yield return null;
        }
        else
        {
            Level3StartPosition = GameObject.Find("/Level3StartPosition/");

            gameObject.transform.position = Level3StartPosition.transform.position;
            LastCheckPoint = gameObject.transform.position;
            FoundPosition = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // Player Dead
        if (other.gameObject.CompareTag("Death"))
        {
            onDeath();
        }
        // KeyJewel
        if (other.gameObject.CompareTag("Pick Up White"))
        {
            audioSrc.PlayOneShot(KeySound);
            KeyCount++;
            score += 400;
            ExplodeWhite();
            timer += 30;
            Destroy(other.gameObject);
            Destroy(expl, 2f);
        }

        // Green Jewel
        if (other.gameObject.CompareTag("Pick Up Green"))
        {
            audioSrc.PlayOneShot(collectSound);
            JewelCount++;
            score += 100;
            timer += 2;
            ExplodeGreen();
            Destroy(other.gameObject);
            Destroy(expl, 2f);
        }
        // Red Jewel
        else if (other.gameObject.CompareTag("Pick Up Red"))
        {
            audioSrc.PlayOneShot(collectSound);
            JewelCount++;
            score += 150;
            timer += 3;
            ExplodeRed();
            Destroy(other.gameObject);
            Destroy(expl, 2f);
        }
        // Blue Jewel
        else if (other.gameObject.CompareTag("Pick Up Blue"))
        {
            audioSrc.PlayOneShot(collectSound);
            JewelCount++;
            score += 300;
            timer += 4;
            ExplodeBlue();
            Destroy(other.gameObject);
            Destroy(expl, 2f);
        }
        // Secret (Orange/Yellow) Jewel
        else if (other.gameObject.CompareTag("Pick Up Special"))
        {
            audioSrc.PlayOneShot(collectSpecial);
            lives += 1;
            timer += 60;
            score += 5000;
            ExplodeYellow();
            Destroy(other.gameObject);
            Destroy(expl, 2f);
        }
        else if (other.gameObject.CompareTag("CheckPoint"))
        {
            LastCheckPoint = other.gameObject.transform.position;
            fadeTimer += 5;
            other.gameObject.SetActive(false);
        }
        //========================Level One Key Jewels============================//

        //------------------Section 1--------------------------//
        if (KeyCount >= 2 && other.gameObject.CompareTag("Warp"))
        {
            audioSrc.PlayOneShot(teleportSound);
            transform.position = StartTwo.transform.position;
            rb.velocity = Vector3.zero;
            LastCheckPoint = StartTwo.transform.position;
        }
        //------------------Section 2--------------------------//
        else if (KeyCount >= 4 && other.gameObject.CompareTag("Warp2"))
        {
            audioSrc.PlayOneShot(teleportSound);
            transform.position = StartThree.transform.position;
            rb.velocity = Vector3.zero;
            LastCheckPoint = StartThree.transform.position;
        }
        //------------------Section 3--------------------------//
        else if (KeyCount >= 6 && other.gameObject.CompareTag("Warp3"))
        {
            audioSrc.PlayOneShot(teleportSound);
            transform.position = StartFour.transform.position;
            rb.velocity = Vector3.zero;
            LastCheckPoint = StartFour.transform.position;
        }
        //------------------Section 4--------------------------//
        else if (KeyCount >= 8 && other.gameObject.CompareTag("Warp Level"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            win = true;
            gameOver();
        }
        if (other.gameObject.CompareTag("Teleport"))
        {
            transform.position = LastCheckPoint;
        }
    }
    void onDeath()
    {
        rb.velocity = Vector3.zero;
        lives -= 1;
        transform.position = LastCheckPoint;
      
        if (lives < 0)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            win = false;
            lives = 0;
            gameOver();
        }
        audioSrc.PlayOneShot(deathSound);
    }
    void gameOver()
    {
        
        if (!End)
        {
            if (win == false)
            {
                index = 0;
                Level = 0;
                timer = 0;
                LoadStart = true;
                audioSrc.PlayOneShot(overSound);
                counterText.text = "Game Over!";
                counterText.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1f);
                SceneTimer += 5;

            }
            if (win == true)
            {
                audioSrc.PlayOneShot(levelWarp);
                index = 0;
                timer = 0;
                Level++;
                LoadLevel = true;
                counterText.text = "You Win!";
                counterText.color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.time, 1));
                SceneTimer += 5;


            }
            End = true;
        }
    
    }
    public void StartGame()
    {
        if (Level == 0)
        {
            Ash.SetActive(true);
            Weather = Ash;
            Weather.SetActive(true);
        }
        index = 0;
        cheat = false;
        End = false;
        timer = 121;
        JewelCount = 0;
        SceneTimer = 0;
        lives = 3;
        score = 0;
        KeyCount = 0;
       

    }
    public void ContinueGame()
    {
        if (Level == 1)
        {
            Ash.SetActive(false);
            Snow.SetActive(true);
            Weather = Snow;
            Weather.SetActive(true);
        }
        if (Level == 2)
        {
            Snow.SetActive(false);
            Rain.SetActive(true);
            Weather = Rain;
            Weather.SetActive(true);
        }
        index = 0;
        if (cheat)
        {
            lives = 3;
            cheat = false;
        }
        timer = 121;
        counterText.text = "";
        KeyCount = 0;
        win = false;
        End = false;
    }
    public void EndGame()
    {
        Weather.SetActive(false);
        index = 0;
        timer = 0;
        LoadStart = true;
        SceneTimer += 5;
        Level = 0;
    }
    void ExplodeGreen()
    {
        expl = Instantiate(explosionGreenPrefab, transform.position, Quaternion.identity) as GameObject;
    }
    void ExplodeRed()
    {
        expl = Instantiate(explosionRedPrefab, transform.position, Quaternion.identity) as GameObject;
    }
    void ExplodeBlue()
    {
        expl = Instantiate(explosionBluePrefab, transform.position, Quaternion.identity) as GameObject;
    }
    void ExplodeYellow()
    {
        expl = Instantiate(explosionYellowPrefab, transform.position, Quaternion.identity) as GameObject;
    }
    void ExplodeWhite()
    {
        expl = Instantiate(explosionWhitePrefab, transform.position, Quaternion.identity) as GameObject;
    }

}

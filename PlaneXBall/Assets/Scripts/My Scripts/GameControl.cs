using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public static GameControl control;

    public int SceneNumber;
    public int LevelCounter;

    public GameObject PlayerObject;
    public GameObject SceneLoaderObject;

    GameObject StartingPosition;

    public bool LoadScene;
    public bool StartLoad;
    public bool LoadPlayer;
    public bool StartPlayer;

    GameObject ScoreObject;
    GameObject LivesObject;
    GameObject JewelObject;
    GameObject KeyObject;

    Text ScoreText;
    Text LivesText;
    Text JewelsText;
    Text KeysText;

    public int Score;
    public int Lives;
    public int JewelCount;
    public int KeyCount;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }
    void Start () {
        Score = PlayerRespawnNew.score;
        Lives = PlayerRespawnNew.lives;
        JewelCount = PlayerRespawnNew.JewelCount;
        KeyCount = PlayerRespawnNew.KeyCount;
        LevelCounter = PlayerRespawnNew.Level;
    }
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(FindCanvas());

        Score = PlayerRespawnNew.score;
        Lives = PlayerRespawnNew.lives;
        JewelCount = PlayerRespawnNew.JewelCount;
        KeyCount = PlayerRespawnNew.KeyCount;
        SceneNumber = SceneManager.GetActiveScene().buildIndex;

        if (Input.GetKey(KeyCode.F1))
        {
            Save();
        }
        if (Input.GetKey(KeyCode.F2))
        {
            Load();
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerSaveData.dat");
        PlayerData data = new PlayerData();
        SceneNumber = SceneManager.GetActiveScene().buildIndex;
        data.Score = Score;
        data.Lives = Lives;
        data.SceneToLoad = SceneNumber;
        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerSaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerSaveData.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            Score = data.Score;
            Lives = data.Lives;
            SceneNumber = data.SceneToLoad;
            SceneManager.LoadScene(SceneNumber);
        }
    }
    void SetCountScore()
    {
        ScoreText.text = "Score: " + Score.ToString();
    }
    void SetCountLives()
    {
        LivesText.text = "Lives: " + Lives.ToString();
    }
    void SetCountJewel()
    {
        JewelsText.text = "Gem Count: " + JewelCount.ToString();
    }
    void SetCountKey()
    {
        KeysText.text = "Keys: " + KeyCount + "/8".ToString();
    }
    IEnumerator FindCanvas()
    {
        ScoreObject = GameObject.Find("/GameControl/PlayerNew/Canvas/Count Score/");
        LivesObject = GameObject.Find("/GameControl/PlayerNew/Canvas/Count Lives/");
        JewelObject = GameObject.Find("/GameControl/PlayerNew/Canvas/Count Jewel/");
        KeyObject = GameObject.Find("/GameControl/PlayerNew/Canvas/Count Key/");

        if (ScoreObject == null)
        {
            yield return null;
        }
        else
        {
            ScoreObject = GameObject.Find("/GameControl/PlayerNew/Canvas/Count Score/");
            ScoreText = ScoreObject.GetComponent<Text>();
            SetCountScore();
        }
        if (JewelObject == null)
        {
            yield return null;
        }
        else
        {
            JewelObject = GameObject.Find("/GameControl/PlayerNew/Canvas/Count Jewel/");
            JewelsText = JewelObject.GetComponent<Text>();
            SetCountJewel();
        }
        if (KeyObject == null)
        {
            yield return null;
        }
        else
        {
            KeyObject = GameObject.Find("/GameControl/PlayerNew/Canvas/Count Key/");
            KeysText = KeyObject.GetComponent<Text>();
            SetCountKey();
        }
        if (LivesObject == null)
        {
            yield return null;
        }
        else
        {
            LivesObject = GameObject.Find("/GameControl/PlayerNew/Canvas/Count Lives/");
            LivesText = LivesObject.GetComponent<Text>();
            SetCountLives();
            if (Lives <= 0)
            {
                LivesText.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
            }
            else if (Lives >= 1)
            {
                LivesText.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
}
[Serializable]

class PlayerData
{
    public int Score;
    public int Lives;
    public int SceneToLoad;
}

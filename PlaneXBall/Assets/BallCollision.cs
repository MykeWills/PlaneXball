using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour {

    [Header("Audio")]
    AudioSource audioSrc;
    public AudioClip jump;
    public AudioClip boost;
    public AudioClip bump;
    public AudioClip powerupSnd;
    public AudioClip cameraSwitch;
    public AudioClip cheater;
    public AudioClip teleport;
    bool loadStart;
    public AudioClip breaking;
    public int WeaponCounter;

    public GameObject OrigBall;
    public GameObject BoostBall;
    public GameObject TeleBall;
    public GameObject BreakBall;
    public float TeleportTimer;
    [Space]
    [Header("BallSettings")]
    public float jumpForce = 6f;
    public bool grounded;
    Rigidbody rb;
    GameObject expl;
    public GameObject explosionWhitePrefab;

    bool cheat;
    public bool FacingUp;
    public bool FacingDown;
    public bool FacingLeft;
    public bool FacingRight;


    public bool PowerUp;
    public bool BoostUp;
    public bool TeleUp;
    public bool BreakUp;

    public bool PlaySoundAgain;
    public bool PlaySound;
    public Material OriBallMaterial;
    public Material BoostBallMaterial;
    public Material TeleBallMaterial;
    public Material BreakBallMaterial;
    public bool ShotBall;
    public bool SuperShot;
    public bool ShotBallUp;
    public float ShotForce;
    public float ShotUpForce;
    private string[] boostCode = new string[5];
    int index;
    GameObject WarpSpeed;
    Vector3 LastCheckpoint;
    // Use this for initialization
    void Start ()
    {
        //WeaponCounter = 0;
        boostCode = new string[5] { "b", "o", "o", "s", "t" };
        index = 0;
        grounded = true;
        ShotBall = false;
        ShotBallUp = false;
        audioSrc = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        WarpSpeed = GameObject.Find("PlayerNew/Ball/BallObject/WarpSpeed");
    }
	
	// Update is called once per frame
	void Update () {
        cheat = PlayerRespawnNew.cheat;
        loadStart = PlayerRespawnNew.LoadStart;
        LastCheckpoint = PlayerRespawnNew.LastCheckPoint;

        if (loadStart)
        {
            PowerUp = false;
            BreakUp = false;
            TeleUp = false;
            BoostUp = false;
        }
        if (cheat)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(boostCode[index]))
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
            }
            if (index == boostCode.Length)
            {
                audioSrc.clip = cheater;
                audioSrc.Play();
                PowerUp = true;
            }
        }
        if (TeleportTimer > 0)
        {
            TeleportTimer -= Time.deltaTime;
        }
        if (TeleportTimer < 0)
        {
            TeleportTimer = 0;
        }

        if (PowerUp)
        {
            OrigBall.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                audioSrc.PlayOneShot(cameraSwitch);
                if (BreakUp && !BreakUp && !TeleUp)
                {
                    if (WeaponCounter >= 1)
                    {
                        WeaponCounter = 1;
                    }
                    else
                    {
                        WeaponCounter++;
                    }
                }
                else if (BoostUp && BreakUp && !TeleUp)
                {
                    if (WeaponCounter >= 2)
                    {
                        WeaponCounter = 2;
                    }
                    else
                    {
                        WeaponCounter++;
                    }
                }
                else if (BreakUp && BoostUp && TeleUp)
                {
                    if (WeaponCounter >= 3)
                    {
                        WeaponCounter = 3;
                    }
                    else
                    {
                        WeaponCounter++;
                    }
                }



            }
            if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Joystick1Button4))
            {
                audioSrc.PlayOneShot(cameraSwitch);
                if (WeaponCounter <= 1)
                {
                    WeaponCounter = 1;
                }
                else
                {
                    WeaponCounter--;
                }
               
            }

            if (BoostUp && WeaponCounter == 2)
            {
                ChangeMaterial(BoostBallMaterial);
                BoostBall.SetActive(true);
                TeleBall.SetActive(false);
                BreakBall.SetActive(false);

                if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Joystick1Button3))
                {
                    ShotBall = true;
                }
                else
                {
                    ShotBall = false;
                }
            }
            if (TeleUp && WeaponCounter == 3)
            { 
                ChangeMaterial(TeleBallMaterial);
                BoostBall.SetActive(false);
                TeleBall.SetActive(true);
                BreakBall.SetActive(false); TeleBall.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    if (TeleportTimer == 0)
                    {
                        audioSrc.PlayOneShot(teleport);
                        rb.velocity = Vector3.zero;
                        gameObject.transform.position = LastCheckpoint;
                        TeleportTimer += 10f;
                    }
                   
                }
                
            }
            if (BreakUp && WeaponCounter == 1)
            {
                
                ChangeMaterial(BreakBallMaterial);
                BoostBall.SetActive(false);
                TeleBall.SetActive(false);
                BreakBall.SetActive(true);
                if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Joystick1Button1))
                {
                    audioSrc.PlayOneShot(breaking);
                    rb.velocity = Vector3.zero;
                }
            }
        }
        else
        {
            ChangeMaterial(OriBallMaterial);
            OrigBall.SetActive(true);
            BoostBall.SetActive(false);
            TeleBall.SetActive(false);
            BreakBall.SetActive(false);
        }
   

        if (grounded)
        {
            WarpSpeed.SetActive(false);
            
            if (Input.GetButtonDown("Jump") || Input.GetAxis("JumpJoy") == -1)
            {
                
                rb.velocity += new Vector3(0, jumpForce, 0);
                grounded = false;
                audioSrc.PlayOneShot(jump);
            }
        }
    }
    private void FixedUpdate()
    {
        FacingUp = BallController.CameraUp;
        FacingRight = BallController.CameraRight;
        FacingDown = BallController.CameraDown;
        FacingLeft = BallController.CameraLeft;

        if (FacingUp && !PlaySound)
        {
            //audioSrc.PlayOneShot(cameraSwitch);
            PlaySoundAgain = false;
            PlaySound = true;

        }
        if (FacingRight && !PlaySoundAgain)
        {
            //audioSrc.PlayOneShot(cameraSwitch);
            PlaySound = false;
            PlaySoundAgain = true;
        }
        if (FacingDown && !PlaySound)
        {
            //audioSrc.PlayOneShot(cameraSwitch);
            PlaySoundAgain = false;
            PlaySound = true;
        }
        if (FacingLeft && !PlaySoundAgain)
        {
            //audioSrc.PlayOneShot(cameraSwitch);
            PlaySound = false;
            PlaySoundAgain = true;
        }
        if (SuperShot)
        {
            ShotForce = 200;
            if (FacingUp)
            {
                rb.AddForce(Vector3.forward * ShotForce, ForceMode.VelocityChange);
            }
            else if (FacingDown)
            {
                rb.AddForce(Vector3.back * ShotForce, ForceMode.VelocityChange);
            }
            else if (FacingLeft)
            {
                rb.AddForce(Vector3.right * ShotForce, ForceMode.VelocityChange);
            }
            else if (FacingRight)
            {
                rb.AddForce(Vector3.left * ShotForce, ForceMode.VelocityChange);
            }
            else
            {
                rb.AddForce(Vector3.forward * ShotForce, ForceMode.VelocityChange);
            }
            ShotForce = 15;
            SuperShot = false;
        }
        if (ShotBall)
        {
            audioSrc.PlayOneShot(boost);
            if (FacingUp)
            {
                rb.AddForce(Vector3.forward * ShotForce, ForceMode.VelocityChange);
            }
            else if (FacingDown)
            {
                rb.AddForce(Vector3.back * ShotForce, ForceMode.VelocityChange);
            }
            else if (FacingLeft)
            {
                rb.AddForce(Vector3.right * ShotForce, ForceMode.VelocityChange);
            }
            else if (FacingRight)
            {
                rb.AddForce(Vector3.left * ShotForce, ForceMode.VelocityChange);
            }
            else
            {
                rb.AddForce(Vector3.forward * ShotForce, ForceMode.VelocityChange);
            }
            ShotForce = 15;
            ShotBall = false;
        }
        if (ShotBallUp)
        {
            audioSrc.PlayOneShot(bump);
            rb.AddForce(Vector3.up * ShotUpForce, ForceMode.VelocityChange);
            ShotBallUp = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            grounded = true;
        }
        if (other.gameObject.CompareTag("ShootUp"))
        {
            ShotBallUp = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shoot"))
        {
            ShotBall = true;
        }
        if (other.gameObject.CompareTag("SuperShoot"))
        {
            WarpSpeed.SetActive(true);
            SuperShot = true;
        }
        if (other.gameObject.CompareTag("ShootUp"))
        {
            ShotBallUp = true;
        }
        if (other.gameObject.CompareTag("BallPowerUp"))
        {
            WeaponCounter = 2;
            audioSrc.PlayOneShot(powerupSnd);
            ExplodeWhite();
            PowerUp = true;
            BoostUp = true;
            Destroy(other.gameObject);
            Destroy(expl, 2f);
        }
        if (other.gameObject.CompareTag("TelePowerUp"))
        {
            WeaponCounter = 3;
            audioSrc.PlayOneShot(powerupSnd);
            ExplodeWhite();
            PowerUp = true;
            TeleUp = true;
            Destroy(other.gameObject);
            Destroy(expl, 2f);
        }
        if (other.gameObject.CompareTag("BreakPowerUp"))
        {
            WeaponCounter = 1;
            audioSrc.PlayOneShot(powerupSnd);
            ExplodeWhite();
            PowerUp = true;
            BreakUp = true;
            Destroy(other.gameObject);
            Destroy(expl, 2f);
        }


    }
    void ChangeMaterial(Material newMat)
    {
        Renderer[] children;
        children = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = newMat;
            }
            rend.materials = mats;
        }
    }
    void ExplodeWhite()
    {
        expl = Instantiate(explosionWhitePrefab, transform.position, Quaternion.identity) as GameObject;
    }

}

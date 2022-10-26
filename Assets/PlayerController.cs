using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour {
    Animator anim;
    public static GameObject player;
    public static GameObject currentPlatform;
    bool canTurn = false;
    Vector3 startPosition;
    public static bool isDead = false;
    Rigidbody rb;
    int livesLeft;
    public Texture aliveIcon;
    public Texture deadIcon;
    public RawImage[] icons;
    public GameObject gameOverPanel;
    public TMP_Text highScore;

    //@@public delegate void playerDied();
    //@@public static event playerDied OnPlayerDeath;
    //@@bool test = false;

    public GameObject magic;
    public Transform magicStartPos;
    Rigidbody mRb;

    void RestartGame()
    {
        SceneManager.LoadScene("ScrollingWorld", LoadSceneMode.Single);
    }

    void OnCollisionEnter(Collision other) {
        if ((other.gameObject.tag == "Fire" || other.gameObject.tag == "Wall")&& !isDead) {
            anim.SetTrigger("isDead");
            isDead = true;
            livesLeft--;
            PlayerPrefs.SetInt("lives", livesLeft);
            if(livesLeft > 0)
                Invoke("RestartGame", 1);
            else
            {
                icons[0].texture = deadIcon;
                gameOverPanel.SetActive(true);

                PlayerPrefs.SetInt("lastscore", PlayerPrefs.GetInt("score"));
                
                
                if (PlayerPrefs.HasKey("highscore"))
                {
                    int hs = PlayerPrefs.GetInt("highscore");
                    if (hs < PlayerPrefs.GetInt("score"))
                    {
                        PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
                    }
                    
                }
                else
                {
                    PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
                }

            }
            //@@if(test)
            //@@    OnPlayerDeath();
        } else
            currentPlatform = other.gameObject;
    }

    // Start is called before the first frame update
    void Start() {
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        mRb = magic.GetComponent<Rigidbody>();

        
        if (PlayerPrefs.HasKey("highscore"))
        {
            highScore.text = "High Score: " + PlayerPrefs.GetInt("highscore");
        }
        else
        {
            highScore.text = "High Score: 0";
        }

        

        player = this.gameObject;
        startPosition = player.transform.position;
        GenerateWorld.RunDummy();
        isDead = false;
        livesLeft = PlayerPrefs.GetInt("lives");

        for (int i = 0; i < icons.Length; i++)
        {
            if( i >= livesLeft)
            {
                icons[i].texture = deadIcon;
            }
        }
    }

    void CastMagic() {
        magic.transform.position = magicStartPos.position;
        magic.SetActive(true);
        mRb.AddForce(this.transform.forward * 6000);
        Invoke("KillMagic", 1);
    }

    void KillMagic() {
        mRb.velocity = Vector3.zero;
        magic.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        if (other is BoxCollider && GenerateWorld.lastPlatform.tag != "platformTSection")
            GenerateWorld.RunDummy();

        if (other is SphereCollider)
            canTurn = true;
    }

    void OnTriggerExit(Collider other) {
        if (other is SphereCollider)
            canTurn = false;
    }

    void StopJump() {
        anim.SetBool("isJumping", false);
    }

    void StopMagic() {
        anim.SetBool("isMagic", false);
    }

    // Update is called once per frame
    void Update() {
        if (PlayerController.isDead) return;

        if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("isMagic") == false) {
            anim.SetBool("isJumping", true);
            rb.AddForce(Vector3.up * 200);

        } else if (Input.GetKeyDown(KeyCode.M) && anim.GetBool("isJumping") == false) {
            anim.SetBool("isMagic", true);
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn) {
            this.transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x,
                                                this.transform.position.y,
                                                startPosition.z);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn) {
            this.transform.Rotate(Vector3.up * -90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x,
                                    this.transform.position.y,
                                    startPosition.z);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            this.transform.Translate(-0.5f, 0, 0);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            this.transform.Translate(0.5f, 0, 0);
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float normalSpeed = 20f;
    [SerializeField] float boostSpeed = 40f;
    [SerializeField] float jumpForce = 10f; 
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb2d;
    public GameObject gameObject;
    SurfaceEffector2D surfaceEffector2D;
    bool canMove = true;
    bool isGrounded = false; 
    [SerializeField] Transform groundCheck; 
    [SerializeField] float groundCheckRadius = 1.5f;
    public int score = 0;
    public TextMeshProUGUI textMeshProUGUI;

    //challengemode
    [SerializeField] bool challengeMode = false;
    public int numberOfSpeedEffect = 0;
    [SerializeField] AudioClip bootSpeedClip;
    [SerializeField] public float gameTime = 10f;
    float mapStartTime;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] AudioClip crashSFX;
    public GameObject gameOver;
    private bool isOutOfTime = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        mapStartTime = gameTime;
    }
        
    void Update()
    {
        if (challengeMode)
        {
            if (!isOutOfTime)
            {
                if (gameTime > 0)
                {
                    gameTime -= Time.deltaTime;
                    timeText.SetText("Time: " + Mathf.Ceil(gameTime)); // Cập nhật UI
                }
                else
                {
                    OutOfTime();
                }
            }     
        }
        if (canMove)
        {
            RotatePlayer();
            RespondToBoost();
            Jump(); 
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OutOfTime()
    {
        Debug.Log("Ouch!");
        AudioManager.Instance.StopBackgroundMusic();
        GetComponent<AudioSource>().PlayOneShot(crashSFX);
        Time.timeScale = 0f;
        gameOver.SetActive(true);
        isOutOfTime = true;
    }

    IEnumerator BoostSpeedForSeconds(float duration)
    {
        if(numberOfSpeedEffect >= 1)
            normalSpeed += 5;
        else
            normalSpeed += 10;
        numberOfSpeedEffect += 1;
        yield return new WaitForSeconds(duration); 
        if(numberOfSpeedEffect > 1)
            normalSpeed -= 5;
        else
            normalSpeed -= 10;     
        numberOfSpeedEffect -= 1;
    }

    public void DisableControls()
    {
        canMove = false;
    }

    public void AddScore(int amount)
    {
        score += amount;
        textMeshProUGUI.SetText("Score: " + score);
    }

    void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = normalSpeed;
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (challengeMode)
        {
            if (collision.gameObject.CompareTag("SpeedBoot"))
            {
                Debug.Log("Speed");
                StartCoroutine(BoostSpeedForSeconds(3f));
                Destroy(collision.gameObject);
                AudioManager.Instance.PlaySoundEffect(bootSpeedClip);
            }
        }
        if (collision.gameObject.CompareTag("finish"))
        {
            string mapName = SceneManager.GetActiveScene().name;
            ScoreController.Instance.SetNewHighScore(score: score, fastestTime: mapStartTime - gameTime,sceneName: mapName);
            Time.timeScale = 0f;
            gameObject.SetActive(true);
        }
    }


}

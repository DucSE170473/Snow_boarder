using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    [SerializeField] float groundCheckRadius = 0.2f;
    public int score = 0;
    public TextMeshProUGUI textMeshProUGUI;
    

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    void Update()
    {
        if (score >= 40 && normalSpeed == 20f)
        {
            StartCoroutine(BoostSpeedForSeconds(5f)); 
        } else if(score >= 20 && normalSpeed == 20f)
        {
            StartCoroutine(BoostSpeedForSeconds(3f));
        } 
        if (canMove)
        {
            RotatePlayer();
            RespondToBoost();
            Jump(); 
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    IEnumerator BoostSpeedForSeconds(float duration)
    {
        normalSpeed = 25f; 
        yield return new WaitForSeconds(duration); 
        normalSpeed = 20f; 
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
        if (collision.gameObject.CompareTag("finish"))
        {
            Time.timeScale = 0f;
            gameObject.SetActive(true);
        }
        
    }


}

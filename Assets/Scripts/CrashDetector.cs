using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{

    [SerializeField] float delayTime = 1f;

    [SerializeField] ParticleSystem crashEffect;

    bool hasCrashed = false;

    [SerializeField] AudioClip crashSFX;
    public GameObject gameOver;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag =="Ground" && !hasCrashed)
        {
            hasCrashed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            Debug.Log("Ouch!");
            crashEffect.Play();
            AudioManager.Instance.StopBackgroundMusic();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Time.timeScale = 0f;
            int? score = FindObjectOfType<PlayerController>().score;
            string mapName = SceneManager.GetActiveScene().name;
            ScoreController.Instance.SetNewHighScore(score, mapName);
            gameOver.SetActive(true);
        }
        if (other.tag==("plane")) // N?u ch?m Plane th? ch?t
        {
            hasCrashed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            Debug.Log("Ouch!");
            crashEffect.Play();
            AudioManager.Instance.StopBackgroundMusic();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Time.timeScale = 0f;
            int? score = FindObjectOfType<PlayerController>().score;
            string mapName = SceneManager.GetActiveScene().name;
            ScoreController.Instance.SetNewHighScore(score, mapName);
            gameOver.SetActive(true);
        }
    }

    void ReloadScene()
    {
       SceneManager.LoadScene(0);  
    }
}

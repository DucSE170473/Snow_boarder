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
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Time.timeScale = 0f;
            gameOver.SetActive(true);
        }
        if (other.tag==("plane")) // N?u ch?m Plane th? ch?t
        {
            hasCrashed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            Debug.Log("Ouch!");
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Time.timeScale = 0f;
            gameOver.SetActive(true);
        }
    }

    void ReloadScene()
    {
       SceneManager.LoadScene(0);  
    }
}

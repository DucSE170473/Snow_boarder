using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public GameObject gameObject;
    [SerializeField] float delayTime = 2f;
    [SerializeField] ParticleSystem finishEffect;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            int? score = FindObjectOfType<PlayerController>().score;
            float? fastestTime = FindObjectOfType<PlayerController>().gameTime;
            string mapName = SceneManager.GetActiveScene().name;
            if(mapName != "ChallegeMode")
                 ScoreController.Instance.SetNewHighScore(score: score, fastestTime: fastestTime, sceneName: mapName);
            AudioManager.Instance.StopBackgroundMusic();
            Debug.Log("You Finished!");
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke("ReloadScene", delayTime);
        }
    }

    void ReloadScene() 
    {
        Time.timeScale = 0f;
    }


}

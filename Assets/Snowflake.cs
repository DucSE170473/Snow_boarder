using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Snowflake : MonoBehaviour
{
    [SerializeField] int pointsToAdd = 10;
    public static int destroyedCount = 0;  
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.AddScore(pointsToAdd);
            }
            destroyedCount++; 
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        // H?y ??ng ký s? ki?n khi ??i t??ng này b? h?y
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset destroyedCount khi scene m?i ???c load
        destroyedCount = 0;
    }
   
}

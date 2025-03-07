using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameOver : MonoBehaviour
{
    public GameObject Resume;

    public void GoToMenu()
    {
        Time.timeScale = 1;
        AudioManager.Instance.PlayBackgroundMusic();
        SceneManager.LoadScene("MenuScene");
    }
    public void ResumeGame()
    {
        // Ti?p t?c game
        Time.timeScale = 1;

        // ?n Canvas ch?a các tùy ch?n
        if (Resume != null)
        {
            Resume.SetActive(false);
        }
    }
    public void PauseGame()
    {
        // D?ng game
        Time.timeScale = 0;
        Debug.Log("Game is paused");

        if (Resume != null)
        {
            Resume.SetActive(true);
            Debug.Log("Resume canvas is active");   
        }
        else
        {
            Debug.Log("Resume canvas is not assigned");
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayTheme1BackgroundMusic();
        SceneManager.LoadScene("Level 1");
    }

    public void RestartLevelGame()
    {
        Time.timeScale = 1f; // ??t l?i t?c ?? th?i gian n?u nó ?ã ???c thay ??i
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            AudioManager.Instance.PlayTheme1BackgroundMusic();
        } else if(SceneManager.GetActiveScene().name == "Level 2")
        {
            AudioManager.Instance.PlayTheme2BackgroundMusic();
        } else if(SceneManager.GetActiveScene().name == "Level 3")
        {
            AudioManager.Instance.PlayTheme3BackgroundMusic();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // T?i l?i scene hi?n t?i
    }
    public void Level3()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayTheme3BackgroundMusic();
        SceneManager.LoadScene("Level 3");
    }
    public void MenuLevel()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayBackgroundMusic();
        SceneManager.LoadScene("MenuLevel");
    }
    public void Level2()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayTheme2BackgroundMusic();
        SceneManager.LoadScene("Level 2");
    }
   
    public void Options()
    {
        SceneManager.LoadScene("Option");
    }

    public void Quite()
    {
       Application.Quit();
    }
}

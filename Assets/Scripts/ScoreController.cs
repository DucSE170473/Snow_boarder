using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static ScoreController _instance;
    public static ScoreController Instance { get { return _instance; } }
    public ScoreData highScore { get; private set; }
    string filePath;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        filePath = Path.Combine(Application.dataPath, "highscore.json");
        LoadHighestScore();
    }

    private void LoadHighestScore()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);
            highScore = data;
            Debug.Log("Loaded " + highScore + " score from " + filePath);
        }
        else
        {
            highScore = new ScoreData(0, 0, 0, 0);
        }
    }

    //public void SetNewHighScore(int? highScoreMap1, int? highScoreMap2, int? highScoreMap3)
    //{
    //    if (highScoreMap1 != null)
    //        highScore.highScoreMap1 = (int)highScoreMap1;
    //    if (highScoreMap2 != null)
    //        highScore.highScoreMap2 = (int)highScoreMap2;
    //    if (highScoreMap3 != null)
    //        highScore.highScoreMap3 = (int)highScoreMap3;
    //}

    public void SetNewHighScore(int? score, float? fastestTime, string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
            return;
        if(sceneName == "Level 1")
        {
            if(score > highScore.highScoreMap1)
            {
                highScore.highScoreMap1 = (int)score;
                SaveToFile();
            }
        }
        else if (sceneName == "Level 2")
        {
            if (score > highScore.highScoreMap2)
            {
                highScore.highScoreMap2 = (int)score;
                SaveToFile();
            }
        }
        else if (sceneName == "Level 3")
        {
            if (score > highScore.highScoreMap3)
            {
                highScore.highScoreMap3 = (int)score;
                SaveToFile();
            }
        }
        else if (sceneName == "ChallegeMode")
        {
            if(fastestTime != null)
            {
                if (fastestTime >= highScore.highScoreChallenegeMap || null != highScore.highScoreChallenegeMap)
                {
                    highScore.highScoreChallenegeMap = (float)fastestTime;
                    SaveToFile();
                }
            }
            
        }
    }
    private void SaveToFile()
    {
        string json = JsonUtility.ToJson(highScore, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Saved " + highScore + " to " + filePath);
    }
}

[Serializable]
public class ScoreData
{
    public int highScoreMap1;
    public int highScoreMap2;
    public int highScoreMap3;
    public float highScoreChallenegeMap;
    public ScoreData(int highScoreMap1, int highScoreMap2, int highScoreMap3, float highScoreChallenegeMap)
    {
        this.highScoreMap1 = highScoreMap1;
        this.highScoreMap2 = highScoreMap2;
        this.highScoreMap3 = highScoreMap3;
        this.highScoreChallenegeMap = highScoreChallenegeMap;
    }
}
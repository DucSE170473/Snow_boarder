using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
   public GameObject HighScoreUI;
   public TextMeshProUGUI highScore1;
   public TextMeshProUGUI highScore2;
   public TextMeshProUGUI highScore3;
   public TextMeshProUGUI challengeMode;

    public void ToogleHighScoreUI()
    {
        if (!HighScoreUI.activeSelf)
        {
            ScoreController scoreController = ScoreController.Instance;
            highScore1.text = scoreController.highScore.highScoreMap1.ToString();
            highScore2.text = scoreController.highScore.highScoreMap2.ToString();
            highScore3.text = scoreController.highScore.highScoreMap3.ToString();
            challengeMode.text = scoreController.highScore.highScoreChallenegeMap != null ? scoreController.highScore.highScoreChallenegeMap.ToString() : "";
        }
        HighScoreUI.SetActive(!HighScoreUI.activeSelf);
    }

    public void CloseHighScoreUI()
    {
        HighScoreUI.SetActive(false);
    }
}

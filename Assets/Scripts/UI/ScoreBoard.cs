using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiScoreText;

    void Update()
    {

        if (GameManager.score > GameManager.hi_score)
        {
            GameManager.hi_score = GameManager.score;
            PlayerPrefs.SetInt("HighScore", GameManager.hi_score);
        }

        scoreText.text = "Score: " + GameManager.score.ToString();
        hiScoreText.text = "Hi-Score: " + GameManager.hi_score.ToString();
    }
}
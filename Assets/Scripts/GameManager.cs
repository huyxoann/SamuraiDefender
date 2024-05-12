using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static int hi_score = 0;

    public static void IncreaseScore(int addScore)
    {
        score += addScore;
        if(score<0){
            score = 0;
        }
    }

    public static void ResetScore()
    {
        score = 0;
    }

    void Start()
    {
        if(PlayerPrefs.HasKey("HighScore")){
            hi_score = PlayerPrefs.GetInt("HighScore");
        }
    }
}
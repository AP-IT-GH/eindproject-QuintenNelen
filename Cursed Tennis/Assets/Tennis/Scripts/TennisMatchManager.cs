using TMPro;
using UnityEngine;

public class TennisMatchManager : MonoBehaviour
{
    public int gamesToWinSet = 6;

    private int scorePlayerA;
    private int scorePlayerB;
    private int setsPlayerA;
    private int setsPlayerB;

    private int prevScorePlayerA;
    private int prevScorePlayerB;

    public TextMeshProUGUI scorePlayerAText;
    public TextMeshProUGUI scorePlayerBText;
    public TextMeshProUGUI setsPlayerAText;
    public TextMeshProUGUI setsPlayerBText;
    public TennisAgent playerA;
    public TennisAgent playerB;

    public void StartMatch()
    {
        scorePlayerA = 0;
        scorePlayerB = 0;
        setsPlayerA = 0;
        setsPlayerB = 0;

        UpdateUI();
    }

    public void ScorePoint(bool scoredByPlayerA)
    {
        if (scoredByPlayerA)
        {
            scorePlayerA++;
        }
        else
        {
            scorePlayerB++;
        }

        if (scorePlayerA >= gamesToWinSet)
        {
            // Player A wins the game
            setsPlayerA++;
            prevScorePlayerA = scorePlayerA;
            prevScorePlayerB = scorePlayerB;
            ResetGame();
        }
        else if (scorePlayerB >= gamesToWinSet)
        {
            // Player B wins the game
            setsPlayerB++;
            prevScorePlayerA = scorePlayerA;
            prevScorePlayerB = scorePlayerB;
            ResetGame();
        }

        if (setsPlayerA >= gamesToWinSet || setsPlayerB >= gamesToWinSet)
        {
            // Either Player A or Player B wins the match
            ResetMatch();
        }

        UpdateUI();
    }

    private void ResetGame()
    {
        prevScorePlayerA = scorePlayerA;
        prevScorePlayerB = scorePlayerB;

        scorePlayerA = 0;
        scorePlayerB = 0;
    }

    private void ResetMatch()
    {
        scorePlayerA = 0;
        scorePlayerB = 0;
        setsPlayerA = 0;
        setsPlayerB = 0;
    }

    private void UpdateUI()
    {
        scorePlayerAText.text = FormatScore(scorePlayerA);
        scorePlayerBText.text = FormatScore(scorePlayerB);

        string setsPlayerAText = FormatSets(setsPlayerA);
        string setsPlayerBText = FormatSets(setsPlayerB);

        this.setsPlayerAText.text = setsPlayerAText;
        this.setsPlayerBText.text = setsPlayerBText;

        Debug.Log("Previous Game Score: " + FormatScore(prevScorePlayerA) + " - " + FormatScore(prevScorePlayerB));
    }

    private string FormatScore(int score)
    {
        if (score == 0)
        {
            return "0";
        }
        else if (score == 1)
        {
            return "15";
        }
        else if (score == 2)
        {
            return "30";
        }
        else if (score == 3)
        {
            return "40";
        }
        else if (score == 4)
        {
            return "AD";
        }
        else
        {
            return score.ToString();
        }
    }

    private string FormatSets(int sets)
    {
        if (sets == 6)
        {
            return "6";
        }
        else
        {
            return sets.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTracker : MonoBehaviour {
    public TextMeshProUGUI player1ScoreText, player2ScoreText, winText;
    public GameObject gameOverUI;
    public Color gamePointColor;
    public Color winColor;
    public BallControl ballControl;
    static int player1Score, player2Score;

    private Color baseColor;

    private void Start() {
        baseColor = player1ScoreText.color;
    }

    public void Score(string goalName) {
        if(goalName == "Player 1 Goal") {
            UpdateScore("Right Paddle", ref player2Score, ref player2ScoreText, 1);
        }
        else if(goalName == "Player 2 Goal") {
            UpdateScore("Left Paddle", ref player1Score, ref player1ScoreText, 2);
        }
        else { Debug.Log("Error: Invalid goal name"); }
    }

    private void UpdateScore(string playerName, ref int playerScore, ref TextMeshProUGUI playerScoreText, int ballSide) {
        playerScore++;
        playerScoreText.text = playerScore.ToString();
        if(playerScore == 10) { playerScoreText.color = gamePointColor; }
        Debug.Log($"{playerName} scored. The score is now {player1Score}-{player2Score}");
        if(playerScore >= 11) { // Game Over
            Debug.Log($"Game Over, {playerName} Wins");
            gameOverUI.SetActive(true);
            winText.text = $"{playerName} Wins!";
            playerScoreText.color = winColor;
            ballControl.PauseBall();
            PowerUpManager.StopSpawningPowerups();
            PowerUpManager.DestroyAllPowerups();
        } else {ballControl.LaunchBall(ballSide);}
    }

    public void Restart() {
        player1Score = 0;
        player2Score = 0;
        gameOverUI.SetActive(false);
        player1ScoreText.color = baseColor;
        player2ScoreText.color = baseColor;
        player1ScoreText.text = "0";
        player2ScoreText.text = "0";
        ballControl.LaunchBall(0);
        PowerUpManager.StartSpawningPowerups();
    }
}

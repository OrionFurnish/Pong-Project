using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour {
    static int player1Score, player2Score;

    public static void Score(string goalName, BallControl ball) {
        if(goalName == "Player 1 Goal") {
            UpdateScore("Right Paddle", ref player2Score);
            ball.LaunchBall(1);
        }
        else if(goalName == "Player 2 Goal") {
            UpdateScore("Left Paddle", ref player1Score);
            ball.LaunchBall(2);
        }
        else { Debug.Log("Error: Invalid goal name"); }
    }

    static void UpdateScore(string playerName, ref int playerScore) {
        playerScore++;
        Debug.Log($"{playerName} scored. The score is now {player1Score}-{player2Score}");
        if(playerScore >= 11) { // Game Over
            Debug.Log($"Game Over, {playerName} Wins");
            player1Score = 0;
            player2Score = 0;
        }
    }
}

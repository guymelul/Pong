using System;
using System.Collections;
using UnityEngine;

public class PongGameSession : MonoBehaviour
{
    public GameObject ball;
    public GameObject ballSpawn;
    private int player1Score;
    private int player2Score;

    void Start()
    {
        Debug.Log("Match starting");
        StartCoroutine(Reset());
    }

    public IEnumerator Reset()
    {
        player1Score = 0;
        player2Score = 0;
        yield return new WaitForSeconds(1);
        SpawnBall();
    }

    public GameObject SpawnBall()
    {
        return Instantiate(ball, ballSpawn.transform.position, Quaternion.identity);
    }

    public void onGoal(int playerNum)
    {
        if (playerNum == 1)
        {
            player2Score++;
        }
        else if(playerNum == 2)
        {
            player1Score++;
        } else
        {
            Debug.LogError("Invalid player number: " + playerNum);
            return;
        }

        Debug.Log("Score: " + player1Score + " : " + player2Score);
    }
}

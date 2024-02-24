using System.Collections;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class PongGameSession : MonoBehaviour
{
    public GameObject ball;
    public GameObject ballSpawn;
    public IntVariable liveBallCount;

    public IntEvent player1ScoreChangedEvent;
    public IntEvent player2ScoreChangedEvent;
    private int player1Score;
    private int player2Score;

    void Start()
    {
        Debug.Log("Match starting");
        Reset();
    }

    public void Reset()
    {
        player1Score = 0;
        player2Score = 0;
        player1ScoreChangedEvent.Raise(player1Score);
        player2ScoreChangedEvent.Raise(player2Score);

        liveBallCount.Reset();

        StartCoroutine(SpawnBall());
    }

    public IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(1);

        Instantiate(ball, ballSpawn.transform.position, Quaternion.identity);
        liveBallCount.Add(1);
    }

    public void onGoal(int goalOwner)
    {
        if (goalOwner == 1)
        {
            player2Score++;
            player2ScoreChangedEvent.Raise(player2Score);
        }
        else if(goalOwner == 2)
        {
            player1Score++;
            player1ScoreChangedEvent.Raise(player1Score);
        } else
        {
            Debug.LogError("Invalid goal owner number: " + goalOwner);
            return;
        }

        liveBallCount.Add(-1);

        if (liveBallCount.Value == 0)
        {
            StartCoroutine(SpawnBall());
        }
    }
}

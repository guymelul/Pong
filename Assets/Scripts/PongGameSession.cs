using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongGameSession : MonoBehaviour
{
    private int player1Score;
    private int player2Score;
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        player1Score = 0;
        player2Score = 0;
    }

    public void onGoal(int playerNum)
    {
        if (playerNum == 1) 
        {
            player1Score++;
        }
        else if(playerNum == 2)
        {
            player2Score++;
        } else
        {
            Debug.LogError("Invalid player number " + playerNum);
            return;
        }

        Debug.Log("Score: " + player1Score + " : " + player2Score);
    }
}

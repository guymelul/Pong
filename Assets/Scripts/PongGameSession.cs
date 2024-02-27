using System.Collections;
using UnityAtoms.BaseAtoms;
using UnityAtoms.PongGame;
using UnityEngine;
using UnityEngine.InputSystem;

public class PongGameSession : MonoBehaviour
{
    public GameSessionDataVariable gameSession;

    public GameObject ballPrefab;
    public GameObject paddlePrefab;
    public GameObject ballSpawn;
    public IntVariable liveBallCount;

    public GameObject player1Swimlane;
    public GameObject player2Swimlane;
    public IntEvent player1ScoreChangedEvent;
    public IntEvent player2ScoreChangedEvent;
    private int player1Score;
    private int player2Score;

    private PlayerInputManager playerInputManager;

    private GameObject[] players;

    void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = paddlePrefab;

        players = new GameObject[playerInputManager.maxPlayerCount];

        ResetMatch();
    }


    public void ResetMatch()
    {
        Debug.Log("Prepare match");

        foreach (var player in players)
        {
            if (player != null) { Destroy(player); }
        }

        playerInputManager.EnableJoining();

        player1Score = 0;
        player2Score = 0;
        player1ScoreChangedEvent.Raise(player1Score);
        player2ScoreChangedEvent.Raise(player2Score);

        liveBallCount.Reset();

        // Spawn players
        SpawnPlayer(
            player1Swimlane.transform.position,
            gameSession.Value.Player1.PaddleSprite,
            "Player1",
            gameSession.Value.HumanPlayerCount >= 1
        );

        SpawnPlayer(
            player2Swimlane.transform.position,
            gameSession.Value.Player2.PaddleSprite,
            "Player2",
            gameSession.Value.HumanPlayerCount >= 2
        );

        StartCoroutine(SpawnBall());

        Debug.Log("Match starting");

        playerInputManager.DisableJoining();
    }

    private void SpawnPlayer(Vector2 position, Sprite paddleSprite, string actionMap, bool isHuman)
    {

        PlayerInput playerInput = playerInputManager.JoinPlayer(-1, -1, null, Keyboard.current);

        if (playerInput != null)
        {
            GameObject player = playerInput.gameObject;
            player.transform.position = position;
            playerInput.SwitchCurrentActionMap(actionMap);

            SpriteRenderer spriteRender;
            if (player.TryGetComponent<SpriteRenderer>(out spriteRender))
            {
                spriteRender.sprite = paddleSprite;
            }

            player.GetComponent<PaddleAIController>().enabled = !isHuman;
            player.GetComponent<PaddleHumanController>().enabled = isHuman;
        }
    }

    public IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(1);

        Instantiate(ballPrefab, ballSpawn.transform.position, Quaternion.identity);
        liveBallCount.Add(1);
    }

    public void onGoal(int goalOwner)
    {
        if (goalOwner == 1)
        {
            player2Score++;
            player2ScoreChangedEvent.Raise(player2Score);
        }
        else if (goalOwner == 2)
        {
            player1Score++;
            player1ScoreChangedEvent.Raise(player1Score);
        }
        else
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

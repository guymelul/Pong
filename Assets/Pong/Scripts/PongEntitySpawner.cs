using System.Collections;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MalulsArcade.Pong
{
    public class PongEntitySpawner : MonoBehaviour
    {
        public PongSessionData gameSession;

        public GameObject ballPrefab;
        public GameObject paddlePrefab;

        public GameObject ballSpawn;
        public GameObject player1Swimlane;
        public GameObject player2Swimlane;

        public GameObjectValueList liveBalls;
        private GameObject[] players;

        private PlayerInputManager playerInputManager;

        void Start()
        {
            playerInputManager = GetComponent<PlayerInputManager>();
            playerInputManager.playerPrefab = paddlePrefab;

            players = new GameObject[playerInputManager.maxPlayerCount];

            liveBalls.Removed.Register(onBallRemoved);
        }

        public void Setup()
        {
            liveBalls.Clear();

            playerInputManager.EnableJoining();

            foreach (var player in players)
            {
                if (player != null) { Destroy(player); }
            }

            // Spawn players
            players[0] = SpawnPlayer(
                player1Swimlane,
                gameSession.Player1.PaddleSprite,
                "Player1",
                gameSession.HumanPlayerCount >= 1
            );

            players[1] = SpawnPlayer(
                player2Swimlane,
                gameSession.Player2.PaddleSprite,
                "Player2",
                gameSession.HumanPlayerCount >= 2
            );

            playerInputManager.DisableJoining();
        }

        private GameObject SpawnPlayer(GameObject spawnPoint, Sprite paddleSprite, string actionMap, bool isHuman)
        {
            PlayerInput playerInput = playerInputManager.JoinPlayer(-1, -1, null, Keyboard.current);

            if (playerInput == null)
            {
                return null;
            }

            GameObject player = playerInput.gameObject;
            player.transform.position = spawnPoint.transform.position;
            player.transform.localScale = spawnPoint.transform.localScale;
            playerInput.SwitchCurrentActionMap(actionMap);

            player.GetComponent<SpriteRenderer>().sprite = paddleSprite;
            player.GetComponent<PaddleAIController>().enabled = !isHuman;
            player.GetComponent<PaddleHumanController>().enabled = isHuman;

            return player;
        }


        private GameObject CreateBall()
        {
            GameObject ball = Instantiate(ballPrefab, ballSpawn.transform.position, Quaternion.identity);
            ball.transform.localScale = ballSpawn.transform.localScale;

            liveBalls.Add(ball);

            return ball;
        }

        private void onBallRemoved(GameObject ball)
        {
            Destroy(ball);
        }

        public int GetLiveBallCount()
        {
            return liveBalls.Count;
        }

        public IEnumerator SpawnBall(float timeToWait = 0.5f)
        {
            GameObject ball = CreateBall();
            yield return new WaitForSeconds(timeToWait);
            ball.GetComponent<BallController>().Launch();
        }
    }
}
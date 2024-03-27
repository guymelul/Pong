using System.Collections;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace MalulsArcade.Pong
{
    [RequireComponent(typeof(PongEntitySpawner))]
    public class PongGameSession : MonoBehaviour
    {
        // Components
        private PongEntitySpawner entitySpawner;

        // Data
        public PongSessionData gameSession;
        public IntVariable player1Score;
        public IntVariable player2Score;

        // Events
        public VoidEvent matchPrepared;
        public IntEvent matchEndedEvent;

        void Start()
        {
            entitySpawner = GetComponent<PongEntitySpawner>();

            StartCoroutine(PrepareMatch());
        }

        public IEnumerator PrepareMatch()
        {
            // Start after all components are loaded
            yield return new WaitForFixedUpdate();

            entitySpawner.Setup();

            player1Score.SetValue(0);
            player2Score.SetValue(0);

            matchPrepared.Raise();
        }

        public void onStartMatch()
        {
            StartCoroutine(entitySpawner.SpawnBall(0.5f));
        }

        public void onGoal(int goalOwner)
        {
            if (goalOwner == 1)
            {
                player2Score.Add(1);

                if (player2Score.Value >= gameSession.WinScore)
                {
                    matchEndedEvent.Raise(2);
                    return;
                }
            }
            else if (goalOwner == 2)
            {
                player1Score.Add(1);

                if (player1Score.Value >= gameSession.WinScore)
                {
                    matchEndedEvent.Raise(1);
                    return;
                }
            }
            else
            {
                Debug.LogError("Invalid goal owner number: " + goalOwner);
                return;
            }

            if (entitySpawner.GetLiveBallCount() == 0)
            {
                StartCoroutine(entitySpawner.SpawnBall());
            }
        }
    }
}
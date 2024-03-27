using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace MalulsArcade.Pong
{
    [RequireComponent(typeof(Collider2D))]
    public class Goal : MonoBehaviour
    {
        [Range(1, 2)]
        [SerializeField]
        private int playerGoal;

        public IntEvent onGoalEvent;

        public GameObject particleEffect;

        public GameObjectValueList liveBalls;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                liveBalls.Remove(collision.gameObject);

                if (particleEffect != null)
                {
                    Vector2 effectPos = collision.ClosestPoint(collision.transform.position);
                    Instantiate(particleEffect, effectPos, Quaternion.identity);
                }

                onGoalEvent.Raise(playerGoal);
            }
        }
    }
}
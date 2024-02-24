using UnityEngine;
using UnityAtoms.BaseAtoms;

[RequireComponent(typeof(Collider2D))]
public class Goal : MonoBehaviour
{
    [Range(1, 2)]
    [SerializeField]
    private int playerGoal;

    public IntEvent onGoalEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<BallController>(out _))
        {
            onGoalEvent.Raise(playerGoal);
            Destroy(collision.gameObject);
        }
    }
}

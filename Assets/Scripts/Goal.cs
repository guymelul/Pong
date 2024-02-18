using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Goal : MonoBehaviour
{
    public UnityEvent OnGoal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallController ball = collision.gameObject.GetComponent<BallController>();
        if (ball != null)
        {
            OnGoal.Invoke();
            StartCoroutine(ball.ResetMovement());
        }
    }
}

using System.Collections;
using UnityAtoms.BaseAtoms;
using UnityEngine;

[RequireComponent(typeof(PaddleController))]
public class PaddleAIController : MonoBehaviour
{
    private PaddleController paddleController;
    private Transform paddleTransform;
    private GameObject followTarget;

    public GameObjectValueList liveBalls;

    private bool isBusy;

    private Vector2 startingPoint;

    private float targetY;

    void Start()
    {
        paddleController = GetComponent<PaddleController>();
        paddleTransform = paddleController.transform;
        followTarget = null;
        isBusy = false;

        startingPoint = transform.position;
    }

    void FixedUpdate()
    {
        // Pick a target and follow it
        if (followTarget == null) {
            targetY = startingPoint.y;

            StartCoroutine(PickNextTarget());
        } else
        {
            targetY = followTarget.transform.position.y;
        }

        // Take to opposite relative y position
        float distance = paddleTransform.position.y - targetY;

        // if distance is small then stop
        if (Mathf.Abs(distance) < 0.5f)
        {
            paddleController.UpdateMovementDirection(0);
            if (!isBusy)
                StartCoroutine(PickNextTarget());

            return;
        }

        float direction = -1 * Mathf.Sign(distance);

        paddleController.UpdateMovementDirection(direction);
    }

    private IEnumerator PickNextTarget()
    {
        if (liveBalls.Count == 0) { yield return null; }

        isBusy = true;
        yield return new WaitForSeconds(0.05f);

        // TODO: look at closest ball from liveBall list
        GameObject closestBall = null;
        float closestBallDistance = float.MaxValue;

        foreach (GameObject ball in liveBalls.List)
        {
            float ballDistanceFromPaddle = Mathf.Abs(Vector2.Distance(ball.transform.position, transform.position));

            float paddleSide = Mathf.Sign(transform.position.x - ball.transform.position.x);

            float ballDir = Mathf.Sign(ball.GetComponent<Rigidbody2D>().velocity.x);

            // Is the ball heading my way?
            if (paddleSide != ballDir)
            {
                continue;
            }

            if (ballDistanceFromPaddle < closestBallDistance)
            {
                closestBall = ball;
                closestBallDistance = ballDistanceFromPaddle;
            }
        }

        followTarget = closestBall;

        isBusy = false;
    }
}

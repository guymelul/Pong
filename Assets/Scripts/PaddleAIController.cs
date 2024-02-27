using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PaddleController))]
public class PaddleAIController : MonoBehaviour
{
    private PaddleController paddleController;
    private Transform paddleTransform;
    private float targetY;

    private bool isBusy;

    void Start()
    {
        paddleController = GetComponent<PaddleController>();
        paddleTransform = paddleController.transform;
        targetY = 4;
        isBusy = false;
    }

    void OnEnable()
    {
        Debug.Log("AI", gameObject);
    }

    void FixedUpdate()
    {
        // Take to opposite relative y position
        float distance = paddleTransform.position.y - targetY;

        // if distance is small stop
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
        isBusy = true;
        yield return new WaitForSeconds(0.4f);
        targetY = Random.Range(-5, 5);
        isBusy = false;
    }
}

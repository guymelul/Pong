using UnityAtoms.BaseAtoms;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleController : MonoBehaviour
{
    public float speed = 5.0f;
    public VoidEvent paddleHitEvent;

    private Rigidbody2D rb;
    private float rayDistance;
    private LayerMask boundsLayer;
    private Vector2 movementDir = Vector2.zero;

    float sliceSize;
    private int[] highAngle = new int[] { 45, 50, 55, 60 };
    private int[] lowAngle = new int[] { 45, 40, 35, 30 };

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float colliderHeight = GetComponent<Collider2D>().bounds.size.y;

        rayDistance = colliderHeight / 2.0f;
        boundsLayer = LayerMask.GetMask("Bounds");

        sliceSize = colliderHeight / ((highAngle.Length - 1) * 2);
    }

    void FixedUpdate()
    {
        if (movementDir.magnitude < 0)
            return;

        Vector2 posDelta = movementDir * speed * Time.fixedDeltaTime;
        RaycastHit2D hit = Physics2D.Raycast(rb.position, movementDir, rayDistance + Mathf.Abs(posDelta.y), boundsLayer);

        if (hit.collider == null)
        {
            rb.MovePosition(rb.position + posDelta);
        }
        else
        {
            Vector2 correctionPos = hit.point + -movementDir * (rayDistance + 0.1f);

            correctionPos.y = Mathf.Min(correctionPos.y, rb.position.y);
            // Close to wall position correction
            rb.MovePosition(correctionPos);
        }
    }

    public Vector2 GetCurrentPosition()
    {
        return rb.position;
    }

    public void UpdateMovementDirection(float y)
    {
        movementDir.y = y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Ball"))
            return;

        paddleHitEvent.Raise();

        DynamicBallController ballController = collision.gameObject.GetComponent<DynamicBallController>();

        if (ballController != null)
        {
            ApplyCustomBallCollisionResponse(ballController, collision);
        }
    }

    private void ApplyCustomBallCollisionResponse(DynamicBallController ballController, Collision2D collision)
    {
        Rigidbody2D ballRb = collision.rigidbody;
        ContactPoint2D contact = collision.GetContact(0);

        float targetAngle;
        float diff = contact.point.y - rb.position.y;

        Vector2 direction = ballRb.velocity;

        int angleSliceIdx = (int)(Mathf.Abs(diff) / sliceSize);

        if (Mathf.Sign(direction.y) == Mathf.Sign(diff))
        {
            targetAngle = highAngle[angleSliceIdx];
        }
        else
        {
            targetAngle = lowAngle[angleSliceIdx];
        }

        Vector2 newDirection = VectorMathHelper.AngleToDirVector(targetAngle);
        newDirection.y = newDirection.y * Mathf.Sign(direction.y);
        newDirection.x = newDirection.x * Mathf.Sign(direction.x);

        ballRb.velocity = Vector2.zero;
        ballRb.AddForce(newDirection * ballController.speed);
    }
}

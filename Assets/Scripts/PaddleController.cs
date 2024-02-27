using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleController : MonoBehaviour
{
    public float speed = 5.0f;

    private Rigidbody2D rb;
    private float rayDistance;
    private LayerMask boundsLayer;
    private Vector2 movementDir = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rayDistance = GetComponent<Collider2D>().bounds.size.y / 2.0f;
        boundsLayer = LayerMask.GetMask("Bounds");
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
            // Close to wall position correction
            rb.MovePosition(hit.point + -movementDir * (rayDistance + 0.2f));
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

}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DynamicBallController : MonoBehaviour
{
    public float speed = 1.0f;
    public float speedAccelerationFactor = 1.1f;
    [Range(0, 359)]
    public int angle = 45;
    public Vector2 direction;

    private bool didCollisionStart;
    private Vector2 lastPosition;
    private Vector2 lastDirection;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

        lastDirection = direction;
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastPosition = transform.position;
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // bool isPlayer = collision.collider.CompareTag("Player");
        Vector2 contactNormal = collision.GetContact(0).normal;

        lastDirection = direction;
        direction = Vector2.Reflect(direction, contactNormal);

        // TODO: add fun factor (when is player)
        // depends on paddle collide position
        if (contactNormal.x != 0.0f && contactNormal.y != 0.0f)
        { 
            Debug.Log("Normal: " + contactNormal);
            Debug.Log("Angle: " + angle);
        }

        angle = (int) Vector2.Angle(direction, Vector2.up);

        didCollisionStart = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (didCollisionStart) {
            didCollisionStart = false;
            return;
        }

        Vector2 diff = rb.position - lastPosition;

        if (diff.x > 0 || diff.y > 0)
        {
            direction.x = -lastDirection.x;
            direction.y = -lastDirection.y;
        }

        rb.velocity = direction*speed;

        angle = (int)Vector2.Angle(direction, Vector2.up);

        Debug.Log("Stay Angle: " + angle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)).normalized);
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DynamicBallController : MonoBehaviour
{
    [Range(200, 3000)]
    public float speed = 300;
    [Range(1.0f, 1.5f)]
    public float speedAccelerationFactor = 1.05f;
    [Range(0, 359)]
    public float angle = 45;

    private float ballFreakoutThreshold = 30.0f;

    private Rigidbody2D rb;

    private float addForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        addForce = 0;
    }

    public void Launch()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(VectorMathHelper.AngleToDirVector(angle) * speed);
    }

    void FixedUpdate()
    {
        float ballMagnitude = rb.velocity.magnitude;

        if (ballMagnitude > 0.1f)
        {
            angle = Vector2.SignedAngle(Vector2.zero, rb.velocity.normalized);

            if (addForce > 0)
            {
                rb.AddForce(VectorMathHelper.AngleToDirVector(angle) * addForce);
                addForce = 0;
            }

            if (ballMagnitude >= ballFreakoutThreshold)
            {
                Debug.Log("Ball goes mad: " + rb.velocity);
                Launch();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);

        if (!(collision.gameObject.CompareTag("Player") &&
             contact.normal.y == 0.0f))
            return;

        addForce = speed * (speedAccelerationFactor - 1);

        speed = speed * speedAccelerationFactor;
    }

    private void OnDrawGizmos()
    {
        if (rb != null)
        {
            Gizmos.DrawRay(transform.position, rb.velocity.normalized);
        }
        else
        {
            Gizmos.DrawRay(transform.position, VectorMathHelper.AngleToDirVector(angle) * 5.0f);
        }
    }
}

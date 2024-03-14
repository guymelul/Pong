using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DynamicBallController : MonoBehaviour
{
    [Range(200, 3000)]
    public float speed = 300;
    [Range(1.0f, 1.5f)]
    public float speedAccelerationFactor = 1.05f;
    [Range(0, 359)]
    public int angle = 45;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(VectorMathHelper.AngleToDirVector(angle) * speed);
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > 1000)
        {
            Debug.Log("V: " + rb.velocity);
            // TODO: lower speed if exceeded
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);

        if (!(collision.gameObject.CompareTag("Player") &&
             contact.normal.y == 0.0f))
            return;

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

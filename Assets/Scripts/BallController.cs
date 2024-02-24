using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BallController : MonoBehaviour
{
    public float startingSpeed = 1.0f;
    public float speedAccelerationFactor = 1.1f;

    private static float hitThreshold = 0.1f;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2(startingSpeed, startingSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movement.magnitude < 0.01f)
            return;

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isPlayer = collision.collider.CompareTag("Player");
        ContactPoint2D contact = collision.GetContact(0);
        bool hitY = Mathf.Abs(contact.normal.y) > hitThreshold;
        bool hitX = Mathf.Abs(contact.normal.x) > hitThreshold;
            
        // Check corner
        if (hitX && hitY)
        {
            movement.y *= Mathf.Sign(contact.point.y);
            movement.x *= -1;

            // TODO: particle and sound
            return;
        }

        if (isPlayer)
        {
            movement = movement * speedAccelerationFactor;
        }

        // Make ball not bounce off top and bottom of paddle
        if (hitY && !isPlayer)
        {
            movement.y *= -1;
        }
        else if (hitX)
        {
            movement.x *= -1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BallController : MonoBehaviour
{
    private static float hitThreshold = 0.1f;
    private Rigidbody2D rb;
    public float startingSpeed = 1.0f;
    public float speedAccelerationFactor = 1.1f;
    private Vector2 movement;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = Vector2.zero;
    }

    private void Start()
    {
        StartMovement();
    }

    void StartMovement()
    {
        movement = new Vector2(startingSpeed, startingSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movement.magnitude < 0.05f)
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

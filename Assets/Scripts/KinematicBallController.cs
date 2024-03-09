using System;
using System.Diagnostics.Contracts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class KinematicBallController : MonoBehaviour
{
    [Range(0.1f, 30.0f)]
    public float speed = 1.0f;
    [Range(1.0f, 3.0f)]
    public float speedAccelerationFactor = 1.1f;

    private Rigidbody2D rb;
    private Vector2 direction;

    private Vector2 lastPosition;

    private ContactPoint2D[] lastContacts = new ContactPoint2D[4];

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        int startingAngle = 45;
        direction = new Vector2(Mathf.Sin(startingAngle * Mathf.Deg2Rad), Mathf.Cos(startingAngle * Mathf.Deg2Rad)).normalized;
        
        lastPosition = rb.position;
    }

    void FixedUpdate()
    {
        float distance = speed * Time.fixedDeltaTime;
        Vector2 newPos = rb.position + direction * distance;

        lastPosition = rb.position;
        rb.MovePosition(newPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ResolveCollision(collision);

        // For Gizmo
        for (int i = 0; i < collision.contactCount; ++i)
        {
            lastContacts[i] = collision.GetContact(i);
        }
    }

    private void ResolveCollision(Collision2D collision)
    {
        rb.position = lastPosition;

        Vector2 contactNormal = collision.GetContact(0).normal;

        if (collision.contactCount == 2)
        {
            ContactPoint2D contact1 = collision.GetContact(0);
            ContactPoint2D contact2 = collision.GetContact(1);

            Vector2 mid = (contact1.point + contact2.point) / 2;

            Vector2 reflectSurface = (lastPosition - mid);

            contactNormal = reflectSurface.normalized;
        }

        if (collision.contactCount > 2)
        {
            Debug.Log("Wierd collision");
            Time.timeScale = 0.0f;
        }

        direction = Vector2.Reflect(direction, contactNormal).normalized;

        bool isPlayer = collision.collider.CompareTag("Player");

        if(isPlayer) {
            speed = speed * speedAccelerationFactor;
        }
    }

    private void OnDrawGizmos()
    {
        for (int i=0; i < lastContacts.Length; ++i)
        {
            if (lastContacts[i].enabled)
            {
                Gizmos.DrawSphere(lastContacts[i].point, 0.08f + 0.02f * i);
            } else
            {
                break;
            }
        }

        Gizmos.DrawSphere(lastPosition, 0.32f);
    }
}

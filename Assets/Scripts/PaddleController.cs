using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleController : MonoBehaviour
{
    public InputAction moveAction;

    private Rigidbody2D rb;
    private float rayDistance;
    private LayerMask boundsLayer;

    public float speed = 5.0f;
    private Vector2 movementDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rayDistance = GetComponent<Collider2D>().bounds.size.y / 2.0f;
        boundsLayer = LayerMask.GetMask("Bounds");
    }

    private void Start()
    {
        moveAction.Enable();
    }

    void Update()
    {
        movementDir = new Vector2(0, moveAction.ReadValue<float>()).normalized;
    }

    private void FixedUpdate()
    {
        if (movementDir.magnitude < 0)
            return;

        Vector2 posDelta = movementDir * speed * Time.fixedDeltaTime;
        Debug.DrawRay(transform.position, movementDir * rayDistance + posDelta, Color.yellow, 0.0f, false);
        RaycastHit2D hit = Physics2D.Raycast(rb.position, movementDir, rayDistance + Mathf.Abs(posDelta.y), boundsLayer);

        if (hit.collider == null)
        {
            rb.MovePosition(rb.position + posDelta);
        } else 
        {
            // Close to wall position correction
            rb.MovePosition(hit.point + -movementDir * (rayDistance + 0.2f));
        }
    }
}

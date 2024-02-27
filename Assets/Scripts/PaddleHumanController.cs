using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PaddleController))]
public class PaddleHumanController : MonoBehaviour
{
    private PaddleController paddleController;

    void Start()
    {
        paddleController = GetComponent<PaddleController>();
    }

    void OnEnable()
    {
        Debug.Log("Human", gameObject);
    }

    // Event from PlayerInput
    public void OnMove(InputValue value)
    {
        paddleController.UpdateMovementDirection(value.Get<float>());
    }
}
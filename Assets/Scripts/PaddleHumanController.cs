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

    // Event from PlayerInput
    public void OnMove(InputValue value)
    {
        if (enabled)
            paddleController.UpdateMovementDirection(value.Get<float>());
    }
}

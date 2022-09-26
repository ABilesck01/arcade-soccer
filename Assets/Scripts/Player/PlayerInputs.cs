using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public Vector2 movementInput;
    public bool passBall;
    public bool kickBall;
    public bool dash;

    public void GetMovementInputs(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void GetPassInputs(InputAction.CallbackContext context)
    {
        passBall = context.action.triggered;
    }

    public void GetKickInputs(InputAction.CallbackContext context)
    {
        kickBall = context.action.triggered;
    }

    public void GetDashInputs(InputAction.CallbackContext context)
    {
        dash = context.action.phase == InputActionPhase.Performed;
    }
}

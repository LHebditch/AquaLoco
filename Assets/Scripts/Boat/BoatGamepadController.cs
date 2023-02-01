using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatGamepadController : BoatInputDevice
{

    private Gamepad input;

    private void Start()
    {
        input = Gamepad.current;
        if(input == null)
        {
            Debug.LogWarning("No gamepad device detected");
        }
    }
    #region movement
    public override int GetMovement()
    {
        if (input == null) return 0;

        if (input.rightShoulder.isPressed)
        {
            return 1;
        }

        if (input.leftShoulder.isPressed)
        {
            return -1;
        }

        return 0;
    }

    public override Vector3 GetRotation()
    {
        if (input == null) return Vector3.zero;

        Vector2 move = input.leftStick.ReadValue();
        return Vector3.up * move.x;
    }

    public override Vector3 GetBank()
    {
        if (input == null) return Vector3.zero;

        Vector2 move = input.leftStick.ReadValue();
        return -(Vector3.forward) * move.x;
    }
    #endregion
}

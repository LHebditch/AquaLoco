using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatKeyboardController : BoatInputDevice
{
    private Keyboard input;

    private enum Directions
    {
        FORWARD,
        REVERSE,
        LEFT,
        RIGHT,
    }

    private Dictionary<Directions, Key> keyboardControls = new Dictionary<Directions, Key>
    {
        { Directions.FORWARD, Key.W },
        { Directions.REVERSE, Key.S },
        { Directions.LEFT, Key.A },
        { Directions.RIGHT, Key.D },
    };

    private void Start()
    {
        input = Keyboard.current;
        if (input == null)
        {
            Debug.LogWarning("No gamepad keyboard detected");
        }
    }

    #region Movement
    public override int GetMovement()
    {
        if (input[keyboardControls[Directions.FORWARD]].isPressed )
        {
            return 1;
        }

        if (input[keyboardControls[Directions.REVERSE]].isPressed)
        {
            return -1;
        }
        return 0;
    }

    public override Vector3 GetRotation()
    {
        Vector3 rotation = Vector3.zero;
        if (input[keyboardControls[Directions.LEFT]].isPressed)
        {
            rotation -= Vector3.up;
        }

        if (input[keyboardControls[Directions.RIGHT]].isPressed)
        {
            rotation += Vector3.up;
        }

        return rotation;
    }

    public override Vector3 GetBank()
    {
        Vector3 bank = Vector3.zero;
        if (input[keyboardControls[Directions.LEFT]].isPressed)
        {
            bank += Vector3.forward;
        }

        if (input[keyboardControls[Directions.RIGHT]].isPressed)
        {
            bank -= Vector3.forward;
        }
        return bank;
    }
    #endregion

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;


public class BoatKeyboardController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoatGearBox gearBox;
    [SerializeField] private float gearShiftDelay = 0.25f;
    [SerializeField] private float acceleration = 0.05f;
    [SerializeField] private float turnSpeed = 0.1f;
    [SerializeField] private float bankSpeed = 1f;
    [SerializeField] private float maxBank = 20f;
    [SerializeField] private Transform boatModel;

    private float runTime = 0;
    private float gearLastShifted = 0;
    private float currentSpeed = 0;

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

    private void Update()
    {
        runTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {

        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            HandleInput(keyboard);
            return;
        }

        throw new MissingComponentException("We got no fucking controls :0");

    }

    private void HandleInput(Keyboard input)
    {
        MoveBoat(input);
        TurnBoat(input);
    }

    #region Movement
    private void MoveBoat(Keyboard input)
    {
        bool canShift = runTime >= (gearLastShifted + gearShiftDelay);

        if (input[keyboardControls[Directions.FORWARD]].isPressed && canShift)
        {
            gearBox.ShiftGear(1);
            gearLastShifted = runTime;
        }

        if (input[keyboardControls[Directions.REVERSE]].isPressed && canShift)
        {
            gearBox.ShiftGear(-1);
            gearLastShifted = runTime;
        }

        Vector3 vel = UpdatedSpeed();
        vel.y = rb.velocity.y; // preserver gravity
        rb.velocity = vel;
    }

    private void TurnBoat(Keyboard input)
    {
        Vector3 rotation = Vector3.zero;
        Vector3 bank = Vector3.zero;
        if (input[keyboardControls[Directions.LEFT]].isPressed)
        {
            rotation -= Vector3.up;
            bank += Vector3.forward;
        }

        if (input[keyboardControls[Directions.RIGHT]].isPressed)
        {
            rotation += Vector3.up;
            bank -= Vector3.forward;
        }


        if(rotation == Vector3.zero)
        {
            // reset bank
            if(boatModel.localRotation.z < 0)
            {
                bank += (Vector3.forward * 0.5f);
            } else if(boatModel.localRotation.z > 0)
            {
                bank -= (Vector3.forward * 0.5f);
            }

            if(Mathf.Abs(boatModel.localRotation.z) < 0.01f)
            {
                Quaternion t = boatModel.localRotation;
                t.z = 0;
                bank = Vector3.zero;
                boatModel.localRotation = t;
            }
        }

        // apply banking 
        transform.RotateAround(transform.position, rotation, turnSpeed);
        boatModel.Rotate(bank * bankSpeed);
        Quaternion existingRot = boatModel.localRotation;
        existingRot.z = Mathf.Clamp(existingRot.z, -maxBank, maxBank);
        boatModel.localRotation = existingRot;
    }
    #endregion

    #region utils
    public Vector3 UpdatedSpeed()
    {
        BoatGear currentGear = gearBox.GetCurrentGear();
        BoatGear lastGear = gearBox.GetLastGear();
        float dir = lastGear.gear < currentGear.gear ? 1 : -1;
        float newSpeed = currentSpeed + (acceleration * dir);
        float diff = Mathf.Abs(newSpeed - currentGear.speed);

        if (diff > 0.05f && !gearBox.IsMaxSpeed(newSpeed))
        {
            currentSpeed = newSpeed;
        }

        if(gearBox.IsMaxSpeed(currentSpeed))
        {
            currentSpeed = gearBox.maxSpeed;
        }

        return transform.forward * currentSpeed;
    }
    #endregion
}

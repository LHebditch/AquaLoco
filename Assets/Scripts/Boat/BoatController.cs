using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoatInputDevice : MonoBehaviour {
    public abstract Vector3 GetRotation();
    public abstract Vector3 GetBank();
    public abstract int GetMovement();
}

public class BoatController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoatGearBox gearBox;
    [SerializeField] private float gearShiftDelay = 0.25f;
    [SerializeField] private float acceleration = 0.05f;
    [SerializeField] private float turnSpeed = 1f;
    [SerializeField] private float bankSpeed = 0.8f;
    [SerializeField] private float maxBank = 0.3f;
    [SerializeField] private Transform boatModel;
    [SerializeField] BoatInputDevice inputDevice;

    private float runTime = 0;
    private float gearLastShifted = 0;
    private float currentSpeed = 0;


    private void Update()
    {
        runTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        MoveBoat();
        TurnBoat();
    }

    #region engineering
    public void ShiftGear(int dir)
    {
        bool canShift = runTime >= (gearLastShifted + gearShiftDelay);
        if (canShift)
        {
            gearBox.ShiftGear(dir);
            gearLastShifted = runTime;
        }
    }
    #endregion

    #region movement
    private void MoveBoat()
    {
        int dir = inputDevice.GetMovement();
        if (dir != 0)
        {
            ShiftGear(dir);
        }
        Vector3 vel = UpdatedSpeed();
        vel.y = rb.velocity.y; // preserver gravity
        rb.velocity = vel;
    }

    public void TurnBoat()
    {
        Vector3 rotation = inputDevice.GetRotation();
        Vector3 bank = inputDevice.GetBank();
        float modifiedBankSpeed = bankSpeed;

        if (rotation == Vector3.zero)
        {
            modifiedBankSpeed *= 2;
            // reset bank
            if (boatModel.localRotation.z < 0)
            {
                bank += (Vector3.forward * 0.5f);
            }
            else if (boatModel.localRotation.z > 0)
            {
                bank -= (Vector3.forward * 0.5f);
            }

            if (Mathf.Abs(boatModel.localRotation.z) < 0.01f)
            {
                Quaternion t = boatModel.localRotation;
                t.z = 0;
                bank = Vector3.zero;
                boatModel.localRotation = t;
            }
        }

        // apply banking 
        transform.RotateAround(transform.position, rotation, turnSpeed * Mathf.Abs(rotation.y));
        boatModel.Rotate(bank * modifiedBankSpeed);
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

        if (gearBox.IsMaxSpeed(currentSpeed))
        {
            currentSpeed = gearBox.maxSpeed;
        }

        return transform.forward * currentSpeed;
    }
    #endregion
}

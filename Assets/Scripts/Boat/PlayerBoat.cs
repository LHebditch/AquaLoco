using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoat : MonoBehaviour
{
    [SerializeField] BoatGearBox gearBox;

    public bool IsStopped()
    {
        return gearBox.GetCurrentGear().gear <= 0;
    }
}

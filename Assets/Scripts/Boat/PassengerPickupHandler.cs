using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger
{
    Transform originalParent;
    public Transform transform;

    public Passenger(Transform p)
    {
        transform = p;
        originalParent = p.transform.parent; // im not sure if we need this because we could just reparent to drop off point?
    }
}

public class PassengerPickupHandler : MonoBehaviour
{
    [SerializeField] BoatSeats seats;

    public void SeatPassenger(Component c, object passenger)
    {
        if (passenger is Transform)
        {
            Transform p = passenger as Transform;
            seats.SeatPassenger(new Passenger(p));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerPickupTrigger : MonoBehaviour
{
    [SerializeField] Transform[] passengers;
    [SerializeField] GameEvent passengerPickupEvent;

    public void PickupPassenger(Component c, object data)
    {
        if(c is ObjectivePoint && c.transform == transform)
        {
            foreach(Transform p in passengers)
            {
                passengerPickupEvent.Raise(this, p);
            }
        }
    }

}

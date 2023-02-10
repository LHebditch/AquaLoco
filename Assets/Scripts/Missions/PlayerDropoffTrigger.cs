using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropoffTrigger : MonoBehaviour
{
    [SerializeField] GameEvent passengerDropoffEvent;
    [SerializeField] Transform dropoffPoint;

    public void DropOffPassengers(Component c, object _)
    {
        if(c is ObjectivePoint && c.transform == transform)
        {
            passengerDropoffEvent.Raise(this, dropoffPoint);
        }
    }
}

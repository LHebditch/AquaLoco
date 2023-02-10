using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerDropoffHandler : MonoBehaviour
{
    [SerializeField] BoatSeats seats;


    public void DropOffPassengers(Component c, object dropoffPoint)
    {
        if (dropoffPoint is Transform)
        {
            seats.DisembarkPassengers(dropoffPoint as Transform);
        }   
    }
}

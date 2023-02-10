using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSeats : MonoBehaviour
{
    [SerializeField] Transform[] seats;

    private Dictionary<int, Passenger> occupiedSeats = new Dictionary<int, Passenger>();

    public void SeatPassenger(Passenger p)
    {
        int index = occupiedSeats.Count;
        occupiedSeats.Add(index, p);
        if(index < seats.Length)
        {
            p.transform.position = seats[index].transform.position;
            p.transform.parent = seats[index].transform;
        }
    }

    public void DisembarkPassengers(Transform dropOffPoint)
    {
        foreach(KeyValuePair<int, Passenger> seat in occupiedSeats)
        {
            seat.Value.transform.position = dropOffPoint.position;
            seat.Value.transform.parent = null;
        }
        occupiedSeats.Clear();
    }
}

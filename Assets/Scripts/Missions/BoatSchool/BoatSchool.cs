using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSchool : MonoBehaviour
{
    [SerializeField] ObjectivePoint startingObjectiove;
    [SerializeField] Mission boatSchoolMission;
    
    public void HandlePlayerRating(Component c, object data)
    {
        if(c is PlayerBoat && (float)data < 0)
        {
            MissionManager.instance.EnqueueMission(boatSchoolMission);
        }
    }

    public void StartMission(Component component, object _)
    {
        if (component is MissionContainer)
        {
            startingObjectiove.Activate();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSchool : MonoBehaviour
{
    [SerializeField] ObjectivePoint startingObjective;
    [SerializeField] Mission boatSchoolMission;
    [SerializeField] GameEvent increaseRankEvent;
    
    public void HandlePlayerRating(Component c, object data)
    {
        if(c is PlayerBoat && (float)data >= 0)
        {
            MissionManager.instance.BlockMission(boatSchoolMission);
        }
    }

    public void StartMission(Component component, object _)
    {
        if (component is MissionContainer)
        {
            startingObjective.Activate();
        }
    }

    public void CompleteMission(Component c, object _)
    {
        increaseRankEvent.Raise(this, 1);
    }
}

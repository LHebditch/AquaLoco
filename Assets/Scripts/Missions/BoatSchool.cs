using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSchool : MonoBehaviour
{
    void Start()
    {
        MissionManager.instance.EnqueueMission(MissionId.U_BOAT_SCHOOL);
    }
}

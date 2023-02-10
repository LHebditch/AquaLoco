using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionEnqueer : MonoBehaviour
{
    [SerializeField] Mission mission;

    void Start()
    {
        MissionManager.instance.EnqueueMission(mission);
    }

}

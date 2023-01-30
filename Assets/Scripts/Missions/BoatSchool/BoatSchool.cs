using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSchool : MonoBehaviour
{
    [SerializeField] Transform startingObjectiove;
    [SerializeField] Mission boatSchoolMission;

    void Start()
    {
        MissionManager.instance.EnqueueMission(boatSchoolMission);
    }

    public void StartMission()
    {
        PlayerManager.instance.UpdatePlayerObjective(startingObjectiove);
        startingObjectiove.gameObject.SetActive(true);
    }
}

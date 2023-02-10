using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AvailableMissionsDisplay : MonoBehaviour
{
    [SerializeField] private int missionCount = 4;
    [SerializeField] private Transform missionSelectorPrefab;

    private float boatStarRating = 0;

    private List<Mission> currentMissions = new List<Mission>();
    private Mission activeMission;

    private void Start()
    {
        Reload();
    }

    public void Open()
    {
        if (activeMission == null)
        {
            Reload();
        } 
        else
        {
            // Display active mission
        }
    }

    public void IncreaseBoatRating(Component c, object data)
    {
        if (data is int || data is float)
        {
            boatStarRating += Convert.ToSingle(data);
            Reload();
        } else
        {
            Debug.LogWarning("Star rating update come from somewhere...." + data.GetType());
        }
    }

    private void Reload()
    {
        MissionContainer[] containers = GetComponentsInChildren<MissionContainer>();
        for (int i = 0; i < containers.Length; i++)
        {
            Destroy(containers[i].transform.gameObject);
        }
        for (int i = 0; i < missionCount - currentMissions.Count; i++)
        {
            Mission m = MissionManager.instance.GetRandomMission(boatStarRating);
            if (m != null)
            {
                currentMissions.Add(m);
            }
        }

        foreach(Mission m in currentMissions)
        {
            Transform t = Instantiate(missionSelectorPrefab, transform.position, Quaternion.identity, transform);
            t.GetComponent<MissionContainer>().SetMission(m);
        }
    }

    public void SelectMission(Component c, object _)
    {
        if(c is MissionContainer)
        {
            activeMission = (c as MissionContainer).mission;
        }
    }

    public void CompleteMission(Component c, object _)
    {
        foreach(GameEvent ev in activeMission.onCompletionGameEvents)
        {
            ev.Raise(this, null);
        }
        currentMissions = currentMissions.Where(m => m.isUnique && m != activeMission).ToList();
        activeMission = null;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class AvailableMissionsDisplay : MonoBehaviour
{
    [SerializeField] private int missionCount = 4;
    [SerializeField] private Transform missionSelectorPrefab;

    private float boatStarRating = 0;

    private List<Mission> currentMissions = new List<Mission>();

    private void Start()
    {
        Reload();
    }

    public void Open()
    {
        // TODO: how do we behave if a mission is selected?
        if(currentMissions.Count == 0)
        {
            Reload();
        }
    }

    public void UpdateBoatRating(Component c, object data)
    {
        if(c is PlayerBoat && (
            data is int ||
            data is float
        ))
        {
            boatStarRating = (float)data;
            Reload();
        } else
        {
            Debug.LogWarning("Star rating update come from somewhere...." + data.GetType());
        }
    }

    private void Reload()
    {
        if (currentMissions.Count > 0) return;

        currentMissions.Clear();
        MissionContainer[] containers = GetComponentsInChildren<MissionContainer>();
        for(int i = 0; i < containers.Length; i++)
        {
            Destroy(containers[i].transform.gameObject);
        }
        for(int i = 0; i < missionCount; i++)
        {
            Mission m = MissionManager.instance.GetRandomMission(boatStarRating);
            if (m != null)
            {
                currentMissions.Add(m);
                Transform t = Instantiate(missionSelectorPrefab, transform.position, Quaternion.identity, transform);
                t.GetComponent<MissionContainer>().SetMission(m);
            }
        }
    }
}

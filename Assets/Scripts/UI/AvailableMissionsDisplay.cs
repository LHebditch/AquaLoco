using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableMissionsDisplay : MonoBehaviour
{
    [SerializeField] private int missionCount = 4;
    [SerializeField] private Transform missionSelectorPrefab;

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

    private void Reload()
    {
        currentMissions.Clear();
        for(int i = 0; i < missionCount; i++)
        {
            Mission m = MissionManager.instance.GetRandomMission(GameManager.instance.starRating);
            if (m != null)
            {
                currentMissions.Add(m);
                Transform t = Instantiate(missionSelectorPrefab, transform.position, Quaternion.identity, transform);
                t.GetComponent<MissionContainer>().SetMission(m);
            }
        }
    }
}

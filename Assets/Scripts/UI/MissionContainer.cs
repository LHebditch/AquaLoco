using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionContainer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] GameEvent missionSelectgameEvent;

    public Mission mission { get; private set; }

    public void SetMission(Mission m)
    {
        mission = m;
        title.SetText(m.title);
        description.SetText(m.description);
    }

    public void SelectMission()
    {
        foreach (GameEvent ev in mission.onStartGameEvents)
        {
            ev.Raise(this, null);
        }
        missionSelectgameEvent.Raise(this, null);
    }
}

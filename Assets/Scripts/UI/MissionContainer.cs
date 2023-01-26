using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionContainer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;

    private Mission mission;

    public void SetMission(Mission m)
    {
        mission = m;
        title.SetText(m.title);
        description.SetText(m.description);
    }

    public void SelectMission()
    {
        mission.OnSelect?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class MissionManager : MonoBehaviour
{
    #region singleton
    private static MissionManager _instance;

    public static MissionManager instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] public Mission[] genericMissions;
    [SerializeField] public Mission[] uniqueMissions;
    [SerializeField] private UnityEvent OnMissionComplete;

    private List<MissionId> completedMissions = new List<MissionId>();
    private Queue<MissionId> queuedMissions = new Queue<MissionId>(); // queue to queue missions for selection

    private int uniqueMissionsGiven = 0;

    /**
     * get a random mission who's level is greater than or equal to the given level
     */
    public Mission GetRandomMission(float level)
    {
        if (queuedMissions.Count > 0 && uniqueMissionsGiven < 3)
        {
            MissionId mId = queuedMissions.Dequeue();
            uniqueMissionsGiven++;
            return uniqueMissions.First(m => m.missoinId == mId);
        }
        // if we've given 2 unique missions, or none are queued then find a random one
        Mission[] missionsForLevel = genericMissions.Where(m => m.level <= level).ToArray();
        if(missionsForLevel.Length == 0)
        {
            return null;
        }
        int index = Random.Range(0, missionsForLevel.Length);  
        return missionsForLevel[index];
    }

    public void EnqueueMission(MissionId id)
    {
        queuedMissions.Enqueue(id);
    }

    public void CompleteMission(MissionId id)
    {
        completedMissions.Add(id);
        uniqueMissionsGiven = 0;

        if (OnMissionComplete != null)
        {
            OnMissionComplete.Invoke();
        }
    }
}
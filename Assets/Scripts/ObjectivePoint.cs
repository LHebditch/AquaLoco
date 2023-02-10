using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectivePoint : MonoBehaviour
{
    [SerializeField] private bool active;
    [SerializeField] private bool deactivateOnReached = false;
    [Header("Time in seconds that player needs to be in objective to trigger")]
    [SerializeField] private float requiredTimeInObjective = 0.5f;
    [Header("List of game events to be invoked when objective is reached")]
    [SerializeField] GameEvent[] gameEvents;
    [SerializeField] ObjectivePoint nextObjective;
    [SerializeField] GameEvent setObjectiveEvent;
    
    private GameObject self;
    private bool reached;
    private bool insideObjective = false;
    private float timeInObjective = 0f;

    private void Awake()
    {
        self = this.gameObject;
        if(active)
        {
            Activate();
        } else
        {
            Deactivate();
        }
    }

    private void Update()
    {
        if(insideObjective)
        {
            timeInObjective += Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(reached)
        {
            return;
        }
        if(other.tag == "Player")
        {
            insideObjective = true;
            if (timeInObjective >= requiredTimeInObjective)
            {
                OnReached();
            }
        }
    }

    private void OnReached()
    {
        reached = true;
        foreach(GameEvent ev in gameEvents)
        {
            ev.Raise(this, null);
        }

        if (nextObjective != null)
        {
            Deactivate();
            nextObjective.Activate();
        } 
        else if (deactivateOnReached)
        {
            Deactivate();
            setObjectiveEvent.Raise(null, null);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        insideObjective = false;
        timeInObjective = 0f;
    }

    public void Activate()
    {
        reached = false;
        self.SetActive(true);
        setObjectiveEvent.Raise(this, null);
    }

    public void Deactivate()
    {
        self.SetActive(false);
    }

    public void LogReached()
    {
        Debug.Log("Reached Objective");
    }
}

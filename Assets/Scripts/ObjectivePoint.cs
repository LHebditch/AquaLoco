using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectivePoint : MonoBehaviour
{
    [SerializeField] private bool active;
    [Header("Time in seconds that player needs to be in objective to trigger")]
    [SerializeField] private float requiredTimeInObjective = 0.5f;
    [SerializeField] GameEvent[] gameEvents;
    [SerializeField] ObjectivePoint nextObjective;
    
    private GameObject self;
    private bool insideObjective = false;
    private float timeInObjective = 0f;

    private void Awake()
    {
        self = this.gameObject;
        self.SetActive(active);
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
        foreach(GameEvent ev in gameEvents)
        {
            ev.Raise();
        }
        if (nextObjective != null)
        {
            Deactivate();
            nextObjective.Activate();
            PlayerManager.instance.UpdatePlayerObjective(nextObjective.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        insideObjective = false;
        timeInObjective = 0f;
    }

    public void Activate()
    {
        self.SetActive(true);
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

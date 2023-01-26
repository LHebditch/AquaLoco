using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    [SerializeField] Transform objective;
    [SerializeField] GameObject arrowMesh;

    public void SetObjective(Transform o)
    {
        objective = o;
    }

    private void FixedUpdate()
    {
        if(!objective)
        {
            arrowMesh.SetActive(false);
            return;
        }
        arrowMesh.SetActive(true);
        transform.LookAt(objective.position);
        Quaternion rot = transform.rotation;
        rot.x = 0;
        transform.rotation = rot;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    [SerializeField] Transform objective;
    [SerializeField] GameObject arrowMesh;

    public void SetObjective(Component o, object _)
    {
        objective = o.transform;
    }

    private void FixedUpdate()
    {
        if(!objective)
        {
            arrowMesh.SetActive(false);
            return;
        }
        arrowMesh.SetActive(true);
        Vector3 p = objective.position;
        p.y = transform.position.y;
        transform.LookAt(p);
        Quaternion rot = transform.rotation;
        rot.x = 0;
        transform.rotation = rot;
    }
}

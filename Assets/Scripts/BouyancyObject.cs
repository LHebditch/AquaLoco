using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BouyancyObject : MonoBehaviour
{
    [SerializeField] Transform[] floaters;
    [SerializeField] float underwaterDrag = 3f;
    [SerializeField] float underwaterAngularDrag = 1f;
    [SerializeField] float airDrag = 0f;
    [SerializeField] float airAngularDrag = 0.05f;
    [SerializeField] float floatingPower = 15f;
    [SerializeField] float waterHeight = 0f;
    private Rigidbody rb;
    private bool underwater = false;
    private int floatersUnderwater = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(floaters.Length == 0)
        {
            floaters = new Transform[1]
            {
                transform,
            };
        }
    }

    private void FixedUpdate()
    {
        floatersUnderwater = 0;
        foreach (Transform floater in floaters)
        {
            float difference = floater.position.y - waterHeight;
            if (difference < 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), floater.position, ForceMode.Force);
                floatersUnderwater += 1;
                if (!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }
            }
        }

        if(underwater && floatersUnderwater == 0) {
            underwater = false;
            SwitchState(false);
        }
    }

    private void SwitchState(bool isUndewater)
    {
        if(isUndewater)
        {
            rb.drag = underwaterDrag;
            rb.angularDrag = underwaterAngularDrag;
        } else
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
        }
    }
}

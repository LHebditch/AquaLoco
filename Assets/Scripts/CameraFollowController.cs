using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField] Transform boatToFollow;

    private Vector3 offset;
    
    void Start()
    {
        offset = transform.position- boatToFollow.position;

    }

    void FixedUpdate()
    {
        Vector3 boatPos = boatToFollow.position;
        boatPos.x += offset.x;
        boatPos.y = transform.position.y;
        boatPos.z += offset.z;
        transform.position = boatPos;
    }
}

using UnityEngine;

public class BoatWake : MonoBehaviour
{
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Material mat;


    void Start()
    {
        GameObject g = new GameObject();
        g.transform.position = Vector3.zero;
        MeshRenderer mr = g.AddComponent<MeshRenderer>();
        mr.material = mat;
        WakeRenderer wr = g.AddComponent<WakeRenderer>();
        wr.backLeftCorner = left;
        wr.backRightCorner = right;
        wr.trailFrameLength = 50;

        g.name = "Boat Wake" + gameObject.name;
    }
}

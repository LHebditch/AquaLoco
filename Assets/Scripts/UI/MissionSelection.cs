using UnityEngine;

public class MissionSelection : MonoBehaviour
{
    private void Awake()
    {
        Close();
    }

    public void Close()
    {
        transform.localPosition = new Vector3(0, -500f, 0);
    }

    public void Open()
    {
        transform.localPosition = Vector3.zero;
    }
}

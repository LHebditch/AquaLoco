using UnityEngine;

public class PlayerBoat : MonoBehaviour
{
    [SerializeField] BoatGearBox gearBox;
    [SerializeField] GameEvent updateStarRatingEvent;

    [SerializeField] private float starRating = -1;

    private void Start()
    {
        updateStarRatingEvent.Raise(this, starRating);
    }

    public bool IsStopped()
    {
        return gearBox.GetCurrentGear().gear <= 0;
    }

    public void StopFromEvent(Component c, object d)
    {
        Stop();
    }

    public void Stop()
    {
        gearBox.ShiftToGear(0);
    }
}

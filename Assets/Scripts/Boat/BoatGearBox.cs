using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class BoatGear
{
    public int gear;
    public float speed;
}

public class BoatGearBox : MonoBehaviour
{
    [Header("Gear Speeds - set the speed for each gear")]
    [SerializeField] float gear1;
    [SerializeField] float gear2;
    [SerializeField] float gear3;
    [SerializeField] float gear4;
    [SerializeField] float gear5;

    public float maxSpeed = 0f;

    private int currentGear = 0;
    private int lastGear = 0;
    private Dictionary<int, float> gears;
    

    private void Start()
    {
        gears = new Dictionary<int, float>
        {
            { 0, 0f }, // neutral is static
            { 1, gear1 },
            { 2, gear2 },
            { 3, gear3 },
            { 4, gear4 },
            { 5, gear5 },
        };
        maxSpeed = gear5;
    }

    public void ShiftGear(int direction)
    {
        int nextGear = currentGear + direction;
        if (!gears.ContainsKey(nextGear))
        {
            return;
        }
        lastGear = currentGear;
        currentGear = nextGear;
    }

    public BoatGear GetCurrentGear()
    {
        gears.TryGetValue(currentGear, out float gearVal);
        return new BoatGear {
            gear=currentGear,
            speed=gearVal,
        };
    }

    public BoatGear GetLastGear()
    {
        gears.TryGetValue(lastGear, out float gearVal);
        return new BoatGear
        {
            gear = lastGear,
            speed = gearVal,
        };
    }

    public bool IsMaxSpeed(float speed)
    {
        return speed >= maxSpeed;
    }
}

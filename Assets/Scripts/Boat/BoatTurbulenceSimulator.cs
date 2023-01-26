using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatTurbulenceSimulator : MonoBehaviour
{
    [SerializeField] Transform boatModel;
    [SerializeField] BoatGearBox gearBox;
    [SerializeField] float minSpeedToHitTurbulence = 0f;
    [SerializeField] float minTurbulence;
    [SerializeField] float maxTurbulence;
    [SerializeField] float minTurbulenceInterval;
    [SerializeField] float maxTurbulenceInterval;

    private float runTime;
    private float nextAloudTurbulenceTime = 0;

    private void Update()
    {
        runTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (runTime >= nextAloudTurbulenceTime)
        {
            if (gearBox.GetCurrentGear().speed < minSpeedToHitTurbulence)
            {
                return;
            }
            float turbulenceAmount = Random.Range(minTurbulence, maxTurbulence);
            

            
            nextAloudTurbulenceTime = runTime + Random.Range(minTurbulenceInterval, maxTurbulenceInterval);
        }
    }
}

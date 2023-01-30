using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnLocation
{
    SPECIFIC,
    BEACH,
    SHALLOW_WATER,
    DEEP_WATER,
}
public class SpawnException : System.Exception
{
    public SpawnException() { }

    public SpawnException(string message) : base(message) { }
}
/**
 * This class will handle spaning something at a location
 * Usecases:
 * - atach to an objective point to use Spawn method on completion
 */
public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform missionStartPrefab;
    [SerializeField] private SpawnLocation location;
    [Header("If using SPECIFIC location then provide position")]
    [SerializeField] private Vector3 spawnPosition;

    public void Spawn()
    {
        switch(location)
        {
            case SpawnLocation.SPECIFIC:
                Instantiate(missionStartPrefab, spawnPosition, Quaternion.identity);
                break;
            default:
                throw new SpawnException("I do not know how to spawn here");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region singleton
    private static PlayerManager _instance;

    public static PlayerManager instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            this.Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public static PlayerBoat playerBoat;
    public static PlayerArrow playerArrow;

    public void Init()
    {
        playerArrow = FindObjectOfType<PlayerArrow>();
        playerBoat = FindObjectOfType<PlayerBoat>();

        if(playerArrow == null || playerBoat == null)
        {
            throw new MissingComponentException("Can't find player");
        }
    }

    public void UpdatePlayerObjective(Transform t)
    {
        playerArrow.SetObjective(t);
    }
}

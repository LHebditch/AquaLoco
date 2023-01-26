using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager _instance;

    public static  GameManager instance { get
        {
            return _instance;
        } 
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region UI Elements
    [SerializeField] GameObject[] ephemeralUIElements;
    #endregion

    public float starRating = -1;

    public void IncreaseStarRating(float amount = 1)
    {
        starRating += amount;
    }

    public void CloseUI()
    {
        foreach (GameObject ui in ephemeralUIElements)
        {
            ui.SetActive(false);
        }
    }
}

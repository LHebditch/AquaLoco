using UnityEngine;

public class InitialState : MonoBehaviour
{

    [SerializeField] private bool initialState; 
    
    void Start()
    {
        gameObject.SetActive(initialState);   
    }
}

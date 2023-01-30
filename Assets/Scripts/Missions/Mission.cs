using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Mission
{   
#if UNITY_EDITOR
    public string title;
#endif
    public GameEvent[] gameEvents;
    public int level;
    [TextArea()]
    public string description;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Mission")]
public class Mission: ScriptableObject
{   
#if UNITY_EDITOR
    public string title;
#endif
    public GameEvent[] onStartGameEvents;
    public GameEvent[] onCompletionGameEvents;
    public int level;
    [TextArea()]
    public string description;
    public bool isUnique;
}

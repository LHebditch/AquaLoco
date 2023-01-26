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
    public MissionId missoinId;
    public int level;
    [TextArea()]
    public string description;
    public UnityEvent OnSelect;
    public bool unique;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct DialogueAnswer
{
    [TextArea(3, 20)]
    public string text;
    public DialogueNode nextNode;
    public bool x;
    public bool y;
}

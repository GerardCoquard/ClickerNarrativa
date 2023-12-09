using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewNode", menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    [TextArea(5, 20)]
    public string text;
    public DialogueNode nextNode;
    public QuestionNode nextQuestion;
    public bool player;
    public bool narrador;
}

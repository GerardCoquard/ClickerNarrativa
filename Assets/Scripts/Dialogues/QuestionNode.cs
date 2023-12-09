using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewQuestion", menuName = "Dialogue/Question")]
public class QuestionNode : ScriptableObject
{
    public List<DialogueAnswer> answers = new List<DialogueAnswer>();
    public bool finalQuestion;
    public DialogueNode xNode;
    public DialogueNode yNode;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicManager : MonoBehaviour
{
    public DialogueNode startingNode;
    public NPC npc;
    public string nextSceneName;
    private void Start() {
        DialogueManager.instance.StartDialogue(npc,startingNode);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;
    private void Awake() {
        instance = this;
    }
    public List<NPC> npcs = new List<NPC>();
    public NPC GetNPC(string npcName)
    {
        foreach (NPC item in npcs)
        {
            if(item.data._name == npcName) return item;
        }
        Debug.LogWarning("No NPC named " + npcName + " found.");
        return null;
    }
}

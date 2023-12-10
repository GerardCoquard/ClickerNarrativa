using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnblockAll : MonoBehaviour
{
    NPC npc;
    bool activated;
    private void Start() {
        npc = GetComponent<NPC>();
    }
    private void Update() {
        if(activated) return;

        if(npc.unblockeds[0] == true)
        {
            activated = true;
            for (int i = 0; i < npc.unblockeds.Length; i++)
            {
                npc.unblockeds[i] = true;
            }
        }
    }
}

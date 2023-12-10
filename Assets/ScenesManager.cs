using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    NPC_CurrencyAmount npc;
    void Start()
    {
        npc = (NPC_CurrencyAmount) GameObject.FindObjectOfType (typeof(NPC_CurrencyAmount));
    }
    void Update()
    {
        if(GameManager.instance.currency >= npc.currencyLimits[2]) SceneManager.LoadScene("Cinematica"+(GameManager.instance.currentPhase+2).ToString());
    }
}

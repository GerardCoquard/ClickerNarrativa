using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_CurrencyAmount : NPC
{
    public Sprite[] shopIcons;
    public float[] currencyLimits;
    private void Start() {
        npcRequirements = new List<NPCRequirement>();
        for (int i = 0; i < currencyLimits.Length; i++)
        {
            NPCRequirement req = new NPCRequirement();
            if(i == 0) req.currencyAmount = GameManager.instance.currencyLimit;
            else req.currencyAmount = currencyLimits[i-1];
            npcRequirements.Add(req);
        }
    }
    public override void ApplyUpgrade(int upgrade)
    {
        GameManager.instance.SetCurrencyLimit(currencyLimits[upgrade]);
        unblockeds[upgrade] = false;
    }
    public override int[] GetUpgrades()
    {
        return new int[2] {GetTotalUpgrades(),GetTotalUpgrades()};
    }
    public override float GetCost(int upgrade)
    {
        return GameManager.instance.currencyLimit;
    }
}

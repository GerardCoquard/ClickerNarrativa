using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public NPCData data;
    public Sprite[] npcUpgradeIcon;
    public Image _icon;
    //[NonSerialized]
    public bool[] unblockeds;
    //[NonSerialized]
    public int[] levels;
    //[NonSerialized]
    public int[] offers;
    public List<NPCRequirement> npcRequirements = new List<NPCRequirement>();
    ControllerNPC controller;
    private void Awake() {
        controller = GetComponent<ControllerNPC>();
        _icon.sprite = data._icon;
        unblockeds = new bool[3];
        levels = new int[3];
        offers = new int[3];
    }
    
    public void Offer(int upgrade)
    {
        offers[upgrade]++;
        RefreshShop();
    }
    public void Unblock(int upgrade)
    {
        unblockeds[upgrade] = true;
        RefreshShop();
    }
    public void Buy(int upgrade)
    {
        levels[upgrade]++;
        ApplyUpgrade(upgrade);
        RefreshShop();
    }
    public float GetCost(int upgrade)
    {
        //Formula rara
        float cost = data._baseCost*(levels[upgrade]+1)*Utilities.GetLevelConstant();
        if(offers[upgrade] > 0)
        {
            float modifier = offers[upgrade] == 2 ? Utilities.GetOfferConstant()*Utilities.GetOfferConstant() : Utilities.GetOfferConstant();
            return cost*modifier;
        }
        else return cost;
    }
    public void RefreshShop()
    {
        Shop.instance.Open(this);
    }
    public void Interact()
    {
        controller.CheckSpeak();
        RefreshShop();
    }
    public int GetTotalUpgrades()
    {
        int totalUpgrades = 0;
        for (int i = 0; i < levels.Length; i++)
        {
            totalUpgrades += levels[i];
        }
        return totalUpgrades;
    }
    public int GetTotalOffers()
    {
        int totalOffers = 0;
        for (int i = 0; i < offers.Length; i++)
        {
            totalOffers += offers[i];
        }
        return totalOffers;
    }
    public bool HasAllUpgrades()
    {
        foreach (bool item in unblockeds)
        {
            if(!item) return false;
        }
        return true;
    }
    public int[] GetOffers()
    {
        if(GetTotalOffers() == 0) return new int[2] {0,1};
        if(GetTotalOffers() == 1) return new int[2] {1,2};
        if(GetTotalOffers() == 2) return new int[2] {0,2};

        return new int[2] {0,0};
    }
    public virtual int[] GetUpgrades()
    {
        List<int> indxs = new List<int>();
        for (int i = 0; i < unblockeds.Length; i++)
        {
            if(indxs.Count >= 2) continue;
            if(!unblockeds[i]) indxs.Add(i);
        }
        if(indxs.Count <= 1) return new int[2] {indxs[0],indxs[0]};
        else return new int[2] {indxs[0],indxs[1]};
    }
    public virtual void ApplyUpgrade(int upgrade){}

}

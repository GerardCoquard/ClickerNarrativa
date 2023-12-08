using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public Image npcUpgradeIcon;
    public GameObject shopInterface;
    public ShopSlot[] slots = new ShopSlot[3];
    public NPC currentNPC;
    private void Awake() {
        instance = this;
    }
    private void Start() {
        shopInterface.SetActive(false);
    }
    public void Open(NPC npc)
    {
        shopInterface.SetActive(true);
        currentNPC = npc;
        SetSlots();
        SetShopIcons();
    }
    public void Close()
    {
        shopInterface.SetActive(false);
    }
    void SetSlots()
    {
        Sprite _upgradeIcon = GameManager.instance.currentPhase > currentNPC.npcUpgradeIcon.Length-1 ? currentNPC.npcUpgradeIcon[0] : currentNPC.npcUpgradeIcon[GameManager.instance.currentPhase];
        npcUpgradeIcon.sprite = _upgradeIcon;
        for (int i = 0; i < slots.Length; i++)
        {
            if(!currentNPC.unblockeds[i]) slots[i].Bloqued(currentNPC);
            else slots[i].Show(currentNPC,i);
        }
    }
    public void TryBuy(ShopSlot slot)
    {
        int upgrade = Array.IndexOf(slots,slot);
        if(currentNPC.unblockeds[upgrade]!=true) return;
        if(currentNPC.GetCost(upgrade) > GameManager.instance.currency) return;
        GameManager.instance.AddCurrency(-currentNPC.GetCost(upgrade));
        currentNPC.Buy(upgrade);
    }
    public void SetShopIcons()
    {
        NPC_CurrencyAmount npc_currency = currentNPC.GetComponent<NPC_CurrencyAmount>();
        if(npc_currency != null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetIcon(npc_currency.shopIcons[i]);
            }
        }
        else
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetIcon(ActorsManager.instance.actors[i].actorData.actorIcon);
            }
        }
    }
}

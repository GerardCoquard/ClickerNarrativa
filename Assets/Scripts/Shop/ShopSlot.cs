using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    public Image upgradeIcon;
    public GameObject offer1;
    public GameObject offer2;
    public GameObject block;
    public TextMeshProUGUI level;
    public TextMeshProUGUI cost;
    public void Show(NPC npc,int upgrade)
    {
        block.SetActive(false);
        offer1.SetActive(npc.offers[upgrade] >=1);
        offer2.SetActive(npc.offers[upgrade] >=2);
        level.text = "LVL: " + npc.levels[upgrade].ToString();
        cost.text = Utilities.ToCurrency(npc.GetCost(upgrade));
    }
    public void Bloqued(NPC npc)
    {
        block.SetActive(true);
        offer1.SetActive(false);
        offer2.SetActive(false);
        level.text = "";
        cost.text = "";
    }
    public void SetIcon(Sprite sprite)
    {
        upgradeIcon.sprite = sprite;
    }
}

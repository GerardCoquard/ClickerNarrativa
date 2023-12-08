using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Requirement : MonoBehaviour
{
    public TextMeshProUGUI textUpgrades;
    public Image npcIcon;
    public Color normalColor;
    public Color enoughColor;
    public void SetRequirement(NPCRequirements requirement)
    {
        textUpgrades.text = requirement.npc.GetTotalUpgrades().ToString() + "/" + requirement.amountUpgrades.ToString();
        npcIcon.sprite = requirement.npc.data._icon;
        if(requirement.npc.GetTotalUpgrades() >= requirement.amountUpgrades) textUpgrades.color = enoughColor;
        else textUpgrades.color = normalColor;
    }
}

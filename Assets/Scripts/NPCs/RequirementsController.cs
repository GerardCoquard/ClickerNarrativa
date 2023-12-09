using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequirementsController : MonoBehaviour
{
    public static RequirementsController instance;
    public List<Requirement> requirements;
    public NPCRequirement currentRequirements;
    public TextMeshProUGUI currencyText;
    bool maxRequirements;

    private void Awake() {
        instance = this;
        gameObject.SetActive(false);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show(NPCRequirement _req)
    {
        maxRequirements = false;
        currentRequirements = _req;
        gameObject.SetActive(true);
        SetRequirements();
        UpdateRequirements();
    }
    public void MaxRequirements()
    {
        maxRequirements = true;
        gameObject.SetActive(true);
        currencyText.gameObject.SetActive(true);
        currencyText.text = "Max";
        foreach (Requirement item in requirements)
        {
            item.gameObject.SetActive(false);
        }
    }
    private void Update() {
        if(!maxRequirements) UpdateRequirements();
    }
    public void UpdateRequirements()
    {
        if(currencyText.gameObject.activeInHierarchy)
        {
            string text = Utilities.ToNumber(GameManager.instance.currency) + "/" + Utilities.ToNumber(currentRequirements.currencyAmount);
            currencyText.text = Utilities.ToCurrencyType(text);
            if(GameManager.instance.currency >= currentRequirements.currencyAmount) currencyText.color = requirements[0].enoughColor;
            else currencyText.color = requirements[0].normalColor;
        }
        
        if(currentRequirements.npcRequirements == null) return;
        for (int i = 0; i < currentRequirements.npcRequirements.Count; i++)
        {
            requirements[i].SetRequirement(currentRequirements.npcRequirements[i]);
        }
    }
    public void SetRequirements()
    {
        currencyText.gameObject.SetActive(currentRequirements.currencyAmount > 0);
        foreach (Requirement item in requirements)
        {
            item.gameObject.SetActive(false);
        }
        if(currentRequirements.npcRequirements == null) return;
        for (int i = 0; i < currentRequirements.npcRequirements.Count; i++)
        {
            requirements[i].gameObject.SetActive(true);
        }
    }
}

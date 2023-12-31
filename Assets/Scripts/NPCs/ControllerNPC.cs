using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerNPC : MonoBehaviour
{
    bool canSpeak;
    int currentPosition;
    public GameObject speakIcon;
    NPC npc;
    private void Start() {
        npc = GetComponent<NPC>();
    }
    public bool CheckConditions()
    {
        if(currentPosition >= npc.npcRequirements.Count) return false;

        NPCRequirement requirements = npc.npcRequirements[currentPosition];
        if(GameManager.instance.currency < requirements.currencyAmount) return false;
        if(requirements.npcRequirements == null) return true;
        foreach (NPCRequirements item in requirements.npcRequirements)
        {
            if(item.npc.GetTotalUpgrades() < item.amountUpgrades) return false;
        }
        return true;
    }

    private void Update() {
        if(!canSpeak) canSpeak = CheckConditions();
        speakIcon.SetActive(canSpeak);
    }
    public void Spoke()
    {
        canSpeak = false;
        currentPosition++;
    }
    public void CheckSpeak()
    {
        if(canSpeak)
        {
            RequirementsController.instance.Hide();
            DialogueManager.instance.StartDialogue(npc,GetDialogueNode());
            Spoke();
        }
        else
        {
            if(currentPosition < npc.npcRequirements.Count) RequirementsController.instance.Show(npc.npcRequirements[currentPosition]);
            else RequirementsController.instance.MaxRequirements();
        }
    }
    public DialogueNode GetDialogueNode()
    {
        if(npc.HasAllUpgrades()) return GetDialogueName("O",npc.GetOffers());
        else return GetDialogueName("D",npc.GetUpgrades());
    }
    public DialogueNode GetDialogueName(string _type, int[] indexes)
    {
        string fileName = "";

        if(indexes[0] == indexes[1]) fileName = _type + indexes[0];
        else fileName = _type + indexes[0] + indexes[1];

        string npcName = npc.data.name.Replace("í","i");
        npcName = npcName.Replace("á","a");

        return (DialogueNode) Resources.Load("NPCs/Mundo_"+(GameManager.instance.currentPhase+1).ToString()+"/"+npcName+"/"+fileName+"/"+"0");
    }
}
[System.Serializable]
public struct NPCRequirements
{
    public NPC npc;
    public int amountUpgrades;

}
[System.Serializable]
public struct NPCRequirement
{
    public List<NPCRequirements> npcRequirements;
    public float currencyAmount;
}

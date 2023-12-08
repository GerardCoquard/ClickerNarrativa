using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewNPCData", menuName = "NPCs/Data", order = 2)]
public class NPCData : ScriptableObject
{
    public float _baseCost;
    public float _modifierValue;
    public string _name;
    public Sprite _icon;
}
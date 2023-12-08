using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AutoBarTime : NPC
{
    public override void ApplyUpgrade(int upgrade)
    {
        Actor actor = ActorsManager.instance.GetActor(upgrade);
        actor.SetAutoBarTime(actor.autoBarTime*data._modifierValue);
    }
}

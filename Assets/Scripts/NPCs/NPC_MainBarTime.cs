using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_MainBarTime : NPC
{
    public override void ApplyUpgrade(int upgrade)
    {
        Actor actor = ActorsManager.instance.GetActor(upgrade);
        actor.SetMainBarTime(actor.mainBarTime/data._modifierValue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_MainBarAmount : NPC
{
    public override void ApplyUpgrade(int upgrade)
    {
        Actor actor = ActorsManager.instance.GetActor(upgrade);
        actor.SetMainBarAmount((int)(actor.mainBarAmount*data._modifierValue));
    }
}

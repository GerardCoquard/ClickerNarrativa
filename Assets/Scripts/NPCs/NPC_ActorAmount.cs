using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ActorAmount : NPC
{
    public override void ApplyUpgrade(int upgrade)
    {
        Actor actor = ActorsManager.instance.GetActor(upgrade);
        actor.SetActorAmount(actor.actorAmount+data._modifierValue);
    }
}

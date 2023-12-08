using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorsManager : MonoBehaviour
{
    public static ActorsManager instance;
    private void Awake() {
        instance = this;
    }
    public Actor[] actors = new Actor[3];
    public Actor GetActor(int upgrade)
    {
        return actors[upgrade];
    }
    public bool IsActive(int upgrade)
    {
        return actors[upgrade].gameObject.activeInHierarchy;
    }
}

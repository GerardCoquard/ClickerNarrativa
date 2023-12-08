using UnityEngine;

[CreateAssetMenu(fileName = "NewActorData", menuName = "Actors/Data", order = 1)]
public class ActorData : ScriptableObject
{
    [Header("Stats")]
    public float mainBarAmount;
    public float mainBarTime;
    public float actorAmount;
    public float autoBarTime;

    [Header("Limits")]
    public float autoBarLimitTime;
    public float mainBarLimitTime;
    [Header("Visuals")]
    public string actorName;
    public Sprite actorIcon;
}

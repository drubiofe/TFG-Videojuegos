using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public new string name;
    public string description;
    public int abilityLevel;
    public Sprite sprite;
    public float cooldownTime;
    public float duration;

    public abstract void InitializeAbility(GameObject obj);
    public abstract void TriggerAbility();
}

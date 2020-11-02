using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string ability_Name = "New Ability";
    public Sprite aSprite;
    public AudioClip aSound;
    public float aBaseCoolDown;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();

}

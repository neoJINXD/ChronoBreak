using UnityEngine;

public abstract class Ability : ScriptableObject
{
    // Assignables
    [SerializeField] protected string abilityName;

    // TODO should probably have a way to make them exposed in the editor while lesser access modifier
    [SerializeField] public Sprite abilitySprite;
    [SerializeField] public AudioClip abilitySound;
    [SerializeField] public float abilityCoolDown;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();

}

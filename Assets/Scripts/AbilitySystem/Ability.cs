using UnityEngine;

public abstract class Ability : ScriptableObject
{
    // Assignables
    [SerializeField] protected string abilityName;

    // TODO try to see if we can implement "getters" for serizlizefield variables

    [SerializeField] Sprite abilitySprite;
    [SerializeField] AudioClip abilitySound;
    [SerializeField] float abilityCoolDown;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();


    public Sprite GetAbilitySprite() 
    {
        return this.abilitySprite;
    }

    public AudioClip GetAudioClip()
    {
        return this.abilitySound;
    }

    public float GetAbilityCoolDown()
    {
        return this.abilityCoolDown;
    }
}

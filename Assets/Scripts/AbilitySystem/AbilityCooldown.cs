using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    // Assignables
    [SerializeField] string abilityButtonAxisName;
    [SerializeField] Image darkMask;
    [SerializeField] Text cooldownTextDisplay;
    [SerializeField] private Ability ability;
    [SerializeField] private GameObject player;
    [SerializeField] private Timer timer;

    // References
    private Image buttonImage;
    private AudioSource abilitySource;
    private float cooldownDuration;
    private float nextReadyTime;
    private float cooldownTimeLeft;


    void Start()
    {
       Initialize(ability, player);
    }

    void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if (coolDownComplete)
        {
            AbilityReady();
            if (Input.GetButtonDown(abilityButtonAxisName))
            {
                ButtonTriggered();
            }
        }
        else
        {
            CoolDown();
        }
    }
   
    public void Initialize(Ability selectedAbility, GameObject obj)
    {
        //pulling data out of scriptable obj
        ability = selectedAbility;
        buttonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();

        buttonImage.sprite = ability.GetAbilitySprite();
        darkMask.sprite = ability.GetAbilitySprite();
        cooldownDuration = ability.GetAbilityCoolDown();
        abilitySource.clip = ability.GetAudioClip();
        
        ability.Initialize(obj);
        
        AbilityReady();
    }

    private void AbilityReady()
    {
        cooldownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }

    private void CoolDown()
    {
        cooldownTimeLeft -= Time.deltaTime;
        darkMask.fillAmount = (cooldownTimeLeft / cooldownDuration);
        
        float roundedCd = Mathf.Round(cooldownTimeLeft);
        cooldownTextDisplay.text = roundedCd.ToString();
    }

    private void ButtonTriggered()
    {
        nextReadyTime = cooldownDuration + Time.time;
        cooldownTimeLeft = cooldownDuration;

        darkMask.enabled = true;
        cooldownTextDisplay.enabled = true;
        
        abilitySource.Play();
        ability.TriggerAbility();

        timer.CountEvent(abilityButtonAxisName);
    }
}

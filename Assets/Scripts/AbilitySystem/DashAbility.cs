using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash Ability")]
public class DashAbility : Ability
{   
    // References
    private GameObject player; 
    private TriggerDash dash;

    public override void Initialize(GameObject obj)
    {
        player = obj;
        dash = player.GetComponent<TriggerDash>();
    }

    public override void TriggerAbility()
    {
        dash.beginDash();
    }

}

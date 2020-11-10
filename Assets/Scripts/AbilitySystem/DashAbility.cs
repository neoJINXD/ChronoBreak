using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash Ability")]
public class DashAbility : Ability
{
    // Assignables 
    [SerializeField] float dashDistance;
    
    // References
    private GameObject player; 
    private PlayerMovement movement;
    private Vector3 inclinedDownForward;
    //TODO can rework this whole thing reeeee
    public override void Initialize(GameObject obj)
    {
        //TODO miught not need this comment here
        // obj should be a player gameObj refferenced in abilitycooldown.cs in
        // hierarchy, there should be a click
        // and drag to refer to this.
        abilityName = "Dash";
        player = obj;
        movement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    public override void TriggerAbility()
    {
        
        bool playerOrientationHit = Physics.Raycast(movement.transform.position, movement.getOrientationDirection(), out RaycastHit hit, (dashDistance+2));
        bool camRayHit = Physics.Raycast(movement.transform.position, movement.getPlayerCam().transform.forward, out RaycastHit hit2, dashDistance);


        // does a dash based on what the orientation forward and playerCam forward are hitting
        // each condition has a special modification done on how far the dash will take the player based on what the ray cast hits
        // raycast both hit something
        if (playerOrientationHit && camRayHit)
        {
            // Debug.Log("1");
            // Debug.Log(hit.collider.tag);
            // Debug.Log(distance);
            float distance = Vector3.Distance(movement.transform.position, hit.collider.transform.position);

            // TODO can prob be reduced 
            float multiplier;
            if (hit.collider.tag == "Ramp")
            {
                multiplier = 1f;
                player.transform.position += movement.getOrientationDirection() * ((1f / 10) * dashDistance);
            }
            else
            {
                
                if (distance > 10.7)
                {
                    //  Debug.Log("a1");
                    multiplier = 7f;
                    player.transform.position += movement.getOrientationDirection() * ((7f / 10f) * dashDistance);
                }
                else if (distance > 8.5 && distance <= 10.7)
                {
                    // Debug.Log("a");
                    multiplier = 5f;
                    player.transform.position += movement.getOrientationDirection() * ((5f / 10f) * dashDistance);
                }
                else if (distance > 6.5 && distance <= 8.5)
                {
                    // Debug.Log("b");
                    multiplier = 3f;
                    player.transform.position += movement.getOrientationDirection() * ((3f / 10f) * dashDistance);
                }
                else if (distance > 5.6f && distance <= 6.5)
                {
                    // Debug.Log("c");
                    multiplier = 1f;
                    player.transform.position += movement.getOrientationDirection() * ((1f / 10f) * dashDistance);
                }
                else if (distance >= 0 && distance <= 5.6)
                {
                    // Debug.Log("d");
                    multiplier = 0.05f;
                    player.transform.position += movement.getOrientationDirection() * ((0.05f / 10) * dashDistance);
                }
                else
                {
                    //  Debug.Log("e");
                    multiplier = 10f;
                    player.transform.position += movement.getOrientationDirection() * dashDistance;
                }
            }
            // player.transform.position += movement.getOrientationDirection() * ((multiplier / 10f) * dashDistance);

        }
        //playerorientation forward hit something but playercameraforward did NOT
        else if (playerOrientationHit && !camRayHit)
        {
           // Debug.Log("2");
            player.transform.position += movement.getPlayerCam().transform.forward * dashDistance;
        }
        //playerorientation forward did NOT hit something but the playercameraforward did
        else if (!playerOrientationHit && camRayHit)
        {
           // Debug.Log("3");
           //mainly used for ariel dashing on this else if block
            if (movement.isGrounded())
                player.transform.position += movement.getOrientationDirection() * dashDistance;
            else 
            {
                player.transform.position += movement.getPlayerCam().transform.forward * (dashDistance / 8);
            }
            
        }
        else if(!playerOrientationHit && !camRayHit)
        {
          // Debug.Log("4");

            if (movement.isGrounded())
            {
                if(Vector3.Angle(movement.transform.up, movement.getPlayerCam().transform.forward) >= 94)
                {
                    inclinedDownForward = new Vector3(movement.getPlayerCam().transform.forward.x, -0.3f, movement.getPlayerCam().transform.forward.z);
                    player.transform.position += inclinedDownForward * dashDistance;
                }
                else
                {
                    player.transform.position += movement.getOrientationDirection() * dashDistance;
                }
                
            }
            else
                player.transform.position += movement.getPlayerCam().transform.forward * dashDistance;

        }

        
    }

}

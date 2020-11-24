using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash Ability")]
public class DashAbility : Ability
{
    // Assignables 
    [SerializeField] float dashDistance;
    
    // References
    private GameObject player; 
    private PlayerMovement movement;
    private TriggerDash dash;
    private Vector3 inclinedDownForward, playerOrientationForward, playerCameraForward;
    private Vector3 movementPosition, groundedVector;

    //TODO can rework this whole thing reeeee
    public override void Initialize(GameObject obj)
    {
        player = obj;
        movement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        dash = player.GetComponent<TriggerDash>();
    }

    public override void TriggerAbility()
    {

        movementPosition = player.transform.position;

        playerOrientationForward = movement.getOrientationDirection();
        playerCameraForward = movement.getPlayerCam().transform.forward;

        groundedVector = -(movement.transform.up);

        bool playerOrientationHit = Physics.Raycast(movementPosition, playerOrientationForward, out RaycastHit hit, (dashDistance+2));
        bool camRayHit = Physics.Raycast(movementPosition, playerCameraForward, dashDistance);
        bool isGrounded = Physics.Raycast(movementPosition, groundedVector, 1.5f);

        // does a dash based on what the orientation forward and playerCam forward are hitting
        // each condition has a special modification done on how far the dash will take the player based on what the ray cast hits
        // raycast both hit something
        if (playerOrientationHit && camRayHit)
        {
            //Debug.Log("1");
            // Debug.Log(hit.collider.tag);
            // Debug.Log(distance);
            float distance = Vector3.Distance(movementPosition, hit.collider.transform.position);
            float multiplier;
            if (hit.collider.CompareTag("Ramp"))
            {
                multiplier = (1f / 10f);
                dash.SetNewPosition(movementPosition + playerOrientationForward * (multiplier * dashDistance));


            }
            else
            {
                
                if (distance > 10.7 && distance <= 13.5)
                {
                    //Debug.Log("a1");
                    multiplier = (7f / 10f);
                    dash.SetNewPosition(movementPosition + playerOrientationForward * (multiplier * dashDistance));

                }
                else if (distance > 8.5 && distance <= 10.7)
                {
                    //Debug.Log("a");
                    multiplier = (5f / 10f);
                    dash.SetNewPosition(movementPosition + playerOrientationForward * (multiplier * dashDistance));

                }
                else if (distance > 6.5 && distance <= 8.5)
                {
                    //Debug.Log("b");
                    multiplier = (3f / 10f);
                    dash.SetNewPosition(movementPosition + playerOrientationForward * (multiplier * dashDistance));
                }
                else if (distance > 5.6f && distance <= 6.5)
                {
                    //Debug.Log("c");
                    multiplier = (1f / 10f);
                    dash.SetNewPosition(movementPosition + playerOrientationForward * (multiplier * dashDistance));

                }
                else if (distance >= 0 && distance <= 5.6)
                {
                    //Debug.Log("d");
                    multiplier = (0.05f / 10);
                    dash.SetNewPosition(movementPosition + playerOrientationForward * (multiplier * dashDistance));

                }
                else
                {
                    //Debug.Log("e");
                    dash.SetNewPosition(movementPosition + playerOrientationForward * dashDistance);

                }
            }

        }
        //playerorientation forward hit something but playercameraforward did NOT
        else if (playerOrientationHit && !camRayHit)
        {
            //Debug.Log("2");
            dash.SetNewPosition(movementPosition + playerCameraForward * dashDistance);
        }

        //playerorientation forward did NOT hit something but the playercameraforward did
        else if (!playerOrientationHit && camRayHit)
        {
           //Debug.Log("3");
           //mainly used for ariel dashing on this else if block
            if (isGrounded)
            {
               // Debug.Log("3.1");
                dash.SetNewPosition(movementPosition + playerOrientationForward * dashDistance);
            }
               
            else 
            {
               // Debug.Log("3.2");
                dash.SetNewPosition(movementPosition + playerCameraForward * (dashDistance / 6));
            }
            
        }

        else if(!playerOrientationHit && !camRayHit)
        {
           // Debug.Log("4");
            
            if (isGrounded)
            {
                if(Vector3.Angle(movement.transform.up, playerCameraForward) >= 94)
                {
                    //Debug.Log("4.1");
                    inclinedDownForward = new Vector3(playerCameraForward.x, -0.3f, playerCameraForward.z);
                    dash.SetNewPosition(movementPosition + inclinedDownForward * dashDistance);

                }
                else
                {
                   // Debug.Log("4.2");
                    dash.SetNewPosition(movementPosition + playerOrientationForward * dashDistance);
                  
                }
                
            }
            else
            {
               // Debug.Log("4.3");
                dash.SetNewPosition(movementPosition + (playerCameraForward * dashDistance));

            }
        }
        player.GetComponent<Rigidbody>().velocity *= 0.15f;
    }

}

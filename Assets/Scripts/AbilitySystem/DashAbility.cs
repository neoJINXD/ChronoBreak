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
    private Rigidbody rb;

    //TODO can rework this whole thing reeeee
    public override void Initialize(GameObject obj)
    {
        player = obj;
        movement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        dash = player.GetComponent<TriggerDash>();
        rb = player.GetComponent<Rigidbody>();
    }

    public override void TriggerAbility()
    {

        movementPosition = player.transform.position;

        playerOrientationForward = movement.getOrientationDirection();
        playerCameraForward = movement.getPlayerCam().transform.forward;

        groundedVector = -(movement.transform.up);

        bool playerOrientationHit = Physics.Raycast(movementPosition, playerOrientationForward, out RaycastHit hit, 12);
        bool camRayHit = Physics.Raycast(movementPosition, playerCameraForward, 10);
        bool isGrounded = Physics.Raycast(movementPosition, groundedVector, 1.5f);

        // does a dash based on what the orientation forward and playerCam forward are hitting
        // each condition has a special modification done on how far the dash will take the player based on what the ray cast hits
        // raycast both hit something
        //Debug.Log(playerOrientationHit);
        //Debug.Log(camRayHit);
        
        if (playerOrientationHit && camRayHit)
        {
            // Debug.Log(hit.collider.tag);
            Debug.Log("1");
            // Debug.Log(hit.collider.tag);
            // Debug.Log(distance);
            dash.SetNewPosition(movementPosition + playerCameraForward * dashDistance);
            rb.AddForce(playerOrientationForward * dashDistance);

        }
        //playerorientation forward hit something but playercameraforward did NOT
        else if (playerOrientationHit && !camRayHit)
        {
            //Debug.Log(hit.collider.tag);
            Debug.Log("2");
            dash.SetNewPosition(movementPosition + playerCameraForward * dashDistance);
            rb.AddForce(playerCameraForward * dashDistance);
        }

        //playerorientation forward did NOT hit something but the playercameraforward did
        else if (!playerOrientationHit && camRayHit)
        {
            //Debug.Log(hit.collider.tag);
             Debug.Log("3");
            //mainly used for ariel dashing on this else if block
            if (isGrounded)
            {
                Debug.Log("3.1");
                dash.SetNewPosition(movementPosition + playerOrientationForward * dashDistance);
                rb.AddForce( playerOrientationForward * dashDistance);
            }
               
            else 
            {
               Debug.Log("3.2");
                dash.SetNewPosition(movementPosition + playerCameraForward * (dashDistance / 6));
                rb.AddForce(playerCameraForward * dashDistance);
            }
            
        }

        else if(!playerOrientationHit && !camRayHit)
        {
            
            Debug.Log("4");

            if (isGrounded)
            {
                if(Vector3.Angle(movement.transform.up, playerCameraForward) >= 94)
                {
                    Debug.Log("4.1");
                    inclinedDownForward = new Vector3(playerCameraForward.x, -0.3f, playerCameraForward.z);
                    dash.SetNewPosition(movementPosition + inclinedDownForward * dashDistance);
                    rb.AddForce(inclinedDownForward * dashDistance);

                }
                else
                {
                    Debug.Log("4.2");
                    dash.SetNewPosition(movementPosition + playerOrientationForward * dashDistance);
                    rb.AddForce(playerOrientationForward * dashDistance);
                }
                
            }
            else
            {
                Debug.Log("4.3");
                dash.SetNewPosition(movementPosition + (playerCameraForward * dashDistance));
                rb.AddForce(playerCameraForward * dashDistance);
            }
        }
      
    }

}

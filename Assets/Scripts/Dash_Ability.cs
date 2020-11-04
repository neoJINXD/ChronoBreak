using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash Ability")]
public class Dash_Ability : Ability
{
    public float dashDistance;
    private GameObject player;
    private PlayerMovement pmScript;
    private Vector3 inclinedDownForward;

    public override void Initialize(GameObject obj)
    {
        //obj should be a player gameObj refferenced in abilitycooldown.cs in
        //hierarchy, there should be a click
        //and drag to refer to this.
        player = obj;
        pmScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        
        //this is a special case vector used for raycast dashing adjustment
        

    }

    public override void TriggerAbility()
    {
        
        bool playerOrientationHit = Physics.Raycast(pmScript.transform.position, pmScript.getOrientationDirection(), out RaycastHit hit, (dashDistance+2));
        bool camRayHit = Physics.Raycast(pmScript.transform.position, pmScript.getPlayerCam().transform.forward, out RaycastHit hit2, dashDistance);


        //does a dash based on what the orientation forward and playerCam forward are hitting
        //each condition has a special modification done on how far the dash will take the player based on what the ray cast hits
       //raycast both hit something
        if (playerOrientationHit && camRayHit)
        {
            

          //  Debug.Log("1");
          //  Debug.Log(hit.collider.tag);
            float distance = Vector3.Distance(pmScript.transform.position, hit.collider.transform.position);
           // Debug.Log(distance);
            if (hit.collider.tag == "Ramp")
            {
                player.transform.position += pmScript.getOrientationDirection() * ((1f / 10) * dashDistance);
            }
            else
            {
                if (distance > 10.7)
                {
                  //  Debug.Log("a1");
                    player.transform.position += pmScript.getOrientationDirection() * ((7f / 10f) * dashDistance);
                }
                else if (distance > 8.5 && distance <= 10.7)
                {
                   // Debug.Log("a");
                    player.transform.position += pmScript.getOrientationDirection() * ((5f / 10f) * dashDistance);
                }
                else if (distance > 6.5 && distance <= 8.5)
                {
                   // Debug.Log("b");
                    player.transform.position += pmScript.getOrientationDirection() * ((3f / 10f) * dashDistance);
                }
                else if (distance > 5.6f && distance <= 6.5)
                {
                   // Debug.Log("c");
                    player.transform.position += pmScript.getOrientationDirection() * ((1f / 10f) * dashDistance);
                }
                else if (distance >= 0 && distance <= 5.6)
                {
                   // Debug.Log("d");
                    player.transform.position += pmScript.getOrientationDirection() * ((0.05f / 10) * dashDistance);
                }
                else
                {
                  //  Debug.Log("e");
                    player.transform.position += pmScript.getOrientationDirection() * dashDistance;
                }
            }

        }
        //playerorientation forward hit something but playercameraforward did NOT
        else if (playerOrientationHit && !camRayHit)
        {
           // Debug.Log("2");
            player.transform.position += pmScript.getPlayerCam().transform.forward * dashDistance;
        }
        //playerorientation forward did NOT hit something but the playercameraforward did
        else if (!playerOrientationHit && camRayHit)
        {
           // Debug.Log("3");
           //mainly used for ariel dashing on this else if block
            if (pmScript.isGrounded())
                player.transform.position += pmScript.getOrientationDirection() * dashDistance;
            else 
            {
                player.transform.position += pmScript.getPlayerCam().transform.forward * (dashDistance / 8);
            }
            
        }
        else if(!playerOrientationHit && !camRayHit)
        {
          // Debug.Log("4");

            if (pmScript.isGrounded())
            {
                if(Vector3.Angle(pmScript.transform.up, pmScript.getPlayerCam().transform.forward) >= 94)
                {
                    inclinedDownForward = new Vector3(pmScript.getPlayerCam().transform.forward.x, -0.3f, pmScript.getPlayerCam().transform.forward.z);
                    player.transform.position += inclinedDownForward * dashDistance;
                }
                else
                {
                    player.transform.position += pmScript.getOrientationDirection() * dashDistance;
                }
                
            }
            else
                player.transform.position += pmScript.getPlayerCam().transform.forward * dashDistance;

        }

    }

}

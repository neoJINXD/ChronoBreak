using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash Ability")]
public class Dash_Ability : Ability
{
    public float dashDistance;
    private GameObject player;
    private PlayerMovement pmScript;

    public override void Initialize(GameObject obj)
    {
        //obj should be a player gameObj refferenced in abilitycooldown.cs in
        //hierarchy, there should be a click
        //and drag to refer to this.
        player = obj;
        pmScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        
    }

    public override void TriggerAbility()
    {
        
        bool playerOrientationHit = Physics.Raycast(pmScript.transform.position, pmScript.getOrientationDirection(), out RaycastHit hit, (dashDistance+2));
        bool camRayHit = Physics.Raycast(pmScript.transform.position, pmScript.getPlayerCam().transform.forward, out RaycastHit hit2, dashDistance);

        if (playerOrientationHit && camRayHit)
        {
            //Debug.Log("1");
            if(hit.collider.tag == "Ramp")
            {
                //Debug.Log(hit.collider.tag);
                float x = pmScript.getPlayerCam().transform.forward.x;
                float y = Mathf.Abs(pmScript.getPlayerCam().transform.forward.y);
                float z = pmScript.getPlayerCam().transform.forward.z;

                Vector3 newForward = new Vector3(x, y + .1f, z);
               // Debug.Log(newForward);
                player.transform.position += newForward * dashDistance;
            }
            else
            {
               // Debug.Log(hit.collider.tag);
                float distance = Vector3.Distance(pmScript.transform.position, hit.collider.transform.position);
               // Debug.Log(distance);
                if(distance > 10 && distance <= 12)
                {
                   // Debug.Log("a1");
                    player.transform.position += pmScript.getOrientationDirection() * ((7f/10f) * dashDistance);
                }
                else if(distance > 8 && distance <= 10)
                {
                   // Debug.Log("a");
                    player.transform.position += pmScript.getOrientationDirection() * ((6f / 10f) * dashDistance);
                }
                else if(distance > 6 && distance <= 8)
                {
                  //  Debug.Log("b");
                    player.transform.position += pmScript.getOrientationDirection() * ((3f / 10f) * dashDistance);
                }
                else if (distance > 4.5f && distance <= 6)
                {
                  //  Debug.Log("c");
                    player.transform.position += pmScript.getOrientationDirection() * ((2f / 10f) * dashDistance);
                }
                else if (distance >=0  && distance <=4.5)
                {
                    //Debug.Log("d");
                    player.transform.position += pmScript.getOrientationDirection() * ((1.5f/10) *dashDistance);
                }
                else
                {
                   // Debug.Log("e");
                    player.transform.position += pmScript.getOrientationDirection() * dashDistance;
                }
                
            }
            

        }
        else if (playerOrientationHit && !camRayHit)
        {
          //  Debug.Log("2");
            player.transform.position += pmScript.getPlayerCam().transform.forward * dashDistance;
        }
        else if (!playerOrientationHit && camRayHit)
        {
           // Debug.Log("3");
            if (pmScript.isGrounded())
                player.transform.position += pmScript.getOrientationDirection() * dashDistance;
            else
                player.transform.position += pmScript.getPlayerCam().transform.forward * (dashDistance/3);
        }
        else if(!playerOrientationHit && !camRayHit)
        {
            //Debug.Log("4");

            if (pmScript.isGrounded())
                player.transform.position += pmScript.getOrientationDirection() * dashDistance;
            else
                player.transform.position += pmScript.getPlayerCam().transform.forward * dashDistance;

        }

    }

}

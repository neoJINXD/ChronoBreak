using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup2 : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] BoxCollider coll;
    [SerializeField] Transform player, gunContainer, fpsCam;

    [SerializeField] LayerMask pickupable;    

    [SerializeField] float pickUpRange, pickUpTime;
    [SerializeField] float dropForwardForce, dropUpwardForce;
    [SerializeField] float throwForwardForce, throwUpwardForce;

    [SerializeField] bool equipped;
    [SerializeField] static bool slotFull;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        if (!equipped)
        {
 
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            slotFull = true;
            rb.isKinematic = true;
            coll.isTrigger = true;

            // makes sure is attached to player
            Grab();
        }

    }
    void Update()
    {


        //Check if player is in range and "E" is pressed
        // TODO Should to move this to the player
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, 5f, pickupable) && 
                !equipped && !slotFull && Input.GetKeyDown(KeyCode.E))
            PickUp();
        // if (!equipped && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();

        //Throw if equipped and "R" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.R)) Throw();

        //TODO animate to the pickup position

    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        Grab();

        //Make Rigidbody Kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

    
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        // transform.position = gunContainer.position;
        // Detach from player
        transform.SetParent(null);

        // Make Rigidbody and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //Add force
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
   

    }

    private void Throw()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //Add force
        rb.AddForce(fpsCam.forward * throwForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * throwUpwardForce, ForceMode.Impulse);
        //Add random rotation
        rb.AddTorque(new Vector3(0.4f, 0.4f, 0.4f) * 10);

    }


    private void Grab()
    {
        //Make weapon a child of the camera and move it to the equippedPosition
        transform.SetParent(gunContainer);
        
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }

    public bool GetEquipped()
    {
        return equipped;
    }
}

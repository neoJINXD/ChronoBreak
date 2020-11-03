using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup2 : MonoBehaviour
{

    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange, pickUpTime;
    public float dropForwardForce, dropUpwardForce;
    public float throwForwardForce, throwUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
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
        }

    }
    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();

        //Throw if equipped and "R" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.R)) Throw();

    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to the equippedPosition
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody Kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

    
    }

    private void Drop()
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
}

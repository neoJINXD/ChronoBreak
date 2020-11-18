using UnityEngine;

// TODO breaking when restartign level, cant pickup
public class Pickup : MonoBehaviour
{
    // Assignables
    [SerializeField] Transform swordContainer, gunContainer;
    //TODO player and cam can prob be private
    [SerializeField] LayerMask pickupable;    

    [SerializeField] float pickUpRange, pickUpTime;
    // [SerializeField] float dropForwardForce, dropUpwardForce;
    // [SerializeField] float throwForwardForce, throwUpwardForce;

    [SerializeField] bool equipped;
    [SerializeField] static bool slotFull;
    
    // References
    private Rigidbody rb;
    private BoxCollider coll;
    private WeaponBase weapon;
    private Transform player, cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        player = GetComponent<Transform>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }
    
    void Update()
    {
        //Check if player is in range and "E" is pressed
        // TODO Should to move this to the player
        RaycastHit hit;
        Physics.Raycast(cam.position, cam.forward, out hit, pickUpRange, pickupable);
        
        // if (hit.collider && !equipped && !slotFull && Input.GetKeyDown(KeyCode.E))
        if (hit.collider && !equipped && Input.GetKeyDown(KeyCode.E))
        {
            equipped = true;
            
            //TODO not ideal to use get component in update?
            weapon = hit.collider.GetComponent<WeaponBase>();
            weapon.Pickup();

            weapon.transform.SetParent(swordContainer);
        
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
            weapon.transform.localScale = Vector3.one;
            //PickUp();
        }

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            weapon.Drop();//TODO maybe pass player.velocity in here instead of reference in weapon

            weapon.transform.SetParent(null);
            weapon = null;
            equipped = false;
        } 

        //Throw if equipped and "R" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.R)) 
        {
            weapon.Throw();//TODO maybe pass player.velocity in here instead of reference in weapon

            weapon.transform.SetParent(null);
            weapon = null;
            equipped = false;
        }

        if (equipped && Input.GetMouseButtonDown(0))
        {
            weapon.Attack();
        }

        //TODO animate to the pickup position
    }

    // private void PickUp()
    // {
    //     equipped = true;
    //     slotFull = true;

    //     Grab();

    //     //Make Rigidbody Kinematic and BoxCollider a trigger
    //     rb.isKinematic = true;
    //     coll.isTrigger = true;
    // }

    // private void Drop()
    // {
    //     equipped = false;
    //     slotFull = false;

    //     // transform.position = gunContainer.position;
    //     // Detach from player
    //     transform.SetParent(null);

    //     // Make Rigidbody and BoxCollider normal
    //     rb.isKinematic = false;
    //     coll.isTrigger = false;

    //     rb.velocity = player.GetComponent<Rigidbody>().velocity;

    //     //Add force
    //     rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
    //     rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

    //     //Add random rotation
    //     float random = Random.Range(-1f, 1f);
    //     rb.AddTorque(new Vector3(random, random, random) * 10);
    // }

    // private void Throw()
    // {
    //     equipped = false;
    //     slotFull = false;

    //     //Set parent to null
    //     transform.SetParent(null);

    //     //Make Rigidbody and BoxCollider normal
    //     rb.isKinematic = false;
    //     coll.isTrigger = false;

    //     rb.velocity = player.GetComponent<Rigidbody>().velocity;

    //     //Add force
    //     rb.AddForce(fpsCam.forward * throwForwardForce, ForceMode.Impulse);
    //     rb.AddForce(fpsCam.up * throwUpwardForce, ForceMode.Impulse);
    //     //Add random rotation
    //     rb.AddTorque(new Vector3(0.4f, 0.4f, 0.4f) * 10);
    // }


    // private void Grab()
    // {
    //     //Make weapon a child of the camera and move it to the equippedPosition
    //     transform.SetParent(gunContainer);
        
    //     transform.localPosition = Vector3.zero;
    //     transform.localRotation = Quaternion.Euler(Vector3.zero);
    //     transform.localScale = Vector3.one;
    // }

    // public bool GetEquipped()
    // {
    //     return equipped;
    // }
}

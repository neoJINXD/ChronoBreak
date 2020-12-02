using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Assignables
    [SerializeField] Transform swordContainer, gunContainer;
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

    //TODO Might have to move the guard colider somehow
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
        RaycastHit hit;
        RaycastHit hit2;
        Physics.Raycast(cam.position, cam.forward, out hit, pickUpRange, pickupable);
        Physics.Raycast(cam.position, cam.forward, out hit2);

        bool isInHardcoreMode = GameManager.instance.hardcoreMode;

        if (!isInHardcoreMode && hit2.collider != null && !equipped && Input.GetKeyDown(KeyCode.E))
        {
            if (hit2.collider.CompareTag("CanGrab"))
            {
                print($"we looking at {hit2.collider.name}");
                equipped = true;
                // print($"yes {hit.collider.gameObject.name}");
                
                weapon = hit2.collider.GetComponent<WeaponBase>();
                weapon.Pickup();

                // if (hit2.collider.CompareTag(""))
                if (hit2.collider.gameObject.name.Contains("Gun"))
                {
                    weapon.transform.SetParent(gunContainer);
                }
                else
                {
                    weapon.transform.SetParent(swordContainer);
                    weapon.transform.localScale = Vector3.one;
                }
                //TODO playerpickup sound
            
                // weapon.transform.localPosition = Vector3.zero;
                // weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
        }

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            weapon.Drop(rb.velocity);

            weapon.transform.SetParent(null);
            weapon = null;
            equipped = false;
        } 

        //Throw if equipped and "R" is pressed
        if (equipped && Input.GetKey(KeyCode.R)) 
        {
            weapon.Predict(swordContainer.position);
        }
        if (equipped && Input.GetKeyUp(KeyCode.R)) 
        {
            weapon.Throw(rb.velocity);

            weapon.transform.SetParent(null);
            weapon = null;
            equipped = false;
        }

        if (equipped && Input.GetMouseButton(0))
        {
            weapon.Attack();
        }
        

        // animate to the pickup position
        if (equipped)
        {
            weapon.transform.localPosition = Vector3.Lerp(weapon.transform.localPosition, Vector3.zero, pickUpTime * Time.deltaTime);
            weapon.transform.localRotation = Quaternion.Lerp(weapon.transform.localRotation, Quaternion.Euler(Vector3.zero), pickUpTime * Time.deltaTime);
        }
    }

}

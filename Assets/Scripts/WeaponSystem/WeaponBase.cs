using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{

    // Assignables
    [SerializeField] float randomForce;
    [SerializeField] float dropForwardForce, dropUpwardForce;
    [SerializeField] float throwForwardForce, throwUpwardForce;

    [SerializeField] Rigidbody playerRb;

    // References
    [SerializeField]protected Collider coll;
    [SerializeField]protected Rigidbody rb;
    [SerializeField]protected Transform cam;

    private bool thrown = false;
    private Vector3 force;

    protected Animation anim;

    virtual protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        anim = GetComponent<Animation>();
    }

    virtual protected void FixedUpdate()
    {
        if (thrown)
        {
            
            // print("reeeeeeeeee");
            //TODO allign point of weapon towards velocity
            // Vector3.RotateTowards(transform.rotation.eulerAngles, rb.velocity, 6f, 2f);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rb.velocity.x, rb.velocity.y, rb.velocity.z), Time.deltaTime * 100f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rb.velocity.x, rb.velocity.y, rb.velocity.z), Time.deltaTime * 10f);

        }
    }

    public virtual void Attack() {}

    public void Pickup()
    {
        rb.isKinematic = true;
        coll.isTrigger = true;

        thrown = false;

        coll.enabled = false;
        // anim.enabled = true;
    }

    public void Drop()
    {
        rb.isKinematic = false;
        coll.isTrigger = false;
        
        rb.velocity = playerRb.velocity;

        //Add force
        rb.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);

        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * randomForce);
        
        coll.enabled = true;
    }

    public void Throw()
    {
        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = playerRb.GetComponent<Rigidbody>().velocity;

        //Add force
        // rb.AddForce(cam.forward * throwForwardForce, ForceMode.Impulse);
        // rb.AddForce(cam.up * throwUpwardForce, ForceMode.Impulse);
        rb.AddForce(force, ForceMode.Impulse);
        
        //Add random rotation
        // rb.AddTorque(new Vector3(0.4f, 0.4f, 0.4f) * randomForce);
        thrown = true;
        
        coll.enabled = true;
        Trajectory.instance.ResetLine();
    }


    void OnCollisionEnter(Collision other) 
    {
        thrown = false;    
    }

    public void Predict(Vector3 pos)
    {
        force = cam.forward * throwForwardForce + cam.up * throwUpwardForce;
        // Instantiate(gameObject, transform.position, Quaternion.identity);
        // Trajectory.instance.Predict(gameObject, transform.position, force);
        Trajectory.instance.Predict(gameObject, pos, force);
    }
}

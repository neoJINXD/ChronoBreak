using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{

    // Assignables
    [SerializeField] float randomForce;
    [SerializeField] float dropForwardForce, dropUpwardForce;
    [SerializeField] float throwForwardForce, throwUpwardForce;
    [SerializeField] Vector3 offset; //TODO need to set this per model cuz reeeeeee

    // References
    protected Collider coll;
    protected Animation anim;
    protected Rigidbody rb;
    protected Transform cam;
    private Vector3 force;


    private bool thrown = false;


    virtual protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        anim = GetComponent<Animation>();
    }

    void FixedUpdate()
    {
        // Debug.DrawRay(transform.position, rb.velocity * 1000f, Color.red);
        // Debug.DrawLine(transform.position, transform.position + rb.velocity, Color.red);
        // print(rb.velocity);
        if (thrown)
        {
            
            //TODO it dont wanna work :c
            //TODO allign point of weapon towards velocity
            // Vector3.RotateTowards(transform.rotation.eulerAngles, rb.velocity, 6f, 2f);
            // Vector3 temp = transform.position + rb.velocity + offset;
            // print($"Velocity is {temp}");
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rb.velocity.x, rb.velocity.y, rb.velocity.z), Time.deltaTime * 10000f);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(temp.x, temp.y, temp.z), Time.deltaTime * 10000f);
            // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(temp.x, temp.y, temp.z), Time.deltaTime * 10f);

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

    public void Drop(Vector3 vel)
    {
        rb.isKinematic = false;
        coll.isTrigger = false;
        
        rb.velocity = vel;

        //Add force
        rb.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);

        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * randomForce);
        
        coll.enabled = true;
    }

    public void Throw(Vector3 vel)
    {
        rb.isKinematic = false;
        coll.isTrigger = false;

        
        transform.localRotation = Quaternion.Euler(offset);
        rb.velocity = vel;

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

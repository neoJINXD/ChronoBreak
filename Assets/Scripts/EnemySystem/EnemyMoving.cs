using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    // Assignables
    [SerializeField] GameObject player;
    [SerializeField] float speed = 5f;
    [SerializeField] float followRadius = 15f;
    [SerializeField] float angularSpeed = 1f;
    [SerializeField] Timer timer;

    // References
    private Rigidbody rb;
    private bool freeMove = true;
    private LinkedList<Vector3> positions;
    private Vector3 pos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        positions = new LinkedList<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if ((distance < followRadius))
        {
            if(freeMove)
            {
                rb.constraints = RigidbodyConstraints.None;

                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

                Vector3 rel = (player.transform.position - transform.position).normalized;
                rel.y = 0;
                Quaternion desiredRotation = Quaternion.LookRotation(rel, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, angularSpeed); //TODO should use Time.deltaTime

                positions.AddLast(transform.position);
                if(positions.Count > 20)
                {
                    positions.RemoveFirst();
                }
            }
        }
        else
        {
           if(rb.velocity.magnitude < 6.5f)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        if(rb.velocity.magnitude > 0f)
        {
            freeMove = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag =="Player")
        {
            //increase time
            Debug.Log("Hit Player");
        }

        if (collision.collider.tag == "Wall")
        {
            freeMove = false;
            
            pos = positions.First.Value;
            positions = null;
            positions = new LinkedList<Vector3>();
            positions.AddLast(pos);
            transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("CanGrab")) // if is sword
        {
            Destroy(gameObject);
            timer.CountEvent("shooting enemy kill");
        }
    }

}

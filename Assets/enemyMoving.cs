using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMoving : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject player;
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float followRadius = 15f;
    [SerializeField]
    float angularSpeed = 1f;
    float distance;
    Rigidbody rb;
    bool freeMove = true;
    LinkedList<Vector3> positions;
    Vector3 pos;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        positions = new LinkedList<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        if ((distance < followRadius))
        {
            if(freeMove)
            {
                rb.constraints = RigidbodyConstraints.None;
                //rb.constraints = constraints;
                //Debug.Log("NOne");

                this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed);
                Vector3 rel = (player.transform.position - this.transform.position).normalized;
                rel.y = 0;
                Quaternion desiredRotation = Quaternion.LookRotation(rel, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, angularSpeed);
                positions.AddLast(this.transform.position);
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

            //Debug.Log("All");
        }

        if(rb.velocity.magnitude > 0f)
        {
            freeMove = true;
        }

        Debug.Log(rb.velocity.magnitude);
        
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
            this.transform.position = pos;
        }
    }

}

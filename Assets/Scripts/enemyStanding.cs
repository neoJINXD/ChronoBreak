using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStanding : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject player;
    [SerializeField]
    float followRadius = 15f;
    [SerializeField]
    float angularSpeed = 1f;
    Rigidbody rb;

    [SerializeField] Timer timer;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < followRadius)
        {
            rb.constraints = RigidbodyConstraints.None;
            //this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed);
            Vector3 rel = (player.transform.position - this.transform.position).normalized;
            rel.y = 0;
            Quaternion desiredRotation = Quaternion.LookRotation(rel, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, angularSpeed);

            //Debug.Log(rel);
        }
        else
        {
            if (rb.velocity.magnitude < 6.5f)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            //increase time
            Debug.Log("Hit Player");
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("CanGrab")) // if is sword
        {
            Destroy(gameObject);
            timer.CountEvent("standing enemy kill");
        }
    }
}

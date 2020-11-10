using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStanding : MonoBehaviour
{
    // Assignables
    [SerializeField] GameObject player;
    [SerializeField] float followRadius = 15f;
    [SerializeField] float angularSpeed = 1f;
    [SerializeField] Timer timer;
    
    // References
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < followRadius)
        {
            rb.constraints = RigidbodyConstraints.None;
            //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
            Vector3 rel = (player.transform.position - transform.position).normalized;
            rel.y = 0;
            Quaternion desiredRotation = Quaternion.LookRotation(rel, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, angularSpeed * Time.deltaTime);

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
            timer.CountEvent("standing enemy touched");
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

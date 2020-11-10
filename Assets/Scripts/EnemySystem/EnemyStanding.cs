using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStanding : EnemyBase
{
    // Assignables
    [SerializeField] GameObject player;
    [SerializeField] float followRadius = 15f;
    [SerializeField] float angularSpeed = 1f;
    
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    // Assignables
    [SerializeField] string type;
    [SerializeField] Timer timer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            //increase time
            // Debug.Log("Hit Player");
            timer.CountEvent(type + " touched");
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("CanGrab")) // if is sword
        {
            Destroy(gameObject);
            timer.CountEvent(type + " kill");
        }
    }
}

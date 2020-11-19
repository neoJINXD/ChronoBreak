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
            //Trigger enemy death sound effect
            FindObjectOfType<AudioManager>().Play("EnemyDying");
            Destroy(gameObject);
            timer.CountEvent(type + " kill");
        }
    }
}

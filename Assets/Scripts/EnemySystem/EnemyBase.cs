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

        // For thrown weapon
        if (collision.collider.CompareTag("CanGrab"))
        {
            Destroy(gameObject);
            timer.CountEvent(type + " kill");
        }

        if (collision.collider.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            timer.CountEvent(type + " kill");
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

    private void OnDestroy() {
       // AudioManager.instance.Play("EnemyDeath");
    }
}

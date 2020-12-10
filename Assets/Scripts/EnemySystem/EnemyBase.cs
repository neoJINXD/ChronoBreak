using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    // Assignables
    [SerializeField] string type;
    [SerializeField] Timer timer;
    [SerializeField] int health = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            //increase time
            // Debug.Log("Hit Player");
            timer.CountEvent(type + " touched");
        }

        // For thrown weapon
        if (collision.collider.CompareTag("CanGrab") && collision.gameObject.GetComponent<WeaponBase>().thrown)
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<WeaponBase>().thrown = false;


        }

        if (collision.collider.CompareTag("Bullet"))
        {
            health--;
            //Debug.Log("Enemy hit");
            if (health < 1)
            {
                Destroy(gameObject);
            }

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("CanGrab")) // if is sword
        {
            health--;
            // Debug.Log("Enemy hit");
            if (health < 1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy() {
        // AudioManager.instance.Play("EnemyDeath");
        timer.CountEvent(type + " kill");
        timer.EnemyHit();
    }
}

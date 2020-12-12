using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    // Assignables
    [SerializeField] string type;
    [SerializeField] Timer timer;
    [SerializeField] int health = 2;
    [SerializeField] protected NavMeshAgent agent;

    protected bool death = false;

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
            timer.EnemyHit();
            if(agent != null)
            {
                agent.SetDestination(transform.position);
            }
            death = true;
            // Destroy(gameObject);
            Die();
            collision.gameObject.GetComponent<WeaponBase>().thrown = false;


        }

        if (collision.collider.CompareTag("Bullet"))
        {
            timer.EnemyHit();
            health--;
            //Debug.Log("Enemy hit");
            if (health == 0)
            {
                // Destroy(gameObject);
                if (agent != null)
                {
                    agent.SetDestination(transform.position);
                }
                death = true;
                Die();
            }

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("CanGrab")) // if is sword
        {
            timer.EnemyHit();
            health--;
            // Debug.Log("Enemy hit");
            if (health == 0)
            {
                // Destroy(gameObject);
                Die();
            }
        }
    }

    private void Die()
    {
        // AudioManager.instance.Play("EnemyDeath");
        timer.CountEvent(type + " kill");
        
        // MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        // Material mat = new Material(meshRenderer.material);
        // meshRenderer.material = mat;
        // StartCoroutine(FadeOut(mat));
        GetComponent<Animator>().SetTrigger("DeathTrigger");
        Destroy(gameObject, 2.5f);
    }

    // private IEnumerator FadeOut(Material mat)
    // {
    //     mat.SetColor("_Color", mat.GetColor("_Color") - 0.1f);
    //     yield return null;
    // }

    private void OnDestroy() {
        // AudioManager.instance.Play("EnemyDeath");
        // timer.CountEvent(type + " kill");
        // timer.EnemyHit();
    }
}

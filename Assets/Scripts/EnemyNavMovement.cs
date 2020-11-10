using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyNavMovement : MonoBehaviour
{
    // Assignables
    [SerializeField] NavMeshAgent agent;

    [SerializeField] GameObject player;

    [SerializeField] ThirdPersonCharacter character;

    [SerializeField] float followRadius = 100f;

    [SerializeField] Timer timer;

    void Update()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) < followRadius)
        {
            agent.SetDestination(player.transform.position);
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, true);
        }
        else
        {
            character.Move(Vector3.zero, false, true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            //increase time
            Debug.Log("Hit Player");
            timer.CountEvent("chasing enemy touched");
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("CanGrab")) // if is sword
        {
            Destroy(gameObject);
            timer.CountEvent("chasing enemy kill");
        }
    }
}

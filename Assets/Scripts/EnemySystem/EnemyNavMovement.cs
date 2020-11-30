using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyNavMovement : EnemyBase
{
    // Assignables
    [SerializeField] NavMeshAgent agent;

    [SerializeField] GameObject player;

    [SerializeField] ThirdPersonCharacter character;

    [SerializeField] float followRadius = 100f;

    private void Start()
    {
        agent.avoidancePriority = Random.Range(0,99);
        NavMesh.avoidancePredictionTime = 10f;
    }
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
}

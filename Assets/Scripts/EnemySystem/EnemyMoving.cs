using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyMoving : EnemyBase
{
    // Assignables
    [SerializeField] GameObject player;
    [SerializeField] float followRadius = 15f;
    [SerializeField] ThirdPersonCharacter character;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float runawayPointDistance = 15f;


    //Referneces 
    bool isFollowing;

    private void Start()
    {
        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        print("Enemy2 distance: " + Vector3.Distance(this.transform.position, player.transform.position));

        if ((Vector3.Distance(this.transform.position, player.transform.position) < followRadius)&& (Vector3.Distance(this.transform.position, player.transform.position) > agent.stoppingDistance -2.1)&&isFollowing)
        {
            agent.SetDestination(player.transform.position);
        }

        if((Vector3.Distance(this.transform.position, player.transform.position) < agent.stoppingDistance -2.1)&&isFollowing)
        {
            agent.SetDestination(RandomNavmeshLocation(runawayPointDistance));
            isFollowing = false;
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, true);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
            isFollowing = true;
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 directionOfTravel = this.transform.position - player.transform.position;
        Vector3 finalDirection = directionOfTravel + directionOfTravel.normalized * radius;
        Vector3 targetPosition = player.transform.position + finalDirection;

        Vector3 randomDirection = Random.insideUnitSphere * (radius/2f);
        randomDirection = targetPosition + randomDirection;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, (radius/2f), 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}

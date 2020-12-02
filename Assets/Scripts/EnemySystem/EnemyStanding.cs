using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyStanding : EnemyBase
{
    // Assignables
    [SerializeField] GameObject player;
    [SerializeField] float followRadius = 15f;
    [SerializeField] float angularSpeed = 1f;
    [SerializeField] ThirdPersonCharacter character;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < followRadius)
        {
            //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
            Vector3 rel = (player.transform.position - transform.position).normalized;
            rel.y = 0;
            Quaternion desiredRotation = Quaternion.LookRotation(rel, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, angularSpeed * Time.deltaTime);

            //Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        }
        else
        {
            character.Move(Vector3.zero, false, true);
        }
    }
}

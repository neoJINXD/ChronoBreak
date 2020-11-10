using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Assignables
    [SerializeField] Timer timer;

    private void OnTriggerEnter(Collider other)
    {   
        // Detect collision with player
        // Set timer state to "Completed" if player reaches the goal
        if (other.gameObject.CompareTag("Player"))
        {
            timer.SetCompleted(true);
        }
    }
}

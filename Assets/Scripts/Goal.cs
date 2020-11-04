using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField] Timer timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Detect collision with player
    private void OnTriggerEnter(Collider other)
    {   
        // Set timer state to "Completed" if player reaches the goal
        if (other.gameObject.CompareTag("Player"))
        {
            timer.SetCompleted(true);
        }
    }
}

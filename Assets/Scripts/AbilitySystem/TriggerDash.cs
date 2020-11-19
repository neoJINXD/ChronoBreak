using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDash : MonoBehaviour
{
    
    [SerializeField] Transform player;

    private Vector3 destination, startingPosition;
    void Start()
    {
        startingPosition = player.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallZone : MonoBehaviour
{
    void OnTriggerEnter(Collider col) 
    {
        if (col.CompareTag("Player"))
            col.GetComponent<PlayerMovement>().LoadSafePos();
    }
}   

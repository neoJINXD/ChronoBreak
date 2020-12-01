using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLine : MonoBehaviour
{
    // Just use this script to see where the forward is for any gameobject
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 1000f, Color.green);
    }
}

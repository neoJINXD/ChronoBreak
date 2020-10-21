using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform player;
    void Update()
    {
        transform.position = player.transform.position;
        
    }
}

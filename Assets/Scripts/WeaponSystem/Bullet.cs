using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    // private Vector3 _dir ;
    public Vector3 dir { get; set; }

    void FixedUpdate()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
        //TODO Add enemy tag to enemy prefabs   
        if (other.collider.CompareTag("Enemy"))
        {
            Destroy(other.collider.gameObject);
            
        }   
    }
}

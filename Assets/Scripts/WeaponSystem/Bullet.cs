using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float life;

    private float timer;

    // private Vector3 _dir ;
    public Vector3 dir { get; set; }

    void Start()
    {
        timer = 0;
    }


    void FixedUpdate()
    {
        timer += Time.deltaTime;
        transform.position += dir * speed * Time.deltaTime;

        if (timer > life)
        {
            Destroy(gameObject);
        }
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

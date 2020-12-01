using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float life;
    [SerializeField] ParticleSystem impact;

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
    
        else
        {
            ParticleSystem eff = Instantiate(impact, transform.position, Quaternion.identity);
            eff.gameObject.transform.forward = other.contacts[0].normal;
            Destroy(eff.gameObject, impact.main.duration);
        }
    }
}

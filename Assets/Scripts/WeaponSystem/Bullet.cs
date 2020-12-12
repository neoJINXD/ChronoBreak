using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float life;
    [SerializeField] ParticleSystem impact;

    private float timer;

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
        // Debug.DrawRay(transform.position, transform.forward * 1000f, Color.green);
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
        if (!other.collider.CompareTag("Enemy"))
        {
            ParticleSystem eff = Instantiate(impact, transform.position, Quaternion.identity);
            eff.gameObject.transform.forward = other.contacts[0].normal;
            Destroy(eff.gameObject, impact.main.duration);
        }
    }
}

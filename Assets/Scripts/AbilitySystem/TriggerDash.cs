using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDash : MonoBehaviour
{
    
    [SerializeField] float speed;
    [SerializeField] ParticleSystem dashEffect;

    private Vector3 newPosition;
    private bool isDashing = false;


    void FixedUpdate()
    {
        PositionChanging();
    }

    void PositionChanging()
    {
        if (isDashing)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
        }
        if (Vector3.Distance(transform.position, newPosition) < .7f)
        {
            isDashing = false;
        }
    }

    public void SetNewPosition( Vector3 destination)
    {
        isDashing = true;
        newPosition = destination;
        dashEffect.Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDash : MonoBehaviour
{
    
    [SerializeField] float dashForce;
    [SerializeField] float dashDuration;
    [SerializeField] ParticleSystem dashEffect;
    [SerializeField] Transform playerCam;

    private Rigidbody rb;
    private bool dashTriggered, isDashing;
    float originalDashForce;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dashTriggered = false;
        isDashing = false;
        originalDashForce = dashForce;
    }

    private void Update()
    {
        if (dashTriggered && !isDashing)
        {
            PlayEffect();
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true; 
        rb.AddForce(playerCam.transform.forward * dashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;
        dashTriggered = false;
        isDashing = false;
    }


    private void PlayEffect()
    {
        dashEffect.Play();
    }

    public void beginDash()
    {
        dashTriggered = true;
        if(Physics.Raycast(transform.position, playerCam.forward, 9))
        {
            dashForce /= 1.70f;
        }
        else
        {
            dashForce = originalDashForce;
        }
    }

}

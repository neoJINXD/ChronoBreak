using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDash : MonoBehaviour
{
    
    [SerializeField] float smoothness;
    [SerializeField] GameObject camera;

    private Vector3 newPosition;
    private bool isDashing = false;

    private LineRenderer[] lines;

    private void Start()
    {
        lines = camera.GetComponentsInChildren<LineRenderer>();
    }
    private void FixedUpdate()
    {
        PositionChanging();
    }

    void PositionChanging()
    {
        if (isDashing)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * smoothness);
        }
        if (Vector3.Distance(transform.position, newPosition) < .7f)
        {
            isDashing = false;
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].enabled = false;
            }
        }

    }

    public void SetNewPosition( Vector3 destination)
    {
        isDashing = true;
        newPosition = destination;
        for (int i=0; i< lines.Length; i++)
        {
            lines[i].enabled = true;
        }
    }

}

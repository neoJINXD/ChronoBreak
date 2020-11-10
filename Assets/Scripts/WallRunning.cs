using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    // Assignables
    [Header("Wallrun Forces")]
    [SerializeField] float wallRunUpForce; // Staying up
    [SerializeField] float wallRunHoldForce; // Staying Stuck to wall
    [SerializeField] float wallRunPushForce; // Jumping off force
    
    [Header("Camera Settings")]
    [SerializeField] float maxWallRunCamTilt; // Angle for the camera while wallrunning
    [SerializeField] Transform mainCamera;
    // [SerializeField] Transform cam; // is Main Camera
    private float wallRunCamTilt;

    [Header("Other")]
    [SerializeField] Transform orientation; // is Orientation
    [SerializeField] Transform wallDirection;
    
    // References
    private Rigidbody rb;
    private bool isRightWall, isLeftWall, isWallRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckWall();
        
        if (!isWallRunning)
        {
            wallDirection.rotation = orientation.rotation;
        }

        // TODO this way of sticking to wall doesnt allow free movement of cam while running

        if(isRightWall && isWallRunning)
        {
            rb.AddForce(wallDirection.right * wallRunHoldForce * Time.deltaTime);
        }
        if(isLeftWall && isWallRunning)
        {
            rb.AddForce(-wallDirection.right * wallRunHoldForce * Time.deltaTime);
        }  


        // Camera Rotations 
        if (Math.Abs(wallRunCamTilt) < maxWallRunCamTilt && isWallRunning && isRightWall)
        {
            wallRunCamTilt += Time.deltaTime * maxWallRunCamTilt * 10;
        }
        if (Math.Abs(wallRunCamTilt) < maxWallRunCamTilt && isWallRunning && isLeftWall)
        {
            wallRunCamTilt -= Time.deltaTime * maxWallRunCamTilt * 10;
        }

        if (wallRunCamTilt > 0 && !isRightWall && !isLeftWall)
        {
            wallRunCamTilt -= Time.deltaTime * maxWallRunCamTilt * 10;
        }
        if (wallRunCamTilt < 0 && !isRightWall && !isLeftWall)
        {
            wallRunCamTilt += Time.deltaTime * maxWallRunCamTilt * 10;
        }

        Vector3 currentAngles = mainCamera.transform.rotation.eulerAngles;
        currentAngles.z = wallRunCamTilt;

        mainCamera.transform.rotation = Quaternion.Euler(currentAngles);

    }

    private void CheckWall()
    {
        // Debug.DrawRay(orientation.transform.position, orientation.transform.right, Color.red);
        // Debug.DrawRay(orientation.transform.position, -orientation.transform.right, Color.red);

        if (Physics.Raycast(orientation.transform.position, orientation.transform.right, 2f))
        {
            // Check right side of player
            isRightWall = true;
            isLeftWall = false;
        }
        else if (Physics.Raycast(orientation.transform.position, -orientation.transform.right, 2f))
        {
            // Check left side of player
            isLeftWall = true;
            isRightWall = false;
        }
        else
        {
            // No walls, reset
            isRightWall = false;
            isLeftWall = false;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("WallRunnable"))
        {
            isWallRunning = true;
            // print("brr");
            // rb.useGravity = false;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        //TODO might be able to optimize the if's here, dont wanna end up like YandereDev
        if (collision.transform.CompareTag("WallRunnable"))
        {
            if (Input.GetKey(KeyCode.Space) && isLeftWall)
            {
                rb.AddForce(Vector3.up * wallRunUpForce, ForceMode.Impulse);
                rb.AddForce(orientation.transform.right * wallRunUpForce, ForceMode.Impulse);
                print("Jumping off left wall");
                EndWallRun();
            }
            if (Input.GetKey(KeyCode.Space) && isRightWall)
            {
                rb.AddForce(Vector3.up * wallRunUpForce, ForceMode.Impulse);
                rb.AddForce(-orientation.transform.right * wallRunUpForce, ForceMode.Impulse);
                print("Jumping off right wall");
                EndWallRun();   
            }
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("WallRunnable"))
        {
            EndWallRun();
        }
    }

    private void EndWallRun()
    {
        // cam.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        isWallRunning = false;
        // rb.useGravity = true;
    }
}

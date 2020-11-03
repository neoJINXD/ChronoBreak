﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [SerializeField] float wallRunUpForce; // Staying up
    [SerializeField] float wallRunForce; // Staying Stuck to wall
    [SerializeField] float wallRunPushForce; // Jumping off force
    [SerializeField] float wallRunCamTilt; // 
    [SerializeField] float maxWallRunCamTilt; // 
    [SerializeField] Transform mainCamera;

    [SerializeField]private bool isRightWall, isLeftWall, isWallRunning;

    private float distanceFromLeftWall, distanceFromRightWall;

    private Rigidbody rb;
    [SerializeField] Transform cam; // is Main Camera
    [SerializeField] Transform orientation; // is Orientation

    [SerializeField] Transform wallDirection;
    private GameObject rightTilt;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // leftTilt = new GameObject();
        // leftTilt.transform.localEulerAngles = new Vector3(0f, 0f, -10f);

        // rightTilt = new GameObject();
        // rightTilt.transform.localEulerAngles = new Vector3(0f, 0f, 10f);

    }

    void Update()
    {
        CheckWall();
        
        if (!isWallRunning)
            wallDirection.rotation = orientation.rotation;

        // TODO this way of sticking to wall doesnt allow free movement of cam while running
        // maybe change which object is taking the right from
        if(isRightWall && isWallRunning)
        {
            rb.AddForce(wallDirection.right * wallRunForce * Time.deltaTime);
        }
        if(isLeftWall && isWallRunning)
        {
            rb.AddForce(-wallDirection.right * wallRunForce * Time.deltaTime);
        }  

        print(wallRunCamTilt);

        // Rotations 
        //TODO might need to combine with movement script to get working
        // //Tilts camera in .5 second
        if (Math.Abs(wallRunCamTilt) < maxWallRunCamTilt && isWallRunning && isRightWall)
            wallRunCamTilt += Time.deltaTime * maxWallRunCamTilt * 10;
        if (Math.Abs(wallRunCamTilt) < maxWallRunCamTilt && isWallRunning && isLeftWall)
            wallRunCamTilt -= Time.deltaTime * maxWallRunCamTilt * 10;

        // //Tilts camera back again
        if (wallRunCamTilt > 0 && !isRightWall && !isLeftWall)
            wallRunCamTilt -= Time.deltaTime * maxWallRunCamTilt * 10;
        if (wallRunCamTilt < 0 && !isRightWall && !isLeftWall)
            wallRunCamTilt += Time.deltaTime * maxWallRunCamTilt * 10;
        Vector3 currentAngles = mainCamera.transform.rotation.eulerAngles;
        currentAngles.z = wallRunCamTilt;
        mainCamera.transform.rotation = Quaternion.Euler(currentAngles);
    }

    private void CheckWall()
    {
        Debug.DrawRay(orientation.transform.position, orientation.transform.right, Color.red);
        Debug.DrawRay(orientation.transform.position, -orientation.transform.right, Color.red);

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
            isRightWall = false;
            isLeftWall = false;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("WallRunnable"))
        {
            isWallRunning = true;
            rb.useGravity = false;

            if (isLeftWall)
            {
                cam.transform.localEulerAngles = new Vector3(0f, 0f, -10f);
                // Quaternion.Lerp(cam.transform.rotation, leftTilt.transform.rotation, 500 * Time.deltaTime);
            }
            if (isRightWall)
            {
                cam.transform.localEulerAngles = new Vector3(0f, 0f, 10f);
                // Quaternion.Lerp(cam.transform.rotation, rightTilt.transform.rotation, 500 * Time.deltaTime);
            }
        }
    }
    void OnCollisionStay(Collision collision)
    {
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
        cam.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        isWallRunning = false;
        rb.useGravity = true;
    }

}

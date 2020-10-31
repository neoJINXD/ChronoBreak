using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [SerializeField] float wallRunUpForce; // Staying up
    [SerializeField] float wallRunPushForce; // Jumping off force

    private bool isRightWall, isLeftWall, isWallRunning;

    private float distanceFromLeftWall, distanceFromRightWall;

    private Rigidbody rb;
    [SerializeField] Transform cam; // is Main Camera
    [SerializeField] Transform orientation; // is Orientation

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckWall();
    }

    private void CheckWall()
    {
        RaycastHit rightRaycast;
        RaycastHit leftRaycast;

        // Check right side of player
        if (Physics.Raycast(orientation.transform.position, orientation.transform.right, out rightRaycast))
        {
            distanceFromRightWall = Vector3.Distance(orientation.transform.position, rightRaycast.point);
            if (distanceFromRightWall <= 3f)
            {
                isRightWall = true;
                isLeftWall = false;
            }
        }
        // Check left side of player
        if (Physics.Raycast(orientation.transform.position, -orientation.transform.right, out leftRaycast))
        {
            distanceFromLeftWall = Vector3.Distance(orientation.transform.position, leftRaycast.point);
            if (distanceFromLeftWall <= 3f)
            {
                isRightWall = false;
                isLeftWall = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("WallRunnable"))
        {
            isWallRunning = true;
            rb.useGravity = false;

            if (isLeftWall)
            {
                cam.transform.localEulerAngles = new Vector3(0f, 0f, -10f);
            }
            if (isRightWall)
            {
                cam.transform.localEulerAngles = new Vector3(0f, 0f, 10f);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("WallRunnable"))
        {
            if (Input.GetKey(KeyCode.Space) && isLeftWall)
            {
                rb.AddForce(Vector3.up * wallRunUpForce, ForceMode.Impulse);
                rb.AddForce(orientation.transform.right * wallRunUpForce, ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.Space) && isRightWall)
            {
                rb.AddForce(Vector3.up * wallRunUpForce, ForceMode.Impulse);
                rb.AddForce(-orientation.transform.right * wallRunUpForce, ForceMode.Impulse);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.transform.CompareTag("WallRunnable"))
        {
            cam.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            isWallRunning = false;
            rb.useGravity = true;
        }
    }

}

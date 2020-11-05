using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator sword;

    private Pickup2 pickup;
    
    // Start is called before the first frame update
    void Start()
    {
        sword = GetComponent<Animator>();
        pickup = GetComponent<Pickup2>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && pickup.GetEquipped())
        {
            sword.enabled = true;
            sword.SetBool("Attack", true);
            Invoke("DisableAnimator", 1.0f); // TODO fix to disable right when the animation ends
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            // sword.enabled = false;
            sword.SetBool("Attack", false);
        }
    }

    private void DisableAnimator()
    {
        sword.enabled = false;
        print("ending animation");
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }
}

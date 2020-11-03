using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator sword;
    // Start is called before the first frame update
    void Start()
    {
        sword = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            sword.SetBool("Attack", true);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            sword.SetBool("Attack", false);
        }
    }
}

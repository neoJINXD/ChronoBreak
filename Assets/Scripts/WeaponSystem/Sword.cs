using UnityEngine;

public class Sword : WeaponBase
{
    
    public override void Attack()
    {
        //plays animation clip here
        coll.enabled = true;

        anim.Play();
        // print("attacc");
        Invoke("DisableCollider", anim.clip.length);

        //Slice sword sound
        AudioManager.instance.Play("Slicing");
    }

    private void DisableCollider()
    {
        coll.enabled = false;
    }
}

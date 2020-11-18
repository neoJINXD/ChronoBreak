using UnityEngine;

public class Gun : WeaponBase 
{
    // Assignables
    [SerializeField] Transform projectileLocation;

    [SerializeField] GameObject projectile;

    [SerializeField] float timeBetweenShot;

    // References
    private float timer = 0;

    public override void Attack()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenShot)
        {
            Instantiate(projectile, projectileLocation.transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}

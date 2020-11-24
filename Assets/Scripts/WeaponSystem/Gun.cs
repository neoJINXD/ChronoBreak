using UnityEngine;

public class Gun : WeaponBase 
{
    // Assignables
    [SerializeField] Transform projectileLocation;
    [SerializeField] Transform orientation;

    [SerializeField] GameObject projectile;

    [SerializeField] float timeBetweenShot;

    // References
    private float timer = Mathf.Infinity;

    void Update() 
    {
        Debug.DrawRay(projectileLocation.position, -projectileLocation.forward * 100f, Color.red);    
    }

    public override void Attack()
    {
        print("Shoot");
        timer += Time.deltaTime;
        if (timer > timeBetweenShot)
        {
            GameObject bullet = Instantiate(projectile, projectileLocation.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().dir = -projectileLocation.forward;
            timer = 0;
        }
    }
}

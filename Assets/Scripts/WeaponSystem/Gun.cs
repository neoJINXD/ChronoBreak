using UnityEngine;

public class Gun : WeaponBase 
{
    // Assignables
    [SerializeField] Transform projectileLocation;

    [SerializeField] GameObject projectile;

    [SerializeField] float timeBetweenShot;

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject muzzleLight;

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
            muzzleFlash.Play();
            muzzleLight.SetActive(true);
            Invoke("DisableFlash", muzzleFlash.main.duration);
            bullet.GetComponent<Bullet>().dir = -projectileLocation.forward;
            timer = 0;
        }
    }

    private void DisableFlash()
    {
        muzzleLight.SetActive(false);
    }
}

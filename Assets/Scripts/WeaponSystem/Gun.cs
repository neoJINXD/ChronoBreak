using UnityEngine;

public class Gun : WeaponBase 
{
    // Assignables
    [SerializeField] Transform projectileLocation;

    [SerializeField] GameObject projectile;

    [SerializeField] float timeBetweenShot;

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject muzzleLight;

    [SerializeField] float recoilAmount;

    // References
    private float timer = Mathf.Infinity;

    void Update() 
    {
        Debug.DrawRay(projectileLocation.position, -projectileLocation.forward * 100f, Color.red); // TODO remove 
    }

    public override void Attack()
    {
        // print("Shoot");
        timer += Time.deltaTime;
        if (timer > timeBetweenShot)
        {
            GameObject bullet = Instantiate(projectile, projectileLocation.transform.position, Quaternion.identity);
            bullet.transform.forward = -transform.forward;
            muzzleFlash.Play();
            muzzleLight.SetActive(true);
            Recoil();
            Invoke("DisableFlash", muzzleFlash.main.duration);
            bullet.GetComponent<Bullet>().dir = -projectileLocation.forward;
            timer = 0;
        }
    }

    private void DisableFlash()
    {
        muzzleLight.SetActive(false);
    }

    private void Recoil()
    {
        // Quaternion rot = transform.localRotation
        transform.localRotation = Quaternion.Euler(Random.value * recoilAmount, Random.value * 1f, 0f);
    }
}

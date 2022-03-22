using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Tooltip("Where the bullet is spawned from.")]
    [SerializeField] Transform bulletSpawn = null;
    [SerializeField] GameObject bulletPrefab = null;
    [Tooltip("Interval between projectiles")]
    public float fireRate = 1f;
    [Tooltip("The force with which the bullet is fired.")]
    public float bulletForce = 100f;
    [HideInInspector] public bool isShooting = false;

    float fireCooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        UnitHealth.OnDeath += Deactivate;
    }
    private void OnDisable()
    {
        UnitHealth.OnDeath -= Deactivate;
    }
    // Update is called once per frame
    void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0 && isShooting)
            Shoot();
    }

    //fires a bullet and sets the firing cooldown.
    public void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation)
            .GetComponent<Bullet>();
        
        bullet.bulletForce = bulletForce;
        fireCooldown = fireRate;
    }

    void Deactivate(bool b) => enabled = false; //Remove control when the battle is over.
}

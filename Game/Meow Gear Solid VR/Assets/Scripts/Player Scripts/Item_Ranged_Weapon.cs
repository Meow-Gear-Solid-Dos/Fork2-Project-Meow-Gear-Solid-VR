using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Ranged_Weapon : Item_Weapon
{
    public GameObject Projectile;
    public GameObject ProjectileSpawnPoint;
    public GameObject bulletPrefab;
    public Transform barrel;
    public int magazineCurrent;
    public int magazineMax;
    public float reloadSpeed = 1.5f;
    public bool isReloading;
    public float bulletSpeed = 25.0f;

    //Lines below deal with sound
    public AudioSource source;
    public AudioClip shootingSound;

    protected Item_Ranged_Weapon(){
    }
    protected override void Start(){
    }

    protected override void Update(){
    }
    
    public override void Activate(){
        if(isReloading == false)
        {
            if (currentAmmo > 0)
            {
                if (magazineCurrent > 0)
                {
                    source.PlayOneShot(shootingSound, .75f);
                    Shoot();
                    //gunMagazine.DecreaseMagazine();
                    magazineCurrent --;
                    currentAmmo --;
                }
                else
                {
                    isReloading = true;
                    Reload(reloadSpeed);

                }
            }
            else
            {
                //add a blank cartridge sfx here
            }
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = Vector3.zero;
        bulletRigidbody.velocity = barrel.forward * bulletSpeed;
        StartCoroutine(BulletLife(2, newBullet));
    }
    
    void Reload(float reloadSpeed)
    {
        StartCoroutine(ReloadTime(reloadSpeed));
    }
    IEnumerator BulletLife(float timer, GameObject newBullet)
    {
        yield return new WaitForSeconds(timer);
        Destroy(newBullet);
    }

    IEnumerator ReloadTime(float timer)
    {
        yield return new WaitForSeconds(timer);

        if(currentAmmo >= magazineMax)
        {
            magazineCurrent = magazineMax;
        }
        else
        {
            magazineCurrent = currentAmmo;
        }
        //gunMagazine.ReloadMagazine();
        isReloading = false;
    }

}

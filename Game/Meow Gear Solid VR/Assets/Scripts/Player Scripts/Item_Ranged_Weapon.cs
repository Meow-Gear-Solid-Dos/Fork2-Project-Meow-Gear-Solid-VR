using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    //Lines below deal with showing ammo count on gun
    public GameObject bulletIconCanvas;
    public RawImage bulletIcon;
    private RawImage newBullet;
    public GridLayoutGroup bulletGrid;
    public List<RawImage> magazineCount = new List<RawImage>();
    //Lines below deal with sound
    public AudioSource source;
    public AudioClip shootingSound;

    protected Item_Ranged_Weapon(){
    }
    protected override void Start(){
        ReloadMagazine();
        bulletIconCanvas.SetActive(false);
    }

    protected override void Update(){
    }
    public override void OnGrab(){
        ShowText(ItemPrefab);
        bulletIconCanvas.SetActive(true);
    }
    public override void OnRelease()
    {
        bulletIconCanvas.SetActive(false);
        //inventory.AddToInventory(ItemPrefab, 1);
        //ShowText(ItemPrefab);

    }
    public override void Activate(){
        if(isReloading == false)
        {
            if (currentAmmo > 0)
            {
                if (magazineCurrent > 0)
                {
                    source.PlayOneShot(shootingSound, .5f);
                    Shoot();
                    DecreaseMagazine();
                    //gunMagazine.DecreaseMagazine();
                    magazineCurrent --;
                    currentAmmo --;
                    ReleasingSound();
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

    public void DecreaseMagazine()
    {
        if(bulletGrid.transform.childCount > 0)
        {       
            Debug.Log("Removing bullet");
            var i = bulletGrid.transform.childCount - 1;
            Object.Destroy(bulletGrid.transform.GetChild(i).gameObject);
        }
    }

    public void ReloadMagazine()
    {
        if(bulletGrid.transform.childCount != magazineCurrent)
        {        
            for (var i = bulletGrid.transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(bulletGrid.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < magazineCurrent; i++)
            {
                Debug.Log("Adding bullet");
                newBullet = Instantiate(bulletIcon, bulletGrid.transform, false);
                magazineCount.Add(newBullet);    
            }           
        }
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
        ReloadMagazine();
        isReloading = false;
    }
    public void ReleasingSound()
    {
        //publish the "position" parameter of the dummy sound object to whoever subscribe to the event.
        EventBus.Instance.HearingSound(transform.position);
    }

}

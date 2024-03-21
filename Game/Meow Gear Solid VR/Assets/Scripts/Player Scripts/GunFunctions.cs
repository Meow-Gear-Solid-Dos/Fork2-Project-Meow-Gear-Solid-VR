using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFunctions : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform barrel;
    public float bulletSpeed = 10.0f;

    private GameObject currentBullet;
    private Rigidbody bulletRigidbody;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Shoot();
        }
    }

    void Shoot(){
        GameObject newBullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
        Destroy(newBullet.GetComponent<GunFunctions>());
        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = barrel.forward * bulletSpeed;
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject bullet;
    public float damage = 50f;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision){
        Destroy(bullet);
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            EnemyHealth enemyScript = collision.gameObject.GetComponent<EnemyHealth>();
            if(enemyScript != null){
                enemyScript.TakeDamage(damage);
            }
        }
    }
}

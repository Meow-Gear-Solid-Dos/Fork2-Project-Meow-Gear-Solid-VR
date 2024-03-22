
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject bullet;
    public float damage = 25f;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Entering Object");
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            Debug.Log("Entering Enemy");
            EnemyHealth enemyScript = other.gameObject.GetComponent<EnemyHealth>();
            if(enemyScript != null){
                enemyScript.TakeDamage(damage);
            }
        }
        Destroy(bullet);
    }
}

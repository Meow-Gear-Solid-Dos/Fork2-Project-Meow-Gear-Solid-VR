
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject bullet;
    public float damage = 25f;
    public AudioSource audioSource;
    public AudioClip enemyImpactSound;
    public AudioClip enemyImpactSoundAlt;
    public AudioClip wallImpactSound;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entering Object");
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            Debug.Log("Entering Enemy");
            EnemyHealth enemyScript = other.gameObject.GetComponent<EnemyHealth>();
            if(enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
                if(Random.Range(0, 2) == 0 )
                {
                    audioSource.PlayOneShot(enemyImpactSound, .5f);
                }
                else
                {
                    audioSource.PlayOneShot(enemyImpactSoundAlt, .5f);
                }
                ReleasingSound();
            }
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Boss")){
            Debug.Log("Entering Enemy");
            EnemyBossHealth enemyScript = other.gameObject.GetComponent<EnemyBossHealth>();
            if(enemyScript != null){
                enemyScript.TakeDamage(damage);
                if(Random.Range(0, 2) == 0 )
                {
                    audioSource.PlayOneShot(enemyImpactSound, .5f);
                }
                else
                {
                    audioSource.PlayOneShot(enemyImpactSoundAlt, .5f);
                }
            }
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("Entering Obstacle");
            audioSource.PlayOneShot(wallImpactSound, .5f);
            ReleasingSound();
        }
        StartCoroutine("DelayedDestroy");
    }

    public void ReleasingSound()
    {
        //publish the "position" parameter of the dummy sound object to whoever subscribe to the event.
        EventBus.Instance.HearingSound(transform.position);
    }
    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(bullet);
    }



}

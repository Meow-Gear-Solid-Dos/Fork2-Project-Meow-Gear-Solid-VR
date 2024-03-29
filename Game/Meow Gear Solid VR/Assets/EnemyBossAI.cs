using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class EnemyBossAI : MonoBehaviour
{
    [SerializeField] private GameObject bloodSplat;
    [SerializeField] private GameObject enemyPrefab;
    public EnemyBossHealth bossHealthbar;
    public Rigidbody rigidBody;
    public NavMeshAgent agent;
    private GameObject splatter;
    public Transform enemyHead;
    public Renderer enemy;
    public Transform player;
    public Vector3 playerPosition;
    //Below are for hitboxes
    public Transform rightArm;
    public Transform leftArm;
    public Transform rightLeg;
    public GameObject HitBoxPrefab;
    

    //Bools for animations and attack states
    public Animator bossAnimator;
    public bool isAttacking;
    public bool isRunning;
    public bool isWalking;
    public bool isMoving;
    public bool inAnimation;
    public bool isInvulnerable;
    public float moveSpeed;
    public int rotationSpeed;
    public float attackDistance;

    //Below are for sounds
    public AudioSource source;
    public AudioClip punchSound1;
    public AudioClip punchSound2;

    //Last item here tracks what state the enemy is in.
    public int phase = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bossHealthbar = GameObject.FindGameObjectWithTag("Boss").GetComponent<EnemyBossHealth>();
        isAttacking = false;
        isRunning = false;
        isWalking = false;
        isMoving = false;
        inAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Keeps track of player postion
        playerPosition = player.position;

        //Below is the FSM for the boss
        //The boss starts in an idle state. When in X range of the player, the boss will walk to the player and punch.
        //If out of X range, the boss will sprint at the player and go for a tackle.
        //In between phases the boss will be vulnerable for a while. After some time passes, the boss will block
    //ChargeAttack(playerPosition);
    Blocking();

    }

    public void Blocking()
    {
        isInvulnerable = true;
        bossHealthbar.isInvulnerable = isInvulnerable;
        bossAnimator.SetBool("IsBlocking", true);
        bossAnimator.SetBool("IsAttacking", false);
        bossAnimator.SetBool("IsMoving", false);
        bossAnimator.SetBool("IsRunning", false);
    }

    public void Walking()
    {
            Vector3 distanceFromPlayer = playerPosition - transform.position;
            float distance = Vector3.Distance(playerPosition,transform.position);
            distanceFromPlayer.Normalize();
            //Debug.Log("Here's the Distance: " + distance);
            if(distance <= attackDistance)
            {
                rigidBody.velocity = distanceFromPlayer * 0;
                bossAnimator.SetBool("IsMoving", false);
                bossAnimator.SetBool("IsAttacking", true);
                transform.LookAt(player.transform);
                agent.SetDestination(rigidBody.position);
                //In the if statement, cancel the path since we don't want the enemy to move.
                //Set the destination to player position instead. 
            }
            if(distance > attackDistance)
            {
                bossAnimator.SetBool("IsMoving", true);
                bossAnimator.SetBool("IsAttacking", false);
                agent.SetDestination(playerPosition);
                //Might need to get rid of rotation since nav mesh should be able to handle it
                if (distanceFromPlayer * moveSpeed != Vector3.zero)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(distanceFromPlayer * moveSpeed);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
                }
            }            

    }

    public void StandingAttack()
    {

    }

    public void ChargeAttack(Vector3 playerPosition)
    {
        //Calculates distance from the player. Converts Vector3 distance into a simple float.
        Vector3 distanceFromPlayer = playerPosition - transform.position;
        distanceFromPlayer.Normalize();
        float distance = Vector3.Distance(playerPosition,transform.position);

        if(distance > attackDistance)
        {
            bossAnimator.SetBool("IsRunning", true);
            bossAnimator.SetBool("IsAttacking", false);
            rigidBody.velocity = distanceFromPlayer * moveSpeed;

            if (rigidBody.velocity != Vector3.zero)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(rigidBody.velocity);
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
            }
        }

        if(distance <= attackDistance)
        {
            rigidBody.velocity = distanceFromPlayer * moveSpeed;
            bossAnimator.SetBool("IsRunning", true);
            bossAnimator.SetBool("IsAttacking", true);
            GameObject hitBox = Instantiate(HitBoxPrefab, rightArm.position, rightArm.rotation);
            hitBox.transform.parent = rightArm;
            StartCoroutine(HitBoxLife(.75f, hitBox));
            bossAnimator.SetBool("IsMoving", false);
            bossAnimator.SetBool("IsRunning", false);
            bossAnimator.SetBool("IsAttacking", false);
        }

    }

    IEnumerator HitBoxLife(float timer, GameObject hitBox)
    { 
        yield return new WaitForSeconds(timer);
        Destroy(hitBox);
        inAnimation = false;
        isAttacking = false;
        bossAnimator.SetBool("IsAttacking", isAttacking);
    }
    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(2.5f);
        isAttacking = false;
        isRunning = false;
        isWalking = false;
        isMoving = false;
        inAnimation = false;
    }
}

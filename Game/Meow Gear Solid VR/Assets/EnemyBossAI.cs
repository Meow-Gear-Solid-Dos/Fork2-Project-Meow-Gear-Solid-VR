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
    public Quaternion startRotation;
    public float turnSpeed;

    //Below are for hitboxes
    public Transform rightArm;
    public Transform leftArm;
    public Transform rightLeg;
    public GameObject HitBoxPrefab;
    
    //Lines down here are for path traversal
    public Transform path;
    public  List<Transform> myNodes;
    Vector3 nodePosition;
    public Transform myCurrentNode;
    public int index;
    //keep track of current nodes
    private int currentNode = 0;

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
    public int punchNumber;

    //Below are for sounds
    public AudioSource source;
    public AudioClip punchSound1;
    public AudioClip punchSound2;

    //Last item here tracks what state the enemy is in.
    public int phase;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bossHealthbar = GameObject.FindGameObjectWithTag("Boss").GetComponent<EnemyBossHealth>();
        startRotation = transform.rotation;
        //Node Functionality for enemy traversal
        myNodes = new List<Transform>();
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        foreach (Transform t in pathTransforms)
        {
            if (t != path.transform)
            {
                myNodes.Add(t);
            }
        }
        index = 0;
        myCurrentNode = myNodes.ElementAt(index);

        //Bools for attack and animation states.
        isAttacking = true;
        isRunning = false;
        isWalking = false;
        isMoving = false;
        inAnimation = false;
        punchNumber = 1;
        phase = 1;
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
        switch(phase)
        {
            case 1://Close Range Attack
                bossAnimator.SetBool("IsMoving", isMoving);
                bossAnimator.SetBool("IsAttacking", isAttacking);
                LookAtPlayer(playerPosition);
                if ((Vector3.Distance(playerPosition, transform.position)) <= attackDistance)
                {
                    isMoving = false;
                    isAttacking = true;
                    agent.SetDestination(rigidBody.position);
                    StandingAttack(isAttacking);
                }
                else
                    isMoving = true;
                    isAttacking = false;
                    agent.SetDestination(player.position);

                break;
            case 2://Running attack
                if(!agent.pathPending)
                {
                    rigidBody.velocity = Vector3.zero;
                    
                }
                // Using distance included y which varied when enemies collided.
                if(myCurrentNode.position.x == transform.position.x && myCurrentNode.position.z == transform.position.z)
                {
                    transform.rotation = startRotation;
                    agent.ResetPath();
                }
                Vector3 distance = transform.position - myCurrentNode.position;
                distance.y = 0;
                if((distance).magnitude > 0.2f)
                {
                    FollowNode(myCurrentNode.position);
                }
                if((Vector3.Distance(myCurrentNode.position, transform.position)) <= attackDistance)
                {
                    ChargeAttack(playerPosition);
                    ++index;
                    if(index == myNodes.Count)
                    {
                        index = 0;
                        myCurrentNode = myNodes.ElementAt(index);
                    }
                    else
                    {
                        myCurrentNode = myNodes.ElementAt(index);
                    }
                }

                break;
            case 3: //defensive stance
                LookAtPlayer(playerPosition);
                Blocking();
                break;
        }

    }
    private void FollowNode(Vector3 nodePosition)
    {
        bossAnimator.SetBool("IsMoving", true);
        bossAnimator.SetBool("IsAttacking", false);
        Vector3 distanceFromNode = nodePosition - transform.position;
        float distance = Vector3.Distance(nodePosition, transform.position);
        distanceFromNode.Normalize();
        agent.SetDestination(nodePosition);
        if (distanceFromNode * moveSpeed != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(distanceFromNode * moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);
        }
    }

    public void Blocking()
    {
        isInvulnerable = true;
        bossHealthbar.isInvulnerable = isInvulnerable;
        bossAnimator.SetBool("IsBlocking", true);
        bossAnimator.SetBool("IsAttacking", false);
        bossAnimator.SetBool("IsMoving", false);
        bossAnimator.SetBool("IsRunning", false);
        StartCoroutine("BlockTimer");
    }

    public void Walking(Vector3 playerPosition)
    {
            Vector3 distanceFromPlayer = playerPosition - transform.position;
            float distance = Vector3.Distance(playerPosition,transform.position);
            distanceFromPlayer.Normalize();
            //Debug.Log("Here's the Distance: " + distance);
            if(distance <= attackDistance)
            {
                isMoving = false;
                isAttacking = true;
                transform.LookAt(player.transform);
                agent.SetDestination(rigidBody.position);
                //In the if statement, cancel the path since we don't want the enemy to move.
                //Set the destination to player position instead. 
            }
            if(distance > attackDistance)
            {
                isMoving = true;
                isAttacking = false;
                agent.SetDestination(playerPosition);
                //Might need to get rid of rotation since nav mesh should be able to handle it
                if (distanceFromPlayer * moveSpeed != Vector3.zero)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(distanceFromPlayer * moveSpeed);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
                }
            }            

    }

    public void StandingAttack(bool isAttacking)
    {
        //Reset's punch number for the attack
        if (isAttacking == true)
        {
            bossAnimator.SetBool("IsAttacking", isAttacking);
            StartCoroutine("Attack");
        }
        else
        {
            StopCoroutine("Attack");
            bossAnimator.SetBool("IsAttacking", isAttacking);
        }
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
            rigidBody.velocity = distanceFromPlayer * moveSpeed * 1.5f;

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
            agent.SetDestination(rigidBody.position);
            StartCoroutine(DashAttack(.75f, hitBox));
        }

    }
    public void LookAtPlayer(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }
    IEnumerator HitBoxLife(float timer, GameObject hitBox)
    {
        inAnimation = true;
        yield return new WaitForSeconds(timer);
        Destroy(hitBox);
        inAnimation = false;
        bossAnimator.SetBool("IsMoving", false);
        bossAnimator.SetBool("IsRunning", false);
        bossAnimator.SetBool("IsAttacking", false);
    }

    IEnumerator DashAttack(float timer, GameObject hitBox)
    {
        inAnimation = true;
        yield return new WaitForSeconds(timer);
        phase = 3;
        bossAnimator.SetBool("IsMoving", false);
        bossAnimator.SetBool("IsRunning", false);
        bossAnimator.SetBool("IsAttacking", false);
        Destroy(hitBox);
        inAnimation = false;
        StopCoroutine("DashAttack");
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

    IEnumerator BlockTimer()
    {
        yield return new WaitForSeconds(2.5f);
        isInvulnerable = false;
        bossHealthbar.isInvulnerable = isInvulnerable;
        bossAnimator.SetBool("IsBlocking", false);
        bossAnimator.SetBool("IsAttacking", false);
        bossAnimator.SetBool("IsMoving", false);
        bossAnimator.SetBool("IsRunning", false);
        if( Random.Range(0, 2) == 0 )
            phase = 1;
        else
            phase = 2;

    }

    IEnumerator Attack()
    {
        if((punchNumber == 1) && inAnimation == false && isAttacking == true)
        {
            Punch(rightArm, punchNumber);
            punchNumber = 2;
            yield return new WaitForSeconds(1f);
        }

        if((punchNumber == 2) && inAnimation == false && isAttacking == true)
        {
            Punch(leftArm, punchNumber);
            punchNumber = 3;
            yield return new WaitForSeconds(1f);  
        }

        if((punchNumber == 3) && inAnimation == false && isAttacking == true)
        {
            Punch(rightLeg, punchNumber);
            punchNumber = 1;
            yield return new WaitForSeconds(1.5f);
            isAttacking = false;
            inAnimation = false;
            bossAnimator.SetBool("IsAttacking", isAttacking); 
            phase = 3; 
            StopCoroutine("Attack");   
        }
    }
    
    void Punch(Transform limb, int punchNumber)
    {
        bossAnimator.SetBool("IsAttacking", isAttacking);
        bossAnimator.SetInteger("MeleeAttack", punchNumber);    
        GameObject hitBox = Instantiate(HitBoxPrefab, limb.position, limb.rotation);
        hitBox.transform.parent = limb;
        StartCoroutine(HitBoxLife(.75f, hitBox));
        if (punchNumber == 3)
        {
            source.PlayOneShot(punchSound2);
            phase = 3;
            StopCoroutine("Attack");   
        }
        else
        {
            source.PlayOneShot(punchSound1);            
        }
    }
}

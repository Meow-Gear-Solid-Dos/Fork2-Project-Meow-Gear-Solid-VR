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
    public bool chargeAttack;
    public bool runningToPlayer;
    //Below are for sounds
    public AudioSource source;
    public AudioClip punchSound1;
    public AudioClip punchSound2;

    //Last item here tracks what state the enemy is in.
    public int phase;
    public int one = 0;
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

        //StartCoroutine("BossPattern");
    }

    // Update is called once per frame
    void Update()
    {
        //Keeps track of player postion
        playerPosition = player.position;
        if(EventBus.Instance.enemyCanMove == false)
        {
            return;
        }
        if(one < 1 )
        {
            one++;
            StartCoroutine("BossPattern");
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



    IEnumerator ChargeAttack()
    {
        //StopCoroutine("Phase2Attack");
            //Calculates distance from the player. Converts Vector3 distance into a simple float.
            Debug.Log("Charge attack!");
            Vector3 distanceFromPlayer = playerPosition - transform.position;
            distanceFromPlayer.Normalize();
            float distance = Vector3.Distance(playerPosition,transform.position);
            Debug.Log("Distance to player: "+ distance);
            if(distance <= attackDistance)
            {
                rigidBody.velocity = distanceFromPlayer * 0;
                Debug.Log("attacking player");
                rigidBody.velocity = distanceFromPlayer * moveSpeed;
                bossAnimator.SetBool("IsRunning", true);
                bossAnimator.SetBool("IsAttacking", true);
                GameObject hitBox = Instantiate(HitBoxPrefab, rightArm.position, rightArm.rotation);
                hitBox.transform.parent = rightArm;
                agent.SetDestination(rigidBody.position);
                chargeAttack = false;
                runningToPlayer = false;
                yield return DashAttack(.75f, hitBox);
                //StopCoroutine("ChargeAttack");
            }

            else
            {
                agent.ResetPath();
                Debug.Log("Walking to player");
                bossAnimator.SetBool("IsRunning", true);
                bossAnimator.SetBool("IsAttacking", false);
                rigidBody.velocity = distanceFromPlayer * moveSpeed * 1.5f;

                if (rigidBody.velocity != Vector3.zero)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(rigidBody.velocity);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
                }
            }

    }
    public void LookAtPlayer(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }
    IEnumerator BossPattern()
    {
        //Below is the FSM for the boss
        //The boss starts in an idle state. When in X range of the player, the boss will walk to the player and punch.
        //If out of X range, the boss will sprint at the player and go for a tackle.
        //In between phases the boss will be vulnerable for a while. After some time passes, the boss will block
        

        while (true)
        {
            switch (phase)
            {
                case 1://Close Range Attack
                    rigidBody.velocity = rigidBody.velocity*0;
                    yield return Phase1Attack();
                    break;
                case 2://Running attack
                       // Using distance included y which varied when enemies collided.
                    rigidBody.velocity = rigidBody.velocity*0;
                    yield return Phase2AttackP1();
                    break;
                case 3:
                    rigidBody.velocity = rigidBody.velocity*0;
                    yield return BlockTimer();
                    break;

            }
        }
    }
    IEnumerator Phase1Attack()
    {
        LookAtPlayer(playerPosition);
        if ((Vector3.Distance(playerPosition, transform.position)) <= attackDistance)
        {
            isMoving = false;
            isAttacking = true;            
            bossAnimator.SetBool("IsMoving", isMoving);
            bossAnimator.SetBool("IsAttacking", isAttacking);

            agent.SetDestination(rigidBody.position);
            yield return Attack();
        }
        else
        {
            isMoving = true;
            isAttacking = false;            
            bossAnimator.SetBool("IsMoving", isMoving);
            bossAnimator.SetBool("IsAttacking", isAttacking);

            agent.SetDestination(player.position);                    
        }
    }
    IEnumerator Phase2AttackP1()
    {
        bossAnimator.SetBool("IsMoving", true);
        bossAnimator.SetBool("IsAttacking", false);
        myCurrentNode = myNodes.ElementAt(index);
        agent.SetDestination(myCurrentNode.position);
        while (((Vector3.Distance(myCurrentNode.position, transform.position)) >= attackDistance))
        {
            Debug.Log("Walking to node");
            yield return new WaitForEndOfFrame(); 
        }

        Debug.Log("Starting part 2");
        yield return Phase2AttackP2();  
    }
    IEnumerator Phase2AttackP2()
    {
            Debug.Log("In part 2");
            agent.ResetPath();
            Vector3 distanceFromPlayer = playerPosition - transform.position;
            distanceFromPlayer.Normalize();
            bossAnimator.SetBool("IsRunning", true);
            bossAnimator.SetBool("IsAttacking", false);
            rigidBody.velocity = distanceFromPlayer * moveSpeed * 1.5f;
            if (rigidBody.velocity != Vector3.zero)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(rigidBody.velocity);
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
            }
            float timer = 0f;
            bool done = false;
            while(((Vector3.Distance(playerPosition, transform.position)) >= attackDistance) && (done == false))
            {
                Debug.Log("Sprinting to player");
                timer += Time.deltaTime;
                if(timer >= 2)
                {
                    done = true;
                }
                yield return new WaitForEndOfFrame(); 
            }
            LookAtPlayer(playerPosition); 
            StopCoroutine("Timeout");
            bossAnimator.SetBool("IsRunning", true);
            Kick(rightLeg);
            yield return new WaitForSeconds(.5f);
            rigidBody.velocity = distanceFromPlayer * .5f;
            yield return new WaitForSeconds(.5f);
            rigidBody.velocity = distanceFromPlayer * 0;

        
    }
    IEnumerator RandomNode()
    {   
        var random = Random.Range(0, 4);
        if(random == 0)
            index = 0;
        if(random == 1)
            index = 1;
        if(random == 2)
            index = 2;
        else
            index = 3;
        yield return new WaitForSeconds(10f);
        //StopCoroutine("RandomNode");
    }
    IEnumerator HitBoxLife(float timer, GameObject hitBox)
    {
        inAnimation = true;
        yield return new WaitForSeconds(timer);
        Destroy(hitBox);
        inAnimation = false;
        bossAnimator.SetBool("IsMoving", false);
        bossAnimator.SetBool("IsAttacking", false);
    }

    IEnumerator DashAttack(float timer, GameObject hitBox)
    {
        chargeAttack = false;
        inAnimation = true;
        yield return new WaitForSeconds(timer);
        phase = 3;
        bossAnimator.SetBool("IsMoving", false);
        bossAnimator.SetBool("IsRunning", false);
        bossAnimator.SetBool("IsAttacking", false);
        Destroy(hitBox);
        inAnimation = false;
        chargeAttack = true;
        yield return new WaitForSeconds(2f);
        //StopCoroutine("DashAttack");
    }
    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Stopping!");
        rigidBody.velocity = rigidBody.velocity *0;
        isAttacking = false;
        isRunning = false;
        isWalking = false;
        isMoving = false;
        inAnimation = false;
        StopCoroutine("Phase2AttackP2");
        phase = 3;
    }

    IEnumerator BlockTimer()
    {
        rigidBody.velocity = rigidBody.velocity *0;
        agent.ResetPath();
        isInvulnerable = true;
        bossHealthbar.isInvulnerable = isInvulnerable;
        bossAnimator.SetBool("IsBlocking", true);
        bossAnimator.SetBool("IsAttacking", false);
        bossAnimator.SetBool("IsMoving", false);
        bossAnimator.SetBool("IsRunning", false);
        LookAtPlayer(playerPosition);
        yield return new WaitForSeconds(2.5f);
        isInvulnerable = false;
        bossHealthbar.isInvulnerable = isInvulnerable;
        bossAnimator.SetBool("IsBlocking", false);
        bossAnimator.SetBool("IsAttacking", false);
        bossAnimator.SetBool("IsMoving", false);
        bossAnimator.SetBool("IsRunning", false);
        StartCoroutine("RandomNode");
        yield return new WaitForSeconds(1f);
        if(Random.Range(0, 2) == 0 )
            phase = 1;
        else
            phase = 2;
        //StopCoroutine("BlockTimer");
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
            bossAnimator.SetBool("IsAttacking", isAttacking);
            yield return new WaitForSeconds(1.5f);
            isAttacking = false;
            inAnimation = false;
            bossAnimator.SetBool("IsAttacking", isAttacking);
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
    void Kick(Transform limb)
    {
        bossAnimator.SetBool("IsAttacking", true);
        bossAnimator.SetBool("IsRunning", true);  
        GameObject hitBox = Instantiate(HitBoxPrefab, limb.position, limb.rotation);
        hitBox.transform.parent = limb;
        StartCoroutine(HitBoxLife(.75f, hitBox));
        source.PlayOneShot(punchSound2);
        phase = 3;  
    }

    private void OnTriggerEnter(Collider other)
    {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                runningToPlayer = false;
            }
        
    }
}

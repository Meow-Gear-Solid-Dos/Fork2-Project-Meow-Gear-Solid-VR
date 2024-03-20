using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Vector3 playerLastKnownPosition;
    public Vector3 playerCurrentPosition;
    public bool canSeePlayer;
    public float playerRange;
    public FieldOfView fieldOfView;
    public Rigidbody rigidBody;
    public float moveSpeed;
    public float turnSpeed;
    public float mininumDistanceFromPlayer = 2f;
    public int rotationSpeed;
    public NavMeshAgent agent;
    public Animator animator;
    private float currentPatrolDistance;
    private bool movingStage1;
    private bool movingStage2;
    private bool chasing;
    public Quaternion startRotation;
    public bool hasBeenAlerted;

    //Lines down here are for path traversal
    public Transform path;
    public  List<Transform> myNodes;
    Vector3 nodePosition;
    public Transform myCurrentNode;
    public int index;
    //keep track of current nodes
    private int currentNode = 0;

    void Awake()
    {
        fieldOfView = GetComponent<FieldOfView>();
        startRotation = transform.rotation;
        rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        animator.SetBool("IsMoving", true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
    }

    void Update()
    {
        playerCurrentPosition = player.position;
        Vector3 distanceFromPlayer = player.position - transform.position;
        canSeePlayer = fieldOfView.canSeeTarget;
        hasBeenAlerted = EventBus.Instance.inAlertPhase;
        
        if(EventBus.Instance.enemyCanMove == false)
        {
            return;
        }

        //If the player has been spotted, chase them
        if(hasBeenAlerted == true)
        {
            playerLastKnownPosition = EventBus.Instance.playerLastKnownPosition;
            animator.SetBool("IsAttacking", true);
            if(canSeePlayer == true)
            {
                
                FollowPlayer(playerCurrentPosition, playerLastKnownPosition, canSeePlayer);
            }
            else
            {
                FollowPlayer(playerCurrentPosition, playerLastKnownPosition, canSeePlayer);;
            }

        }

        //Otherwise, return to their position
        else
        {
            animator.SetBool("IsAttacking", false);
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
            else
            {
                //update current node
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
        }
    }

    void FollowPlayer(Vector3 playerPosition, Vector3 lastKnownPosition, bool canSeePlayer)
    {
        
        if(canSeePlayer == true)
        {
            Vector3 distanceFromPlayer = playerPosition - transform.position;
            float distance = Vector3.Distance(playerPosition,transform.position);
            distanceFromPlayer.Normalize();
            Debug.Log("Here's the Distance: " + distance);
            if(distance <= mininumDistanceFromPlayer)
            {
                rigidBody.velocity = distanceFromPlayer * 0;
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", true);
                transform.LookAt(player.transform);
                agent.SetDestination(rigidBody.position);
                //In the if statement, cancel the path since we don't want the enemy to move.
                //Set the destination to player position instead. 
            }
            if(distance > mininumDistanceFromPlayer)
            {
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsAttacking", false);
                agent.SetDestination(playerCurrentPosition);
                //Might need to get rid of rotation since nav mesh should be able to handle it
                if (distanceFromPlayer * moveSpeed != Vector3.zero)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(distanceFromPlayer * moveSpeed);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
                }
            }            
        }
        //If the enemy cannot see the player, it will go to their last known location
        else
        {
            Vector3 distanceFromPlayer = playerLastKnownPosition - transform.position;
            float distance = Vector3.Distance(playerLastKnownPosition,transform.position);
            distanceFromPlayer.Normalize();
            Debug.Log("Here's the Distance: " + distance);       

            if(distance <= mininumDistanceFromPlayer)
            {
                rigidBody.velocity = distanceFromPlayer * 0;
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", false);
                transform.LookAt(player.transform);
                agent.SetDestination(rigidBody.position);
                //In the if statement, cancel the path since we don't want the enemy to move.
                //Set the destination to player position instead. 
            }
            else
            {
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsAttacking", false);
                agent.SetDestination(playerLastKnownPosition);   
                if (distanceFromPlayer * moveSpeed != Vector3.zero)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(distanceFromPlayer * moveSpeed);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
                }            
            }
                        
        }

    }
    private void FollowNode(Vector3 nodePosition)
    {
        animator.SetBool("IsMoving", true);
        animator.SetBool("IsAttacking", false);
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
    
}
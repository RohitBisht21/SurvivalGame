using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    private Animator enemyAnimator;
    private float newSpeed = 5f;
    private Survival playerSurvival;

    //For Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //For Attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //For States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        playerSurvival =player.GetComponent<Survival>();

    }
    private void Update()
    {
        //To check for sightRange and attackRange
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling(); 
        if (playerInSightRange && !playerInAttackRange) ChasePlayer(); 
        if (playerInSightRange && playerInAttackRange) Attacking(); 
    }
    private void Patroling()
    {
        if(!walkPointSet || agent.remainingDistance < 1f) SearchWalkPoint();

        if (walkPointSet)
            enemyAnimator.SetBool("Running", false);
        agent.speed = newSpeed - 3;
            enemyAnimator.SetBool("Walking", true);
            agent.SetDestination(walkPoint);

    }
    private void SearchWalkPoint()
    {
        //To calculate random point
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
        else
            walkPointSet = false;
    }
    private void ChasePlayer()
    {
        enemyAnimator.SetBool("Attacking", false);
        enemyAnimator.SetBool("Running", true);
        agent.speed = newSpeed;
        agent.SetDestination(player.position);
    }    
    private void Attacking()
    {
        enemyAnimator.SetBool("Attacking", true);
        //Stop enemy from moving
        agent.stoppingDistance = attackRange;
 
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            playerSurvival.TakeDamage(10);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
       
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    //private void TakeDamage(int damage)
    //{
    //    health -= damage;
    //    if (health < 0)
    //        Invoke(nameof(DestroyEnemy), 0.5f);
    //}
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

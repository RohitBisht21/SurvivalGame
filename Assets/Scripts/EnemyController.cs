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
    public Animator enemyAnimator;
    public float newSpeed = 5f;
    private Survival playerSurvival;
    public ParticleSystem bloodParticles;
    public Transform headTransform;
    public float headHitRadius = 0.2f;
    private bool hasBeenHit = false;
    public bool isZombie = true;

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
        playerSurvival =player.GetComponent<Survival>();
        timeBetweenAttacks = 0.5f;
    }
    private void Update()
    {
        //To check for sightRange and attackRange
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (health > 0)
        {
            if (!playerInSightRange && !playerInAttackRange)
            {
                if (hasBeenHit)
                {
                    // If the player has hit the enemy, start chasing
                    ChasePlayer();
                }
                else
                {
                    Patroling();
                }
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
            }
            else if (playerInSightRange && playerInAttackRange)
            {
                Attacking();
            }
        }
        else
        {
            // If health is 0 or less, stop all actions
            agent.SetDestination(transform.position); // Stop the NavMeshAgent
            enemyAnimator.SetBool("Running", false);
            enemyAnimator.SetBool("Attacking", false);
            enemyAnimator.SetBool("Walking", false);
            enemyAnimator.SetBool("isHit", true);
        }
    }
    private void Patroling()
    {
      
        if(!walkPointSet || agent.remainingDistance <= 3f ) SearchWalkPoint();

        if (walkPointSet)
        {
            enemyAnimator.SetBool("Running", false);
            enemyAnimator.SetBool("Walking", true);
            agent.speed = 2f;
            agent.SetDestination(walkPoint);
            enemyAnimator.SetBool("isHit", false);
      
        }

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
        
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            playerSurvival.TakeDamage(5);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void TakeEnemyDamage(float damage, Vector3 hitPoint)
    {
        if (health > 0)
        {
            bool hitOnHead = Vector3.Distance(hitPoint, headTransform.position) <= headHitRadius;

            health -= damage;
    
            if(hitOnHead)
            {
                // Play head hit animation
                enemyAnimator.SetTrigger("Hit");
            }
            if (health <= 0)
            {
                enemyAnimator.SetTrigger("Die");
                Invoke(nameof(DestroyEnemy), 3f);
            }

            hasBeenHit = true; // Set the hasBeenHit flag
            if (!playerInSightRange)
            {
                // If the player is not initially in sight range, start chasing
                ChasePlayer();
            }
        }
    }
    public void DestroyEnemy()
    {
        
        if (isZombie)
        {
            // Return the zombie to the pool for reuse
            gameObject.SetActive(false);
        }
        else
        {
            // Destroy boss-level enemy
            Destroy(gameObject);
        }
    }

     // Reset the properties of the zombie when it's respawned
    public void ResetZombieProperties()
    {
        health = 10f;
        hasBeenHit = false;
        alreadyAttacked = false;
        
    }
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

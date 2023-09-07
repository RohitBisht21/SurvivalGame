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
    public float newSpeed = 8f;

    private Survival playerSurvival;
    public ParticleSystem bloodParticles;
    public ParticleSystem DeadCrab;
    public Transform headTransform;
    public float headHitRadius = 0.2f;
    private bool hasBeenHit = false;

    public bool isZombie = true;
    public bool isBoss1 = false;
    public bool isBoss2 = false;

    public GameObject keyPrefab;

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
        playerSurvival = player.GetComponent<Survival>();
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
        if (!walkPointSet || agent.remainingDistance <= 3f) SearchWalkPoint();

        if (walkPointSet)
        {
            enemyAnimator.SetBool("Running", false);
            enemyAnimator.SetBool("Walking", true);
            agent.speed = 2f;
            agent.SetDestination(walkPoint);
            enemyAnimator.SetBool("isHit", false);

        }
        if (isBoss1)
        {
            AudioManager.Instance.Play("BullBreathing");
        }
        else if (isBoss2)
        {
            AudioManager.Instance.Play("CrabBreathing");
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
        if (isBoss1)
        {
            AudioManager.Instance.Play("BullMoan");
        }
        else if (isZombie)
        {
            AudioManager.Instance.Play("ZombieMoan");
        }
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

            if (hitOnHead)
            {
                // Play head hit animation
                enemyAnimator.SetTrigger("Hit");
            }
            if (health <= 0)
            {
                enemyAnimator.SetTrigger("Die");
                if (isBoss1)
                {
                    AudioManager.Instance.Play("BullDead");
                    AudioManager.Instance.Stop("BullBreathing");
                    AudioManager.Instance.Stop("BullMoan");

                }
                else if (isBoss2 && DeadCrab != null)
                {
                    DeadCrab.Play();
                    AudioManager.Instance.Play("CrabDead");
                    AudioManager.Instance.Stop("CrabBreathing");
                }
                else
                {
                    AudioManager.Instance.Play("ZombieDead");
                }

                Invoke(nameof(DestroyEnemy), 2f);
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
            GameManager.Instance.DefeatZombie();
            // Return the zombie to the pool for reuse
            gameObject.SetActive(false);
        }
        else
        {
            DropKey();
            // Destroy boss-level enemy
            GameManager.Instance.DefeatBoss1();
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
    private void DropKey()
    {
        if (keyPrefab != null)
        {
            // Instantiate the key prefab at the enemy's position
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
            KeyCollection.Instance.keyParticles.Play();
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, sightRange);
    //}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonSlash : MonoBehaviour
{
    public float Health = 50f;
    public float Magicspeed = 10;
    public Enum.Type typeMon;
    public Enum.Type Lose;
    public Enum.Type Win;


    public NavMeshAgent agent;

    public Transform player;
    public Transform MagicMonShooot;
    public Transform Mons;
    public LayerMask Ground, Player;
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    [HideInInspector]
    public SpawnMon spawnMon;
    public int state;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public GameObject projectile;
    public GameObject MonMagic;
    public GameObject Mana;

    public Animator ani;


    private void Awake()
    {
        player = GameObject.Find("Playfind").transform;
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (state == 0)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        }
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);


        if (playerInSightRange && !playerInAttackRange)
        {
            spawnMon.FoundPlayer();
        }
        else if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
        else if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);

        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;


        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        RaycastHit hit;
        if (Physics.Raycast(walkPoint, -transform.up, out hit, 2, Ground))
        {
            Debug.DrawRay(walkPoint, -transform.up * 2, Color.green);
            Debug.Log(hit.transform.gameObject.name);
            walkPointSet = true;
        }

    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
        Debug.Log(gameObject.name);
    }

    private void AttackPlayer()
    {

        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            projectile = Instantiate(MonMagic, MagicMonShooot.transform.position, Mons.transform.rotation);
            Rigidbody RigiMonMagic = projectile.GetComponent<Rigidbody>();
            RigiMonMagic.AddForce(RigiMonMagic.transform.forward);
            ani.SetTrigger("IsAttack");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);


        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamageMon(float damage, Enum.Type type)
    {
        if (type == Win)
        {
            damage = damage * 2;
        }
        else if (type == Lose)
        {
            damage = damage / 2;
        }
        Health -= damage;

        if (Health <= 0)
        {
            ani.SetTrigger("IsDead");
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
        DropMana();
    }
    public void DropMana()
    {
        Vector3 position = transform.position;
        GameObject mana = Instantiate(Mana, position + new Vector3(0f, 1f, 0f), Quaternion.identity);
    }

    IEnumerator wait()
    {/*
        projectile = Instantiate(MonMagic, MagicMonShooot.transform.position, Mons.transform.rotation);
        Rigidbody RigiMonMagic = projectile.GetComponent<Rigidbody>();
        RigiMonMagic.AddForce(RigiMonMagic.transform.forward);
        yield return new WaitForSeconds(1f);
        RigiMonMagic.AddForce(RigiMonMagic.transform.forward * Magicspeed);
        ani.SetTrigger("IsAttack");
        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);*/

        yield return new WaitForSeconds(2f);

        /* projectile = Instantiate(MonMagic, MagicMonShooot.transform.position, Mons.transform.rotation);
          Rigidbody RigiMonMagic = projectile.GetComponent<Rigidbody>();
          RigiMonMagic.AddForce(RigiMonMagic.transform.forward * Magicspeed);*/
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

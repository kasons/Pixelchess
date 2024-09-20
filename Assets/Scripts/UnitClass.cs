using System.Collections.Generic;
using UnityEngine;

public class UnitClass : MonoBehaviour
{
    public HealthBar healthBar;

    // Unit stats
    public int health { get; set; }
    public int attack { get; set; }
    public float attackRange { get; set; }
    public float attackSpeed { get; set; }  // Number of times unit attacks per second
    public float movementSpeed { get; set; }

    protected string enemyTag = "";           // Tag to determine enemy units
    public bool enemyTargeted = false;
    public List<GameObject> enemies;       // Array of enemy units
    public GameObject targetedEnemy;

    private float attackCooldown;           // how long until unit can attack again

    private Animator animr;
    Rigidbody rb;

    public bool addList = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        DetermineEnemy();
        enemies = new List<GameObject>();
        animr = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }   

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GameMaster.instance.selectPhase == false)
        {
            if (addList)
            {
                enemies.AddRange(GameObject.FindGameObjectsWithTag(enemyTag));
                FindObjectOfType<AudioManager>().Stop("CharacterSelectMusic");
                FindObjectOfType<AudioManager>().Play("BattleMusic");
                addList = false;
            }

            if (enemyTargeted == false && enemies.Count > 0)
            {
                targetedEnemy = getClosestEnemy();
                transform.LookAt(targetedEnemy.transform);
            }
            else if (enemyTargeted == true && enemies.Count > 0)
            {
                // If enemy isn't in range, move towards them
                if (Vector3.Distance(transform.position, targetedEnemy.transform.position) > attackRange)
                {
                    float step = movementSpeed * Time.deltaTime;    // calculate distance to move
                    transform.position = Vector3.MoveTowards(transform.position, targetedEnemy.transform.position, step);
                    attackCooldown = Time.time + attackSpeed;
                }
                else
                {
                    if (Time.time >= attackCooldown)
                    {
                        attackCooldown = Time.time + attackSpeed;
                        Attack(targetedEnemy);
                    }
                }
            }
        }
    }


    /*
     * Checks self tag to determine enemy
     */
    public void DetermineEnemy()
    {
        if (gameObject.tag == "Player")
        {
            enemyTag = "Enemy";
        }
        else
        {
            enemyTag = "Player";
        }
    }

    /*
     * Gets all enemies then loops through them to find closest target
     */
    public GameObject getClosestEnemy()
    {
        GameObject closest = null;
        float closestDistanceSq = Mathf.Infinity ;
        Vector3 currentPos = transform.position;

        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 distToTarget = potentialTarget.transform.position - currentPos;
            float distToTargetSq = distToTarget.sqrMagnitude;

            if (distToTargetSq < closestDistanceSq)
            {
                closest = potentialTarget;
                closestDistanceSq = distToTargetSq;
            }
        }
        enemyTargeted = true;
        return closest;
    }

    /*
     * Tell enemy that damage was taken
     */
    public virtual void Attack(GameObject targetedEnemy)
    {
        if (enemies.Contains(targetedEnemy))
        {
            animr.SetTrigger("IsAttacking");
            FindObjectOfType<AudioManager>().Play("knight_attack");
            targetedEnemy.transform.SendMessage("TakeDamage", attack);
        }
    }

    /*
     * Calculate damage received
     */
    public void TakeDamage(int damage)
    {
        health -= damage;

        healthBar.SetHealth(health);

        if (health <= 0)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.transform.SendMessage("UpdateEnemyList", gameObject);
            }

            animr.SetTrigger("IsDead");
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            FindObjectOfType<AudioManager>().Play("death");
            Destroy(gameObject, 2);
        }
    }

    /*
     * Updates oppossing units enemies list after unit is destroyed
     */
    public void UpdateEnemyList(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
        enemyTargeted = false;
    }
}

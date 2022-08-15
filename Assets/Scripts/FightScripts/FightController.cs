using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] allies;
    GameObject closestObject;

    float moveSpeed = 3f;
    float minDistance = 1.5f;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int maxHealt = 100;
    public int currentHealt;
    public int attackDamage = 20;

    public float attackRate = 2f;
    float nextAttackTime = 0f;




    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        allies = GameObject.FindGameObjectsWithTag("Ally");
        currentHealt = maxHealt;
    }

    void Update()
    {
        MoveNearTheOpponent();
        if (Time.time >=nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f /attackRate;
        }
    }
    public GameObject FindClosestOpponent(GameObject[] opponents)
    {
        float oldDistance = 99999;

        foreach (var g in opponents)
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, g.transform.position);


            if (dist < oldDistance)
            {
                closestObject = g;
                oldDistance = dist;
            }
        }
        //Debug.Log(closestObject.name);
        return closestObject;

    }
    public void MoveNearTheOpponent()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        allies = GameObject.FindGameObjectsWithTag("Ally");

        if (gameObject.CompareTag("Enemy"))
        {
            if (Vector3.Distance(transform.position, FindClosestOpponent(allies).transform.position) > minDistance)
            {
                transform.LookAt(FindClosestOpponent(allies).transform.position);
                transform.position = Vector3.MoveTowards(transform.position, FindClosestOpponent(allies).transform.position, moveSpeed * Time.deltaTime);
                //Walk Animation
            }
        }
        if (gameObject.CompareTag("Ally"))
        {
            if (Vector3.Distance(transform.position, FindClosestOpponent(enemies).transform.position) > minDistance)
            {
                transform.LookAt(FindClosestOpponent(enemies).transform.position);
                transform.position = Vector3.MoveTowards(transform.position, FindClosestOpponent(enemies).transform.position, moveSpeed * Time.deltaTime);
                //Walk Animation
            }
        }

    }

    void Attack()
    {
        //Play an attack animation

       Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<FightController>().TakeDamage(attackDamage);
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint== null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
    public void TakeDamage(int damage)
    {
        currentHealt -= damage;

        //Hurt animation

        if (currentHealt<=0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log(gameObject.name + " Die");
        //Die animation
        GetComponent<Collider>().enabled = false;
        this.enabled = false;

    }

}

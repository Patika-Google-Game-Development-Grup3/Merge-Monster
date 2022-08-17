using System.Collections;
using UnityEngine;

public class FightController : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] allies;
    GameObject closestObject;
    
    public Transform attackPoint;
    public LayerMask enemyLayers;
    
    

    private float currentHealth;
    

    [SerializeField] private CharacterPropertiesSO _character;
    //private UIManager _uıManager;

    Animator animator;
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        allies = GameObject.FindGameObjectsWithTag("Ally");
        currentHealth = _character.CharacterHealth;
        animator = GetComponent<Animator>();
        //_uıManager = FindObjectOfType<UIManager>();
        //Debug.Log("Start: "+_uıManager.Gold);
        StartCoroutine("Reload");
    }

    void Update()
    {
        MoveNearTheOpponent();

    }
    public GameObject FindClosestOpponent(GameObject[] opponents)
    {
        float oldDistance = 99999;

        foreach (var g in opponents)
        {
            if (g.GetComponent<Collider>().enabled == true )
            {
                float dist = Vector3.Distance(this.gameObject.transform.position, g.transform.position);


                if (dist < oldDistance)
                {
                    closestObject = g;
                    oldDistance = dist;
                }
            }
            
        }
        return closestObject;

    }
    IEnumerator Reload()
    {
        while (true)
        {
            animator.SetBool("isRunning", false);
            Attack();
            yield return new WaitForSeconds(_character.CharacterAttackRate);
            animator.SetBool("isIdle", true);

        }
    }
    public void MoveNearTheOpponent()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        allies = GameObject.FindGameObjectsWithTag("Ally");

        if (gameObject.CompareTag("Enemy"))
        {
            if (Vector3.Distance(transform.position, FindClosestOpponent(allies).transform.position) > _character.CharacterAttackRange+1)
            {
                transform.LookAt(FindClosestOpponent(allies).transform.position);
                transform.position = Vector3.MoveTowards(transform.position, FindClosestOpponent(allies).transform.position, _character.CharacterMoveSpeed * Time.deltaTime);
                //Walk Animation
                animator.SetBool("isRunning", true);
            }
            

        }
        if (gameObject.CompareTag("Ally"))
        {
            if (Vector3.Distance(transform.position, FindClosestOpponent(enemies).transform.position) > _character.CharacterAttackRange+1)
            {
                transform.LookAt(FindClosestOpponent(enemies).transform.position);
                transform.position = Vector3.MoveTowards(transform.position, FindClosestOpponent(enemies).transform.position, _character.CharacterMoveSpeed * Time.deltaTime);
                //Walk Animation
                animator.SetBool("isRunning", true);
            }
        }
        

    }
    
    void Attack()
    {
        //Attack animation

       Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, _character.CharacterAttackRange, enemyLayers);

        if (hitEnemies != null)
        {
            foreach (Collider enemy in hitEnemies)
            {
                if (closestObject == enemy.gameObject)
                {
                    animator.SetTrigger("Attack");
                    enemy.GetComponent<FightController>().TakeDamage(_character.CharacterAttackPower);
                    //if (this.gameObject.CompareTag("Ally"))
                    //{
                    //    _uıManager.UpdateGold(_character.CharacterAttackPower);
                    //    Debug.Log("Atak: "+_uıManager.Gold);
                    //}

                }

            }
        }
        

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint== null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.transform.position, _character.CharacterAttackRange);
    }
    private void TakeDamage(float damage)
    {
        currentHealth -= damage;

        //Hurt animation
        //animator.SetTrigger("isHurt");

        if (currentHealth<=0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log(gameObject.name + " Die");
        //Die animation
        animator.SetBool("isDead", true);
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
    }

}

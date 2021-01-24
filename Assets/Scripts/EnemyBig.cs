using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBig : MonoBehaviour
{
    private string currentState = "IdleState";
    private Transform target;
    public float chaseRange = 1.0f;
    public Animator animator;
    public float speed = 3.0f;
    public int damage = 20;
    public int health = 100;
    public GameObject deathEffect;
    private bool isDead = false;
    public GameObject rewardPrefab;
    public GameObject explotionPrefab;
    public float deadDelay = 4.0f;
    public float attackRange = 4.0f;
    AnimationClip clip;
    //public float timeReward = 1.0f;
    private bool robotAttacks = false;
    private int shootOnce = 1;
    
    void Start(){
       target =  GameObject.FindGameObjectWithTag("Player").transform;
       //animator.SetBool("isDead", true);
    }
    void Update(){
        float distance =  Vector3.Distance(transform.position, target.position);
        if(currentState == "IdleState"){
            if (distance < chaseRange)
                Debug.Log("Detected hero!");
                currentState = "ChaseState";
        }else if(currentState == "ChaseState"){
            //play the run animation
            animator.SetTrigger("chase");
            animator.SetBool("isAttacking", false);

            if(distance < attackRange)
                currentState = "AttackState";

            //move towards the player
            if(target.position.x > transform.position.x){
                //move right
                transform.Translate(transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else{
                //move left
                transform.Translate(-transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.identity;
            }

        }
        else if(currentState == "AttackState"){
            animator.SetBool("isAttacking", true);
            //AttackPlayer();
            shootOnce+=1;
            if(shootOnce>50){
                shootOnce = 0;
            }
         if (distance > attackRange)
            currentState = "ChaseState";
        }
    }
    public void TakeDamage(int damage){
        health-= damage;
        if(health<=0){
           Die();
            Instantiate(explotionPrefab, transform.position, Quaternion.identity);
            Destroy(explotionPrefab,0.5f);
            Instantiate(rewardPrefab, transform.position, Quaternion.identity);
        }
    }
    void Die(){
        isDead = true;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        //StartCoroutine (destroyLater());
    }
    void OnTriggerEnter(Collider hitInfo) {
        PlayerControllerv2 player = hitInfo.GetComponent<PlayerControllerv2>();
        Debug.Log(hitInfo);
         if (player != null && player.playerIsDead == false){
             player.TakeDamage(damage);
         }
    }
    void AttackPlayer(){
        EnemyWeapon enemyWeapon = gameObject.GetComponent<EnemyWeapon>();
        if(shootOnce == 0){
            enemyWeapon.Shoot();
        }
    }
    IEnumerator destroyLater(){
        //animator.SetBool("isDead", true);
        yield return new WaitForSeconds(deadDelay);
        Destroy(gameObject);
    }
    // IEnumerator showReward(){
    //     yield return new WaitForSeconds(timeReward);
    //     Instantiate(rewardPrefab, transform.position, Quaternion.identity);
    // }
}
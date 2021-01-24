using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
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
    public float deadDelay = 1.0f;
    public float timeReward = 2.0f;
    
    void Start(){
       target =  GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update(){
        float distance =  Vector3.Distance(transform.position, target.position);
        if(currentState == "IdleState"){
            if(distance < chaseRange){
                currentState = "ChaseState";
            }else if(currentState == "ChaseState"){
                animator.SetTrigger("chase");
                if(target.position.x> transform.position.x){
                    transform.Translate(new Vector3(2,0,0)*speed * Time.deltaTime);
                }else{
                     transform.Translate(new Vector3(-2,0,0)*speed * Time.deltaTime);
                }
            }
        }
    }
    public void TakeDamage(int damage){
        health-= damage;
        if(health<=0){
            Die();
            StartCoroutine("showReward");
            // Instantiate(rewardPrefab, transform.position, Quaternion.identity);
        }
    }
    void Die(){
        isDead = true;
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        animator.SetBool("isDead", true);
        StartCoroutine("destroyLater");
    }
    void OnTriggerEnter(Collider hitInfo) {
        PlayerControllerv2 player = hitInfo.GetComponent<PlayerControllerv2>();
        Debug.Log(hitInfo);
         if (player != null && player.playerIsDead == false){
             player.TakeDamage(damage);
         }
    }
    IEnumerator destroyLater(){
        yield return new WaitForSeconds(deadDelay);
        Destroy(gameObject);
    }
    IEnumerator showReward(){
        yield return new WaitForSeconds(timeReward);
        Instantiate(rewardPrefab, transform.position, Quaternion.identity);
    }
}

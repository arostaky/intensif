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
                    transform.Translate(transform.right*speed * Time.deltaTime);
                }else{
                     transform.Translate(-transform.right*speed * Time.deltaTime);
                }
            }
        }
    }
}

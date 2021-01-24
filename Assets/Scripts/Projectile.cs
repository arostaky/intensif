using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public int damage = 40;
    // Start is called before the first frame update
    void Start(){
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider hitInfo) {
    PlayerControllerv2 player = hitInfo.GetComponent<PlayerControllerv2>();
        //FraiseBoy player  = hitInfo.GetComponent<FraiseBoy>();
        Debug.Log(hitInfo);
         if (player != null){
             player.TakeDamage(damage);
         }

         Destroy(gameObject);
    }
}

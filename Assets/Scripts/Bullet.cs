using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public int damage = 40;
    // Start is called before the first frame update
    void Start(){
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider hitInfo) {
        Enemy enemy  = hitInfo.GetComponent<Enemy>();

         if (enemy != null){
            enemy.TakeDamage(damage);
         }

         Destroy(gameObject);
    }

}
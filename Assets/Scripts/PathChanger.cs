using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathChanger : MonoBehaviour{
   
    void OnTriggerEnter(Collider other){
        
        PlayerController player = other.GetComponent<PlayerController>();
        // if(player.rotateL == true){
        //     transform.gameObject.tag = "RotateRight";
        // }
        // if(player.rotateR == true){
        //     transform.gameObject.tag = "RotateLeft";
        // }
      
       Destroy(gameObject);
    }
}

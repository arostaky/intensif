using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathChanger : MonoBehaviour
{
    //public GameObject player;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){
        Debug.Log("Do something i am the trigger..");
        // PlayerController player = other.GetComponent<PlayerController>();
        // player.RotatePlayer(0f);
        //player.transform.Rotate(0,0,0);
        Destroy(gameObject);

        // camera.transform.Rotate(0,-90f,0);
    }
}

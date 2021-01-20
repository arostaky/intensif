using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerv2 : MonoBehaviour{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    bool facingRight =  true;
    public GameObject childrenToRotate;

    public CharacterController controller;
    private bool rotateL = false, rotateR = false;
    public GameObject CameraToRotate;
    public float rotationSpeed = 2.5f;
    private void Awake() {
        controller = GetComponent<CharacterController>();
        if (controller == null){
            Debug.LogError("Character Controller not found.");
            enabled = false;
        }
    }
     void Update() {
         //original: CharacterController controller = GetComponent<CharacterController>();
         //moveDirection =  new Vector3(0,0,0);
         if (controller.isGrounded) {
             //moveDirection.z = Input.GetAxis("Horizontal");
             moveDirection = new Vector3(0, 0, Input.GetAxis("Horizontal"));
             moveDirection = transform.TransformDirection(moveDirection);
             moveDirection *= speed;
             if (Input.GetKey(KeyCode.W))
                 moveDirection.y = jumpSpeed;
             
         }
         //adding flip condition:
        Debug.Log(moveDirection.z);
        Debug.Log("facingright?" + facingRight);
        if(moveDirection.z < 0 ){
            facingRight = true;
            flip();
        }else if(moveDirection.z > 0){
            facingRight = false;
            flip();
        }
        if(rotateL == true){
            StartCoroutine("rotateLeft");
        }
        if(rotateR == true){
            StartCoroutine("rotateRight");
        }
         moveDirection.y -= gravity * Time.deltaTime;
         controller.Move(moveDirection * Time.deltaTime);
     }
     void flip(){        
        if(facingRight == true && rotateL == true && rotateR == false){
           childrenToRotate.transform.localRotation = Quaternion.Euler(0f,180f,0f);
        }
        if(facingRight == true && rotateL == false && rotateR == false){
             childrenToRotate.transform.localRotation = Quaternion.Euler(0f,0f,0f);
        }
        if(facingRight == false && rotateL == false && rotateR == false){
              childrenToRotate.transform.localRotation = Quaternion.Euler(0f,180f,0f);
        }
        if(facingRight == false && rotateL == true && rotateR == false){
             childrenToRotate.transform.localRotation = Quaternion.Euler(0f,0f,0f);
        }
        if(facingRight == false && rotateL == false && rotateR == true){
             childrenToRotate.transform.localRotation = Quaternion.Euler(0f,0f,0f);
        }
        if(facingRight == true && rotateL == false && rotateR == true){
             childrenToRotate.transform.localRotation = Quaternion.Euler(0f,180f,0f);
        }
    }
    void OnTriggerEnter(Collider other) {
        switch(other.tag){
            case "RotateLeft": 
                rotateL = true;
                rotateR = false;
                break;
            case "RotateRight":
                rotateL = false;
                rotateR = true;
                break;
        }
    }
    IEnumerator rotateLeft(){
        transform.localRotation = Quaternion.Slerp(transform.localRotation,Quaternion.Euler(new Vector3(0,0, 0)), Time.deltaTime * rotationSpeed);
        CameraToRotate.transform.localRotation = Quaternion.Slerp(CameraToRotate.transform.localRotation,Quaternion.Euler(new Vector3(0,-90, 0)), Time.deltaTime * rotationSpeed);
        yield return null;
    }
    IEnumerator rotateRight(){
        transform.localRotation = Quaternion.Slerp(transform.localRotation,Quaternion.Euler(new Vector3(0,90, 0)), Time.deltaTime * rotationSpeed);
        CameraToRotate.transform.localRotation = Quaternion.Slerp(CameraToRotate.transform.localRotation,Quaternion.Euler(new Vector3(0,0, 0)), Time.deltaTime * rotationSpeed);
        yield return null;
    }
}
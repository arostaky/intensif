using System;
using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10;
    public float _rotationSpeed = 90f;
 
    private Vector3 rotation;
    private Vector3 newRotation;
    private Vector3 currentPosition;

    public GameObject CameraToRotate;
    public float rotationSpeed = 2.5f;
    private bool rotateL = false;
    // private bool isRotating = false;
    private bool rotateR =  false;
    private bool groundedPlayer;
    //private Vector3 playerVelocity;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;
    private Vector3 move;
    private bool jump;
    void Start(){
        // newRotation = new Vector3(0,90f,0);
        // transform.Rotate(newRotation);
    }
    void Update(){
        //capture controls here:
        move = new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * Time.deltaTime);
        jump =  Input.GetButtonDown("Jump");
        
        //Vector3 move = new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * Time.deltaTime);
        move = transform.TransformDirection(move);
        controller.Move(move * speed);
        currentPosition = transform.position;
               
        groundedPlayer = controller.isGrounded;
        
        if (groundedPlayer && move.y < 0){
            move.y = 0f;
        }
         if ( jump && groundedPlayer){
            move.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        //Debug.Log(playerVelocity.y);
        move.y += gravity * Time.deltaTime;
        controller.Move(move * Time.deltaTime);
        // if (rotateL == true){
        //     StartCoroutine("rotateLeft");
        // }
        // if (rotateR == true){
        //      StartCoroutine("rotateRight");
        // }
        
    }
    void FixedUpdate(){
        
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
    public IEnumerator rotateLeft(){
        transform.localRotation = Quaternion.Slerp(transform.localRotation,Quaternion.Euler(new Vector3(0,0, 0)), Time.deltaTime * rotationSpeed);
        CameraToRotate.transform.localRotation = Quaternion.Slerp(CameraToRotate.transform.localRotation,Quaternion.Euler(new Vector3(0,-90, 0)), Time.deltaTime * rotationSpeed);
        yield return null;
    }
     public IEnumerator rotateRight(){
        transform.localRotation = Quaternion.Slerp(transform.localRotation,Quaternion.Euler(new Vector3(0,90, 0)), Time.deltaTime * rotationSpeed);
        CameraToRotate.transform.localRotation = Quaternion.Slerp(CameraToRotate.transform.localRotation,Quaternion.Euler(new Vector3(0,0, 0)), Time.deltaTime * rotationSpeed);
        yield return null;
    }
}
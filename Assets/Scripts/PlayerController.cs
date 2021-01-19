using System;
using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10;
 
    private Vector3 rotation;
    private Vector3 newRotation;
    private Vector3 currentPosition;

    public GameObject CameraToRotate;
    public float rotationSpeed = 2.5f;
    public bool rotateL = false;
    // private bool isRotating = false;
    public bool rotateR =  false;
    private bool groundedPlayer;
    //private Vector3 playerVelocity;
    public float gravity = -9.8f;
    public float jumpHeight = 10.0f;
    private Vector3 move;
    private bool jump;
    public Animator animator;
    public GameObject childrenToRotate;
    private Vector3 moveAir;
    bool facingRight =  true;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        if (controller == null){
            Debug.LogError("Character Controller not found.");
            enabled = false;
        }
    }
    void Start(){
        // newRotation = new Vector3(0,90f,0);
        // transform.Rotate(newRotation);
        move.y = 0f;
    }
    void Update(){
        if (controller == null)
            return;
        //capture controls here:
        //these two we should remove and put the movement under is grounded
        move = new Vector3(0, 0,0);
        moveAir = Vector3.zero;
        jump =  Input.GetKeyDown(KeyCode.W);
        move.z =  Input.GetAxisRaw("Horizontal") * speed;
        //controller.Move(move * speed);
        currentPosition = transform.position;
       // Debug.Log(move.x);
        groundedPlayer = controller.isGrounded;
        if(move.z < 0 && facingRight || move.x < 0 && facingRight){
            flip();
        }else if(move.z>0 && !facingRight || move.x > 0 && !facingRight){
            flip();
        }
        if (groundedPlayer){
            Debug.Log("Player is grounded");
            
            moveAir.y = 0f;
            if ( jump ){
                moveAir.y = jumpHeight;
               // moveAir.y = jumpHeight + speed * Time.deltaTime;
                //move.y = jumpHeight * Time.deltaTime;
                //move.y += Mathf.Sqrt(jumpHeight * 3.0f * Time.deltaTime);
            }
        }else{
            moveAir.y = gravity * Time.deltaTime;
        }
        if(rotateL == true){
            StartCoroutine("rotateLeft");
        }
        if(rotateR == true){
            StartCoroutine("rotateRight");
        }
        move = transform.TransformDirection(move);
        controller.Move(move * Time.deltaTime);
        controller.Move(moveAir *Time.deltaTime);
        //this will fix the animation if player rotates:
        animator.SetFloat("speed", Mathf.Abs(move.x + move.z*100.0f));
        
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
    void flip(){
        facingRight = !facingRight;
        childrenToRotate.transform.Rotate(0f,180f,0f);
        // this.gameObject.transform.rotation = Quaternion.Euler(0f,180f,0f);
    }
}
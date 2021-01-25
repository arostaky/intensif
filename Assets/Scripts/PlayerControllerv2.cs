using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerControllerv2 : MonoBehaviour{
    [SerializeField] private float moveSpeed = 0.0F;
    [SerializeField] private float walkSpeed = 3.0F;
    [SerializeField] private float runSpeed = 6.0F;
    // public float jumpSpeed = 8.0F;
    private Vector3 velocity;
    [SerializeField] private float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    bool facingRight =  true;
    public GameObject childrenToRotate;

    private CharacterController controller;
    private bool rotateL = false, rotateR = false;
    public GameObject CameraToRotate;
    public float rotationSpeed = 2.5f;
    private Animator animator;
    //jump:
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight;
    private bool isJumping = false;
    public bool playerIsDead = false;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    private int shootOnce = 0;
    public int lives = 1;
    
    // Rigidbody m_Rigidbody;
    
    private void Awake() {
        controller = GetComponent<CharacterController>();
        if (controller == null){
            Debug.LogError("Character Controller not found.");
            enabled = false;
        }
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    void Start(){
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        healthBar.setLives(lives);
        // m_Rigidbody = GetComponent<Rigidbody>();
        // m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
    }
     void Update() {
         //original: CharacterController controller = GetComponent<CharacterController>();
         //moveDirection =  new Vector3(0,0,0);
        //  if (controller.isGrounded) {
        //      //moveDirection.z = Input.GetAxis("Horizontal");
        //      moveDirection = new Vector3(0, 0, Input.GetAxis("Horizontal"));
        //      moveDirection = transform.TransformDirection(moveDirection);
        //      if (Input.GetKey(KeyCode.W))
        //          moveDirection.y = jumpSpeed;
        //  }
 
        // if (Input.GetKey(KeyCode.W))
        //          moveDirection.y = jumpSpeed;
        Move();
         //adding flip condition:
        // Debug.Log(moveDirection.z);
        // Debug.Log("facingright?" + facingRight);
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
        // Debug.Log("is jumping is? "+ isJumping);
        if(Input.GetButtonDown("Fire1")){
            Attack();
            shootOnce+=1;
            if(shootOnce>1){
                shootOnce = 0;
            }
            
        }
        // moveDirection.y -= gravity * Time.deltaTime;
         //animator.SetFloat("speed", Mathf.Abs(moveDirection.x + moveDirection.z));
     }
     void flip(){        
        if(facingRight == true && rotateL == true && rotateR == false){
           RotationBack(15f);
        }
        if(facingRight == true && rotateL == false && rotateR == false){
            RotationBack(15f);
        }
        if(facingRight == false && rotateL == false && rotateR == false){
            RotationBack(195f);
        }
        if(facingRight == false && rotateL == true && rotateR == false){
            RotationBack(195f);
        }
        if(facingRight == false && rotateL == false && rotateR == true){
             RotationBack(195f);
        }
        if(facingRight == true && rotateL == false && rotateR == true){
            RotationBack(15f);
            //childrenToRotate.transform.localRotation = Quaternion.Euler(0f,195f,0f);
        }
    }
    void RotationBack(float val){
        // childrenToRotate.transform.Rotate(0f,val,0f);
        childrenToRotate.transform.localRotation = Quaternion.Euler(0f,val,0f);
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
            case "deadFactorOne":
                TakeDamage(20);
                break;
            case "deadFactorTwo":
                TakeDamage(40);
                break;
            case "goLevelTwo":
                SceneManager.LoadScene("Level_Two");
                break;
        }
    }
    IEnumerator rotateLeft(){
        transform.localRotation = Quaternion.Slerp(transform.localRotation,Quaternion.Euler(new Vector3(0,-180,0)), Time.deltaTime * rotationSpeed);
        CameraToRotate.transform.localRotation = Quaternion.Slerp(CameraToRotate.transform.localRotation,Quaternion.Euler(new Vector3(0,90,0)), Time.deltaTime * rotationSpeed);
        yield return null;
    }
    IEnumerator rotateRight(){
        transform.localRotation = Quaternion.Slerp(transform.localRotation,Quaternion.Euler(new Vector3(0,0,0)), Time.deltaTime * rotationSpeed);
        CameraToRotate.transform.localRotation = Quaternion.Slerp(CameraToRotate.transform.localRotation,Quaternion.Euler(new Vector3(0,0,0)), Time.deltaTime * rotationSpeed);
        yield return null;
    }
    private void Move(){
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        moveDirection = new Vector3(0, 0, Input.GetAxis("Horizontal"));
        moveDirection = transform.TransformDirection(moveDirection);
        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }
        if(isGrounded){
            if(moveDirection != Vector3.zero && !Input.GetButton("Fire3")){
                Walk();
            }
            else if(moveDirection != Vector3.zero && Input.GetButton("Fire3")){
                Run();
            
            }
            else if(moveDirection == Vector3.zero){
                Idle();
            }
            if(Input.GetButtonDown("Fire2")){
                Jump();
                animator.SetBool("Jump", true);
                isJumping = true;
            }
        }
        if(!isGrounded && isJumping){
            animator.SetBool("Jump", false);
            isJumping = false;
        }
        moveDirection *= moveSpeed;
        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void Idle(){
        animator.SetFloat("Speed",0, 0.1f, Time.deltaTime);
    }
    private void Walk(){
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed",1, 0.1f, Time.deltaTime);
    }
    private void Run(){
        moveSpeed = runSpeed;
        animator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }
    private void Jump(){
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
    private void Attack(){
        if(shootOnce == 0){
            animator.SetTrigger("Attack");
        }else{
            animator.SetTrigger("Attack2");
        }
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        if(currentHealth<=0){
            countLives();
            Die();
        }
    }
    public void countLives(){
        lives--;
    }
    public void Die(){
        controller.enabled = false;
        animator.SetBool("Death", true);
        // mouvement.y = 0;
        // mouvement.x = 0;
        // Destroy(gameObject);
        // Invoke("MoveBody", reSpawnTime);
        playerIsDead = true;
        healthBar.setLives(lives);
        if(lives <= 0){
           StartCoroutine(restartLevel());
            
        }
        // Debug.LogError("die");
    }
    IEnumerator restartLevel(){
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Level_One");
    }
}
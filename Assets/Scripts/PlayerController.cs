using UnityEngine;
 
public class PlayerController : MonoBehaviour
{
    public CharacterController _controller;
    public float _speed = 10;
    public float _rotationSpeed = 90f;
 
    private Vector3 rotation;
    private Vector3 newRotation;
    private Vector3 currentPosition;
    void Start(){
        newRotation = new Vector3(0,90f,0);
        this.transform.Rotate(newRotation);
    }
    public void Update()
    {
        // this.rotation = new Vector3(0, _rotationSpeed * Time.deltaTime, 0);
 
        Vector3 move = new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * Time.deltaTime);
        move = transform.TransformDirection(move);
        _controller.Move(move * _speed);
        currentPosition = transform.position;
        
    }
    // public void RotatePlayer(float rotationValue){
    //     Debug.Log("now i will rotate!");
    //     _controller.Move(new Vector3(0f,0f,0f));
    //      transform.Rotate(0f,rotationValue,0f);
    //     // this.transform.Rotate(new Vector3(0f,rotationValue,0f));
    //     //newRotation = new Vector3(0,rotationValue,0);
    // }
    void OnTriggerEnter(Collider other) {
        // _controller.enabled = false;
        switch(other.tag){
            case "RotateLeft": 
                // transform.position = currentPosition;
                transform.localRotation =  Quaternion.Euler(new Vector3(0, 0, 0));
                // transform.Rotate(0,0,0); 
                Debug.Log("I will rotate left");
                // _controller.enabled = true; 
                break;
            case "RotateRight": transform.Rotate(0,-90,0); break;
            //case "Finish": FinishGame(); break;
        }
    }
}
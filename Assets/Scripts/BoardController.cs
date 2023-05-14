using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    private Transform truck1;
    private Transform truck2;
    private Transform turnPoint;
    private TrickController tc;
    
    private float pushSpeed = 30.0f;
    private float groundDist;
    private float turnAnglePerFixedUpdate;

    public bool flipped = false;

    void Start()
    {
        //Get our Game Objects.
        rb = GetComponent<Rigidbody>();
        tc = GetComponent<TrickController>();
        groundDist = GetComponent<Collider>().bounds.extents.y;
        truck1 = GameObject.Find("Truck").GetComponent<Transform>();
        truck2 = GameObject.Find("Truck2").GetComponent<Transform>();
        turnPoint = GameObject.Find("TurnPoint").GetComponent<Transform>();
    }

    void Update()
    {
        // ProcessInputs();
        //Check if the board has flipped by checking for the y component on the transform within a threshold, if it is also grounded.
        if(transform.up.y > 0){
            flipped = false;
        }
        else{
            flipped = true;
        }
        //Flip the board over, used for testing.
        // if(Input.GetKeyDown(KeyCode.F) && flipped){
        //     Flip();
        // }
        //Turn the board, used for testing.
        // Turn();
        // Debug.Log("Flipped?:" + flipped);
        // Debug.Log("Grounded?:" + IsGrounded());
        // Debug.Log(transform.up.y);
    }

    //Set the y rotation of the board to 0 to flip it back on its wheels.
    public void Flip(){
        transform.rotation = new Quaternion(0,transform.rotation.x,0,transform.rotation.z);
        flipped = false;
    }
    
    //Input processing used for testing.
    private void ProcessInputs(){
        float push = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(rb.velocity.x, rb.velocity.y, push);
    }
    
    // Check for a raycast hit when casting down from the trucks to check if the board is grounded.
    public bool IsGrounded(){
      return Physics.Raycast(truck1.position, -Vector3.up, groundDist + 0.1f) || Physics.Raycast(truck2.position, -Vector3.up, groundDist + 0.1f);
    }
    
    //Push the board.
    public void Push(){
        tc.pushing = true;
    }
    
    //Stop pushing the board.
    public void StopPushing(){
        tc.pushing = false;
    }
    
    // If the player has forward speed, apply a braking force in the opposite direction until they have stopped.
    public void Brake(){
        if(IsGrounded() && rb.velocity.magnitude < 0){
            rb.AddRelativeForce(0, 0, -1 * pushSpeed);
        }
    }
    
    //Rotate the player, used for testing.
    private void Turn(){
        if(IsGrounded() && !flipped){
        rb.transform.Rotate(0,Input.GetAxisRaw("Horizontal")/20f,0);
        }
    }
    
    //Turn the player to the right.
    public void TurnRight(){
        if(IsGrounded() && !flipped){
        rb.transform.Rotate(0,1/20f,0);
        }
    }
    
    //Turn the player to the left.
    public void TurnLeft(){
        if(IsGrounded() && !flipped){
        rb.transform.Rotate(0,-1/20f,0);
        }
    }
}

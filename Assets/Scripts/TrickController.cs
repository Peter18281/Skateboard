using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class TrickController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform rFoot;
    private Transform lFoot;
    private BoardController bc;
    private CinemachineVirtualCamera skateCam;
    private TrickControls controls;
    private GyroInput gyro;
    private Transform spawnPoint;
    private UI ui;

    private float pushSpeed = 2.0f;

    public bool pushing = false;

    void Start()
    {
        //Enable the controls;
        controls = new TrickControls();
        controls.Gameplay.Enable();
        gyro = GetComponent<GyroInput>();
        
        //Gather our Game Objects. Set angular velocity.
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 15;
        bc = GetComponent<BoardController>();
        ui = GameObject.Find("Canvas").GetComponent<UI>();
        spawnPoint = GameObject.Find("Spawnpoint").GetComponent<Transform>();
        rb.transform.position = spawnPoint.transform.position;
        rFoot = GameObject.Find("RightFoot").GetComponent<Transform>();
        lFoot = GameObject.Find("LeftFoot").GetComponent<Transform>();
        skateCam = GameObject.Find("SkateCam").GetComponent<CinemachineVirtualCamera>();

        //Input Actions.
        controls.Gameplay.Pop.performed += ctx => Pop();
        controls.Gameplay.Lift.performed += ctx => Lift();
        controls.Gameplay.Kick.performed += ctx => Kick();
        controls.Gameplay.Heel.performed += ctx => Heel();
        controls.Gameplay.Shuvit.performed += ctx => Shuvit();
        controls.Gameplay.Flip.performed += ctx => bc.Flip();
        controls.Gameplay.Push.started += ctx => bc.Push();
        controls.Gameplay.Push.canceled += ctx => bc.StopPushing();
        controls.Gameplay.Brake.performed += ctx => bc.Brake();
        controls.Gameplay.Reset.performed += ctx => Reset();
        controls.Gameplay.TurnRight.performed += ctx => bc.TurnRight();
        controls.Gameplay.TurnLeft.performed += ctx => bc.TurnLeft();
        controls.Gameplay.Pause.performed += ctx => ui.PauseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        //Test Keyboard Tricks.
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     Ollie();
        // }
        // if(Input.GetKeyDown("1")){
        //     Kickflip();
        // }
        // if(Input.GetKeyDown("2")){
        //     PopShuvit();
        // }
        // if(Input.GetKeyDown(KeyCode.Mouse0)){
        //     Land();
        // }
        
        //Gyro braking and turning.
        if(gyro.finalRot.z < -0.001){
            bc.TurnRight();
        }
        else if(gyro.finalRot.z > 0.001){
            bc.TurnLeft();
        }
        if(gyro.finalRot.x < -0.001){
            bc.Brake();
        }

        //Push the Board.
        if(pushing == true && bc.IsGrounded() && !bc.flipped){
        rb.AddRelativeForce(0, 0, 1 * pushSpeed);
        }
    }
    
    //Forces of the tricks, broken down into their individual movements.
    //Forces are applied where the feet would be and are relative to the board.
    private void Pop(){
        if(bc.IsGrounded()){
      Vector3 upForce = new Vector3(0,8,0);
      rb.AddForceAtPosition(upForce, lFoot.position,ForceMode.Impulse);
        }
    }

    private void Lift(){
        if(!bc.IsGrounded()){
      Vector3 upForce = new Vector3(0,4,0);
      rb.AddForceAtPosition(upForce, rFoot.position,ForceMode.Impulse);
        }
    }

    private void Slide(){
        if(bc.IsGrounded()){
        Vector3 force = new Vector3(0,-3,0);
        rb.AddForceAtPosition(force, lFoot.position,ForceMode.Impulse);
        }
    }

    private void Kick(){
        if(!bc.IsGrounded()){
        Vector3 force = new Vector3(0,0,1f);
        rb.AddRelativeTorque(force, ForceMode.Impulse);
        }
    }

    private void Heel(){
        if(!bc.IsGrounded()){
        Vector3 force = new Vector3(0,0,-1f);
        rb.AddRelativeTorque(force, ForceMode.Impulse);
            }    
        }

    private void Shuvit(){
        if(!bc.IsGrounded()){
        Vector3 force = new Vector3(0,0,6);
        rb.AddForceAtPosition(force, rFoot.position,ForceMode.Impulse);
        }
    }

    private void Weight(){
        if(!bc.IsGrounded()){
        Vector3 force = new Vector3(0,-1,0);
        rb.AddForceAtPosition(force, rFoot.position,ForceMode.Impulse);
        }
    }
    
    //Trick functions used for testing.
    private void Ollie(){ 
        if(bc.IsGrounded()){
        Pop();
        Invoke("Lift", 0.1f);
        Invoke("Slide", 0.2f);
        Invoke("Weight", 0.3f);
        }
    }

    private void Kickflip(){ 
        if(bc.IsGrounded()){
        Pop();
        Invoke("Lift", 0.1f);
        Invoke("Kick", 0.2f);
        Invoke("Slide", 0.3f);
        }
    }

    private void Heelflip(){ 
        if(bc.IsGrounded()){
        Pop();
        Invoke("Lift", 0.1f);
        Invoke("Heel", 0.2f);
        Invoke("Slide", 0.3f);
        }
    }

    private void PopShuvit(){ 
        if(bc.IsGrounded()){
        Pop();
        Invoke("Lift", 0.1f);
        Invoke("Shuvit", 0.2f);
        Invoke("Slide", 0.3f);
        }
    }
    
    //Downward force on board to land.
    private void Land(){
        if(!bc.IsGrounded()){
            Vector3 downForce = new Vector3(0,-10,0);
            rb.AddRelativeForce(downForce,ForceMode.Impulse);
        }
    }

    public void Reset(){
        rb.transform.position = spawnPoint.transform.position;
    }
}
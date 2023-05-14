using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    AudioSource audioSource;
    BoardController bc;
    private Rigidbody rb;
    private bool rolling = false;

    void Start(){
        //Get our Game Objects.
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoardController>();
    }

    void Update(){
        Rolling();
        if(rolling){
            audioSource.Play(0);
        }
    }
    
    private void Rolling(){
        //Play rolling sound when player reaches certain speed.
       if(rb.velocity.magnitude > 5 && bc.IsGrounded()){
        rolling = true;
       }
       else{
        rolling = false;
        audioSource.Stop();
       }
    }
}

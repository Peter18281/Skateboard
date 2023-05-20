using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    BoardController bc;
    public AudioClip push;
    private Rigidbody rb;
    private bool rolling = false;
    public AudioClip pop;
    public AudioClip land;

    void Start(){
        //Get our Game Objects.
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoardController>();
    }

    void Update(){
        Rolling();
        if(rolling){
            if(!audioSource.isPlaying){
            audioSource.PlayOneShot(push);
            }
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

    public void PopSound(){
        audioSource.PlayOneShot(pop);
    }
}

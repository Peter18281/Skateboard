using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCrowd : MonoBehaviour
{
    AudioSource audioSource;
    int random;

    void Start(){
        //Get our Game Object.
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        //Play the cheering at random intervals.
       random = Random.Range(1,10000);
       if(random == 666){
        audioSource.Play(0);
       } 
    }
}

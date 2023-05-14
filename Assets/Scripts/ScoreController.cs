using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int score;
    public int currentTrick;
    public int currentAir;
    public int currentShuvit;
    public int currentFlip;
    public int multiplier = 0;
    public float timer = 10f;

    bool airborne;
    bool shuvin;
    bool flippin;
    public bool practice;
    public bool start = false;

    private BoardController bc;
    private Rigidbody rb;
    private UI ui;
    
    void Start(){
       //Get Our Game Objects.
       bc = GetComponent<BoardController>();
       rb = GetComponent<Rigidbody>();
       ui = GameObject.Find("Canvas").GetComponent<UI>();
    }

    void Update(){
        //Check for timer if not in practice mode.
        if(start){
        timer -= Time.deltaTime;
        }
        if(timer<=0 && !practice){
            timerEnded();
        }

        //Check the scores and add them to the current trick total.
        currentTrick = (currentAir + currentShuvit + currentFlip) * multiplier;
        AirTime();
        FlipScore();
        ShuvitScore();
        Multiplier();

        //if player doesn't land the trick, reset the trick score.
        if(bc.flipped && bc.IsGrounded()){
            ResetTrick();
        }

        //add the trick if landed to the main score.
        AddTrick();
    }

    private void Multiplier(){
        //Change the multiplier depending on what the player is doing.
        if (airborne && flippin && shuvin){
            multiplier = 3;
        }
        else if (airborne && flippin || airborne && shuvin){
            multiplier = 2;
        }
        else if(shuvin || flippin || airborne){
            multiplier = 1;
        }
        else{
            multiplier = 0;
        }
    }

    public void AddTrick(){
        //Add the trick score to the main score if it is landed properly.
        if(bc.IsGrounded() && !bc.flipped && currentTrick > 0){
        score += currentTrick;
        ui.success.text = "SUCCESS!";
        Invoke("ClearSuccess", 1f);
        }
    }

    void ClearSuccess(){
        //Clear the success text.
        ui.success.text = "";
    }

    public void ResetTrick(){
        //Reset the scores of the variables of the trick.
        currentAir = 0;
        currentShuvit = 0;
        currentFlip = 0;
    }

    public void AirTime(){
       //Add score for time spend in the air.
       if(!bc.IsGrounded()){
        currentAir++;
        airborne = true;
       }
       else{
        airborne = false;
       }
    }

    public void ShuvitScore(){
        //Add score for the shuvit movement.
        if(rb.angularVelocity.x < -1 || rb.angularVelocity.x > 1){
        currentShuvit++;
        shuvin = true;
       }
       else{
        shuvin = false;
       }
    }

    public void FlipScore(){
       //Add score for the flip movement.
       if(rb.angularVelocity.y < -1 || rb.angularVelocity.y > 1){
        currentFlip++;
        flippin = true;
       }
       else{
        flippin = false;
       }
    }

    public void timerEnded(){
        //Add up the score at the end of the timer.
        ui.ScoreAddUp();
    }
}

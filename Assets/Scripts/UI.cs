using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    TMP_Text trickScore;
    TMP_Text finalScore;
    TMP_Text score;
    TMP_Text airScore;
    TMP_Text shuvScore;
    TMP_Text flipScore;
    TMP_Text multiplier;
    TMP_Text timer;
    TMP_Text flipOver;
    GameObject Tutorial;
    GameObject Pause;
    Transform endPos;
    bool paused;
    public TMP_Text success;

    ScoreController sc;
    TrickController tc;
    BoardController bc;

    void Start()
    {
        //Get our Game Objects.
        sc = GameObject.Find("Board").GetComponent<ScoreController>();
        tc = GameObject.Find("Board").GetComponent<TrickController>();
        bc = GameObject.Find("Board").GetComponent<BoardController>();
        trickScore = GameObject.Find("TrickScore").GetComponent<TMP_Text>();
        flipOver = GameObject.Find("FlipOver").GetComponent<TMP_Text>();
        airScore = GameObject.Find("AirScore").GetComponent<TMP_Text>();
        shuvScore = GameObject.Find("ShuvScore").GetComponent<TMP_Text>();
        flipScore = GameObject.Find("FlipScore").GetComponent<TMP_Text>();
        finalScore = GameObject.Find("FinalScore").GetComponent<TMP_Text>();
        score = GameObject.Find("Score").GetComponent<TMP_Text>();
        multiplier = GameObject.Find("Multiplier").GetComponent<TMP_Text>();
        success = GameObject.Find("Success").GetComponent<TMP_Text>();
        timer = GameObject.Find("Timer").GetComponent<TMP_Text>();
        Tutorial = GameObject.Find("Tutorial");
        Pause = GameObject.Find("Pause");
        endPos = GameObject.Find("EndPos").GetComponent<Transform>();
        
        //Disable/Enable parts of the UI.
        tc.Disable();
        Tutorial.SetActive(true);
        finalScore.gameObject.SetActive(false);
        Pause.SetActive(false);
    }

    void Update()
    {
        //If we're in practice, show practice, if not show the timer.
        if(!sc.practice && (Mathf.FloorToInt(sc.timer/60f)+1) != 0){
           timer.text = "" + Mathf.FloorToInt(sc.timer/60f) + ":" + (Mathf.FloorToInt(sc.timer)/(Mathf.FloorToInt(sc.timer/60f)+1));
        }
        else{
           timer.text = "Practice";
        } 

        Flipped();
        
        //Increment scores when conditions are met.
        AirScore();
        FlipScore();
        Multiplier();
        ShuvScore();
        Score();
        
        //Increment end score display to real score then stop.
        score.text = "" + sc.score;

        MoveTut();
    }

    void AirScore(){
        //Air Score Text.
        if(sc.currentAir > 10){
           airScore.text = "Air Time: " + sc.currentAir/5;
        }
        else{
            airScore.text = null;
        }
    }

    void ShuvScore(){
        //Shuvit Score Text.
        if(sc.currentShuvit > 10){
            shuvScore.text = "Shuvin: " + sc.currentShuvit/5;
        }
        else{
            shuvScore.text = null;
        }
    }

    void FlipScore(){
        //Flip Score Text.
        if(sc.currentFlip > 10){
            flipScore.text = "Flippin: " + sc.currentFlip/5;
        }
        else{
            flipScore.text = null;
        }
    }

    void Multiplier(){
        //Multiplier Score Text.
        if(sc.multiplier > 1){
         multiplier.text = sc.multiplier + "x";
        }
        else{
            multiplier.text = null;
        }
    }

    void Score(){
        //Trick Score Text.
        if(sc.currentTrick > 10){
            trickScore.text = "Trick Total: " + sc.currentTrick/5;
        }
        else{
            trickScore.text = null;
        }
    }

    public void ScoreAddUp(){
        //Disable all trick UI and active end UI.
        finalScore.gameObject.SetActive(true);
        trickScore.gameObject.SetActive(false);
        airScore.gameObject.SetActive(false);
        shuvScore.gameObject.SetActive(false);
        flipScore.gameObject.SetActive(false);
        success.gameObject.SetActive(false);
        multiplier.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        tc.Disable();
    }

    public void Retry(){
        //Restart the run.
      SceneManager.LoadScene("TimedRun");
    }

    public void Quit(){
        //Back to Main Menu
      SceneManager.LoadScene("MainMenu");
    }

    public void EndTut(){
        //Close the Tutorial.
        tc.Enable();
        Tutorial.SetActive(false);
    }

    public void PauseMenu(){
        //Pause/Unpause the Game.
        if(!Tutorial.active){
        if(!paused){
        Time.timeScale = 0f;
        Pause.SetActive(true);
        paused = true;
        }
        else{
        Time.timeScale = 1f;
        Pause.SetActive(false);
        paused = false;
        }
        }
    }

    private void MoveTut(){
        if(Tutorial.transform.position != endPos.position){
            Tutorial.transform.position = Vector3.MoveTowards(Tutorial.transform.position, endPos.position, 600 * Time.deltaTime);
        }
    }

    public void StartTime(){
        sc.start = true;
    }

    public void Flipped(){
        if(bc.IsGrounded() && bc.flipped){
         flipOver.gameObject.SetActive(true);
        }
        else{
            flipOver.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    TMP_Text gyroB;

    public void Start(){
        gyroB = GameObject.Find("GyroT").GetComponent<TMP_Text>();
        PlayerPrefs.SetInt("gyro", 1);
    }

    public void Update(){
        if(PlayerPrefs.GetInt("gyro") == 1){
            gyroB.text = "Gyro: Enabled";
        }
        else{
            gyroB.text = "Gyro: Disabled";
        }
    }
    
    public void StartGame(){
        //Load the timed run.
        SceneManager.LoadScene("TimedRun");
    }

    public void Practice(){
        //Load Practice.
        SceneManager.LoadScene("Practice");
    }

    public void Quit(){
        //Quit to desktop.
        Application.Quit();
    }

     public void EnDisGyro(){
        if(PlayerPrefs.GetInt("gyro") == 1){
           PlayerPrefs.SetInt("gyro", 0);
        }
        else{
           PlayerPrefs.SetInt("gyro", 1);
        }
}
}

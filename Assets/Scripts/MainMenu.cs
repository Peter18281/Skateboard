using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
}

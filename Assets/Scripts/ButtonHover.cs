using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    private Image button;
    private Color ogColor;
    private Color hoverColor;

    void Start(){
        button = GetComponent<Image>();
        ogColor = button.color;
        hoverColor = Color.white;
    }

    public void changeWhenHover(){
        button.color = hoverColor;
    }

    public void changeWhenLeaves(){
        button.color = ogColor;
    }
}

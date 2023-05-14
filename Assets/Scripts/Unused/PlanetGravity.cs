using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    private Transform planet;
    private Rigidbody rb;
    private float gravity = 15f;

    public bool autoOrient = false;
    public float autoOrientSpeed = 1f;

    void Awake(){
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        ProcessGravity();
    }

    void ProcessGravity(){
        Vector3 diff = transform.position - planet.position;
        rb.AddForce(-diff.normalized * gravity * rb.mass);
    }
}

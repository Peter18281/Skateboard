using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private Rigidbody room;
    private Rigidbody rb;
    private Vector3 rotationAngle;
    private Vector3 rotationAxis;

    private float rotationSpeed = 40;
    private bool rotate = false;

    void Start()
    {
        //Get our Game Objects.
        room = GameObject.Find("Floor").GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        if(rotate){
          room.transform.RotateAround(rb.transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
        }
        if(room.transform.eulerAngles == rotationAngle){
            rotate = false;
          }
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.name=="BR"){
            rotationAxis = new Vector3(0,0,1);
            rotationAngle = new Vector3(0,0,90);
            rotate = true;
        }
    }
}

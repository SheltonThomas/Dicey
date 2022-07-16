using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Quaternion startingRotation;
    public float speed = 25;
    private bool moving = false;
    public float rotationSpeed = 25;
    public float hitboxSize = 1.5f;
    private LayerMask blockMask = 0;
    private Vector3 destination;

    void Start(){
    }

    void Update(){
        if (!moving)
        {
            if(Input.GetAxis("Horizontal") > 0){
                if(CheckDirection(Vector3.right)){
                    moving = true;
                    destination = transform.position + Vector3.right;
                    StopAllCoroutines();
                    StartCoroutine(Rotate(new Vector3(0, -90, 0)));
                }
            }
            else if(Input.GetAxis("Horizontal") < 0){
                if(CheckDirection(Vector3.left)){
                    moving = true;
                    destination = transform.position + Vector3.left;
                    StopAllCoroutines();
                    StartCoroutine(Rotate(new Vector3(0, 90, 0)));
                }
            }
            else if(Input.GetAxis("Vertical") > 0){
                if(CheckDirection(Vector3.up)){
                    moving = true;
                    destination = transform.position + Vector3.up;
                    StopAllCoroutines();
                    StartCoroutine(Rotate(new Vector3(90, 0, 0)));
                }
            }
            else if(Input.GetAxis("Vertical") < 0){
                if(CheckDirection(Vector3.down)){
                    moving = true;
                    destination = transform.position + Vector3.down;
                    StopAllCoroutines();
                    StartCoroutine(Rotate(new Vector3(-90, 0, 0)));
                }
            }
        }
    }

    IEnumerator Rotate(Vector3 rotationAmount){
        startingRotation = this.transform.rotation;
        Quaternion finalRotation = Quaternion.Euler( rotationAmount.x, rotationAmount.y, rotationAmount.z ) * startingRotation;
        Vector3 finalPosition = destination;

        while(this.transform.rotation != finalRotation){
            float time = Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalRotation, time * rotationSpeed);
            this.transform.position = Vector3.Lerp(this.transform.position, finalPosition, time * speed);
            yield return 0;
        }
        moving = false;
    }

    private bool CheckDirection(Vector3 direction){
        if (Physics.Raycast( transform.position, direction, out RaycastHit hit, hitboxSize)) {
            return false;
        }
        else return true;
    }
}
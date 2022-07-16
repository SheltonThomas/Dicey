using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 25f;
    public float rotationSpeed = 25f;
    public float hitboxSize = 1f;
    private LayerMask blockMask = 0;    // Start is called before the first frame update
    private Vector3 destination;
    private Quaternion rotation;
    private bool moving;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if (!moving)
        {
            if(Input.GetAxis("Horizontal") > 0){
                if(CheckDirection(Vector3.right)){
                    destination = transform.position + Vector3.right;
                    rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, -90f, 0f));
                    moving = true;
                    StartCoroutine(RotateAndMove());
                }
            }
            if(Input.GetAxis("Horizontal") < 0){
                if(CheckDirection(Vector3.left)){
                    destination = transform.position + Vector3.left;
                    rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 90f, 0f));
                    moving = true;
                    StartCoroutine(RotateAndMove());
                }
            }
            if(Input.GetAxis("Vertical") > 0){
                if(CheckDirection(Vector3.up)){
                    destination = transform.position + Vector3.up;
                    rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(-90f, 0f, 0f));
                    moving = true;
                    StartCoroutine(RotateAndMove());
                }
            }
            if(Input.GetAxis("Vertical") < 0){
                if(CheckDirection(Vector3.down)){
                    destination = transform.position + Vector3.down;
                    rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(90f, 0f, 0f));
                    moving = true;
                    StartCoroutine(RotateAndMove());
                }
            }
        }
    }

    IEnumerator RotateAndMove(){
        Quaternion finalRotation = rotation * transform.rotation;
        Vector3 finalPosition = destination;
        while(this.transform.rotation != finalRotation && this.transform.position != finalPosition){
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalRotation, Time.deltaTime*rotationSpeed);
            this.transform.position = Vector3.Lerp(this.transform.position, finalPosition, Time.deltaTime*speed);
            yield return 0;
        }
            moving = false;
    }

    private bool CheckDirection(Vector3 direction){
        if (Physics.Raycast(
            transform.position,
            direction,
            out RaycastHit hit,
            hitboxSize,
            blockMask
        ))
        {
            return false;
        }
        else return true;
    }

}

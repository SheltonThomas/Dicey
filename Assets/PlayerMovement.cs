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
    //[SerializeField]
    //private LevelManager levelManager;

    void Start(){
    }

    void Update(){
        if (!moving/* && levelManager.isPlayerTurn*/)
        {
            if(Input.GetAxis("Horizontal") > 0){
                if(CheckDirection(Vector3.right)){
                    moving = true;
                    destination = transform.position + Vector3.right;
                    StopAllCoroutines();
                    StartCoroutine(Rotate(new Vector3(0, 0, -90)));
                }
            }
            else if(Input.GetAxis("Horizontal") < 0){
                if(CheckDirection(Vector3.left)){
                    moving = true;
                    destination = transform.position + Vector3.left;
                    StopAllCoroutines();
                    StartCoroutine(Rotate(new Vector3(0, 0, 90)));
                }
            }
            else if(Input.GetAxis("Vertical") > 0){
                if(CheckDirection(Vector3.up)){
                    moving = true;
                    destination = transform.position + Vector3.forward;
                    StopAllCoroutines();
                    StartCoroutine(Rotate(new Vector3(90, 0, 0)));
                }
            }
            else if(Input.GetAxis("Vertical") < 0){
                if(CheckDirection(Vector3.down)){
                    moving = true;
                    destination = transform.position + Vector3.back;
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

        Debug.DrawRay(transform.position, Vector3.up, Color.red, 5);
        Debug.Log(Physics.Raycast(transform.position, Vector3.up, 1));
        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 1))
        {
            hit.collider.gameObject.GetComponent<FaceInfo>().isAttacking = true;
        }

        //levelManager.isPlayerTurn = false;
        moving = false;
    }

    private bool CheckDirection(Vector3 direction){
        if (Physics.Raycast( transform.position, direction, out RaycastHit hit, hitboxSize, 3)) {
            return false;
        }
        else return true;
    }
}
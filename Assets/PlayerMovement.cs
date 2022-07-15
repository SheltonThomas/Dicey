using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    public float hitboxSize = 1f;
    private LayerMask blockMask = 0;    // Start is called before the first frame update
    private Vector3 destination;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){

        if(Vector3.Distance(transform.position, destination) < Mathf.Epsilon){
            if(Input.GetAxis("Horizontal") > 0){
                if(CheckDirection(Vector3.right)){
                    destination = transform.position + Vector3.right;
                }
            }
            if(Input.GetAxis("Horizontal") < 0){
                if(CheckDirection(Vector3.left)){
                    destination = transform.position + Vector3.left;
                }
            }
            if(Input.GetAxis("Vertical") > 0){
                if(CheckDirection(Vector3.up)){
                    destination = transform.position + Vector3.up;
                }
            }
            if(Input.GetAxis("Vertical") < 0){
                if(CheckDirection(Vector3.down)){
                    destination = transform.position + Vector3.down;
                }
            }

        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
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

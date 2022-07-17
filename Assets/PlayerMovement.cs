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
    [SerializeField]
    private LevelManager levelManager;
    private PlayerHealth playerhealth;
    public bool canMove = true;

    void Start(){
        playerhealth = GetComponent<PlayerHealth>();
    }

    void Update(){
        if (!moving && levelManager.isPlayerTurn && canMove)
        {
            if(Input.GetAxis("Horizontal") > 0){
                if(CheckDirection(Vector3.right)){
                    moving = true;
                    destination = transform.position + Vector3.right;
                    StopAllCoroutines();
                    StartCoroutine(Rotate(new Vector3(0, 0, -90), "Right"));
                }
            }
            else if(Input.GetAxis("Horizontal") < 0){
                if(CheckDirection(Vector3.left)){
                    moving = true;
                    destination = transform.position + Vector3.left;
                    StopAllCoroutines();
                    StartCoroutine(Rotate(new Vector3(0, 0, 90), "Left"));
                }
            }
            else if(Input.GetAxis("Vertical") > 0){
                if(CheckDirection(Vector3.forward)){
                    moving = true;
                    destination = transform.position + Vector3.forward;
                    StopAllCoroutines();
                    StartCoroutine(Rotate(new Vector3(90, 0, 0), "Forward"));
                }
            }
            else if(Input.GetAxis("Vertical") < 0){
                if(CheckDirection(Vector3.back)){
                    moving = true;
                    destination = transform.position + Vector3.back;
                    StopAllCoroutines();
                    StartCoroutine(Rotate(new Vector3(-90, 0, 0), "Back"));
                }
            }
        }
    }

    IEnumerator Rotate(Vector3 rotationAmount, string direction){
        canMove = false;
        startingRotation = this.transform.rotation;
        Quaternion finalRotation = Quaternion.Euler( rotationAmount.x, rotationAmount.y, rotationAmount.z ) * startingRotation;
        Vector3 finalPosition = destination;

        while(this.transform.rotation != finalRotation){
            float time = Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalRotation, time * rotationSpeed);
            this.transform.position = Vector3.Lerp(this.transform.position, finalPosition, time * speed);
            yield return 0;
        }
        transform.position = new Vector3(Mathf.Round(transform.position.x),transform.position.y, Mathf.Round(transform.position.z));
        transform.rotation = new Quaternion(Mathf.Round(transform.rotation.x), Mathf.Round(transform.rotation.y), Mathf.Round(transform.rotation.z), Mathf.Round(transform.rotation.w));

        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 1))
        {
            FaceInfo face = hit.collider.gameObject.GetComponent<FaceInfo>();
            face.isAttacking = true;
            face.direction = direction;
            face.PlayerHealth = playerhealth;
        }
        moving = false;
    }

    private bool CheckDirection(Vector3 direction){
        if (Physics.Raycast( transform.position, direction, out RaycastHit hit, hitboxSize, 3)) {
            return false;
        }
        else return true;
    }
}
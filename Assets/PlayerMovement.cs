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
    private LoadVariablesScriptableObject GameVariables;
    private PlayerHealth playerhealth;
    public bool canMove = true;
    private FaceInfo _attackFace;
    private IEnumerator rotate;

    void Start(){
        playerhealth = GetComponent<PlayerHealth>();
    }

    void Update(){
        if (!moving /*&& canMove*/)
        {
            if(rotate != null)
                StopCoroutine(rotate);
            if (_attackFace != null)
                StopCoroutine(_attackFace.UseAbility());
            if (Input.GetAxis("Horizontal") > 0){
                if(CheckDirection(Vector3.right)){
                    moving = true;
                    destination = transform.position + Vector3.right;
                    rotate = Rotate(new Vector3(0, 0, -90), "Right");
                    StartCoroutine(rotate);
                }
            }
            else if(Input.GetAxis("Horizontal") < 0){
                if(CheckDirection(Vector3.left)){
                    moving = true;
                    destination = transform.position + Vector3.left; 
                    rotate = Rotate(new Vector3(0, 0, 90), "Right");
                    StartCoroutine(rotate);
                }
            }
            else if(Input.GetAxis("Vertical") > 0){
                if(CheckDirection(Vector3.forward)){
                    moving = true;
                    destination = transform.position + Vector3.forward;
                    rotate = Rotate(new Vector3(90, 0, 0), "Forward");
                    StartCoroutine(rotate);
                }
            }
            else if(Input.GetAxis("Vertical") < 0){
                if(CheckDirection(Vector3.back)){
                    moving = true;
                    destination = transform.position + Vector3.back;
                    rotate = Rotate(new Vector3(-90, 0, 0), "Back");
                    StartCoroutine(rotate);
                }
            }
        }
    }

    IEnumerator Rotate(Vector3 rotationAmount, string direction){
        canMove = false;
        startingRotation = this.transform.rotation;
        Quaternion finalRotation = Quaternion.Euler( rotationAmount.x, rotationAmount.y, rotationAmount.z ) * startingRotation;
        Vector3 finalPosition = destination;

        while(this.transform.rotation != finalRotation)
        {
            float time = Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalRotation, time * rotationSpeed);
            this.transform.position = Vector3.Lerp(this.transform.position, finalPosition, time * speed);
            yield return 0;
        }
        transform.position = new Vector3(Mathf.Round(transform.position.x),transform.position.y, Mathf.Round(transform.position.z));
        transform.rotation = new Quaternion(Mathf.Round(transform.rotation.x), Mathf.Round(transform.rotation.y), Mathf.Round(transform.rotation.z), Mathf.Round(transform.rotation.w));

        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 1))
        {
            _attackFace = hit.collider.gameObject.GetComponent<FaceInfo>();
            _attackFace.direction = direction;
            _attackFace.PlayerHealth = playerhealth;
        }
        moving = false;
        if(_attackFace)
            StartCoroutine(_attackFace.UseAbility());
        if (_attackFace.FinishedAttacking)
            canMove = true;
    }

    private bool CheckDirection(Vector3 direction){
        if (Physics.Raycast( transform.position, direction, out RaycastHit hit, hitboxSize, 3)) {
            return false;
        }
        else return true;
    }
}
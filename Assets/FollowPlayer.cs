using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public enum EnemyBehaviour
    {
        moveAway,
        moveTowards
    }

    public EnemyBehaviour enemyBehaviour;
    public float followDistance = 10;
    public float speed = 10;
    private GameObject player;
    private bool seesPlayer;
    private bool moving = false;
    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = transform.position - player.transform.position;
        //float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (Physics.Raycast(transform.position, -directionToPlayer, out RaycastHit hit, followDistance))
        {
            seesPlayer = hit.collider.gameObject.CompareTag("Player");
        }
        else
        {
            seesPlayer = false;
        }
        if(!moving && seesPlayer){
            moving = true;
            if(Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
            {
                if(enemyBehaviour == EnemyBehaviour.moveAway)
                {
                    destination = transform.position + new Vector3(directionToPlayer.x/Mathf.Abs(directionToPlayer.x), 0, 0);
                }else if (enemyBehaviour == EnemyBehaviour.moveTowards)
                {
                    destination = transform.position + new Vector3(-directionToPlayer.x/Mathf.Abs(directionToPlayer.x), 0, 0);
                }
            }
            else
            {
                if(enemyBehaviour == EnemyBehaviour.moveAway)
                {
                    destination = transform.position + new Vector3(0, directionToPlayer.y/Mathf.Abs(directionToPlayer.y), 0);
                }else if (enemyBehaviour == EnemyBehaviour.moveTowards)
                {
                    destination = transform.position + new Vector3(0, -directionToPlayer.y/Mathf.Abs(directionToPlayer.y), 0);
                }
            }
        }else if(!moving && !seesPlayer)
        {
            switch (Random.Range(0, 3)){
                case 0:
                    destination = new Vector3(1,0,0) + transform.position;
                    break;
                case 1:
                    destination = new Vector3(-1,0,0) + transform.position;
                    break;
                case 2:
                    destination = new Vector3(0,-1,0) + transform.position;
                    break;
                case 3:
                    destination = new Vector3(0,1,0) + transform.position;
                    break;
            }
            moving = true;
        }
        else if (moving)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, destination, Time.deltaTime * speed);
            if(transform.position == destination)
            {
                transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
                moving = false;
            }
        }
    }
}

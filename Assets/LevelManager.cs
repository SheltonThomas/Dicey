using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int EnemiesFinishedMoving = 0;
    public int EnemiesOnLevel = 0;
    private List<FollowPlayer> aiControl = new List<FollowPlayer>();

    public bool isPlayerTurn = true;
    private bool reset = false;

    private void Start()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemiesOnLevel++;
            aiControl.Add(enemy.GetComponent<FollowPlayer>());
        }
    }

    //Setup so that all enemies will move before letting player move again by adding a variable to enemies that tracks if they finished moving and check if they all finished before letting player move
    private void Update()
    {
        if(EnemiesFinishedMoving == EnemiesOnLevel)
        {
            EnemiesFinishedMoving = 0;
            isPlayerTurn = true;
            reset = false;
        }
            

        if (!isPlayerTurn) 
        {
            foreach (FollowPlayer ai in aiControl)
                ai.finishedTurn = false;
        }
            
    }
}

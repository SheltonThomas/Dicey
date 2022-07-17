using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int levelCount;
    [SerializeField]
    private List<int> _enemycount = new List<int>();
    public bool isPlayerTurn = true;
}

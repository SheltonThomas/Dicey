using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
    }
}

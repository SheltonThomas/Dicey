using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPatterns : MonoBehaviour
{
    public static Dictionary<string, Texture2D> attackPatterns = new Dictionary<string, Texture2D>();
    // Start is called before the first frame update
    void Awake()
    {
        Texture2D[] loadedObjects = Resources.LoadAll<Texture2D>("AttackPatterns");

        foreach(Texture2D attack in loadedObjects)
        {
            attackPatterns.Add(attack.name, attack);
        }
    }
}

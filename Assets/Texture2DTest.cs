using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texture2DTest : MonoBehaviour
{
    public Texture2D attackpattern;
    public List<Vector2> AttackPoints = new List<Vector2>();
    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        GameObject blank = new GameObject();
        GameObject attackholder = Instantiate(blank);
        for (int x = 0; x < attackpattern.width; x++)
        {
            for (int y = 0; y < attackpattern.width; y++)
            {
                Color pixelColor = attackpattern.GetPixel(x, y);
                if(pixelColor.a != 0)
                {
                    Vector2 yeet = new Vector2(x, y);
                    AttackPoints.Add(yeet);
                    GameObject newCube = Instantiate(test, attackholder.transform);
                    newCube.transform.position = yeet;
                    newCube.transform.localScale = new Vector3(.1f, .1f, .1f);
                }
            }
        }
        attackholder.transform.position = new Vector3(-4, 0, -4);
        attackholder.transform.eulerAngles = new Vector3(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texture2DTest : MonoBehaviour
{
    public Texture2D attackpattern;
    public GameObject test;
    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject blankObject = new GameObject();
        //blankObject.transform.position = new Vector3(0, 0, 0);
        //GameObject attackholder = Instantiate(blankObject);
        //for (int x = 0; x < attackpattern.width; x++)
        //{
        //    for (int y = 0; y < attackpattern.width; y++)
        //    {
        //        Color pixelColor = attackpattern.GetPixel(x, y);
        //        if(pixelColor.a != 0)
        //        {
        //            Vector2 yeet = new Vector2(x, y);
        //            AttackPoints.Add(yeet);
        //            GameObject newCube = Instantiate(test, attackholder.transform);
        //            newCube.transform.position = yeet;
        //            newCube.transform.localScale = new Vector3(.1f, .1f, .1f);
        //        }
        //    }
        //}
        //attackholder.transform.eulerAngles = new Vector3(90, 0, 0);
        //Vector3 attackPos = gameObject.transform.position - new Vector3(4, 0, 4);
        //attackholder.transform.position = attackPos;

        if (started)
            return;
        GameObject test1 = new GameObject();
        test1.transform.position = new Vector3(0, 0, 0);
        test1.AddComponent<BoxCollider>();
        //Instantiate(test1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] health = new Sprite[6];
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateUIPlayerHealth(int playerHealth)
    {
        this.GetComponent<Image>().sprite = health[playerHealth - 1];
    }
}

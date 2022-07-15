using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplaceCubeFaceBehavior : MonoBehaviour
{
    public Transform Cube;
    private List<MeshRenderer> faces = new List<MeshRenderer>();
    private List<Image> buttons = new List<Image>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform face in Cube)
        {
            faces.Add(face.gameObject.GetComponent<MeshRenderer>());
        }

        foreach(Transform button in gameObject.transform)
        {
            buttons.Add(button.gameObject.GetComponent<Image>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < faces.Count; i++)
        {
            buttons[i].material = faces[i].material;
        }
    }
}

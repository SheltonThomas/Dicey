using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeBehavior : MonoBehaviour
{
    private List<MeshRenderer> _faces = new List<MeshRenderer>();
    public List<Material> CubeMats = new List<Material>();
    public int matindex = 0;
    private float timer = 0;
    [SerializeField]
    List<Material> materials;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in gameObject.transform)
        {
            _faces.Add(child.gameObject.GetComponent<MeshRenderer>());
            CubeMats.Add(child.gameObject.GetComponent<MeshRenderer>().material);
        }
    }

    // Update is called once per frame
    void Update()
    { 
        for(int i = 0; i < _faces.Count; i++)
        {
            _faces[i].material = CubeMats[i];
        }
    }
}

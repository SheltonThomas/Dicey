using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceInfo : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer FaceSprite;
    [SerializeField]
    private float _attackDuration = 0;
    private float _activeTime = 0;
    private List<GameObject> _attackHitboxes = new List<GameObject>();
    public bool isAttacking = false;

    private void Start()
    {
        //Need to rename attack patterns to match icon names
        isAttacking = true;
    }

    private void Update()
    {
        if (isAttacking)
        {
            if(_activeTime >= _attackDuration)
            {
                isAttacking = false;
                DestroyHitBoxes();
                _activeTime = 0;
                return;
            }
            if (_activeTime == 0)
                CreateHitBoxes(AttackPatterns.attackPatterns[FaceSprite.sprite.name]);

            _activeTime += Time.deltaTime;
        }
    }

    private void CreateHitBoxes(Texture2D attack)
    {
        for (int x = 0; x < attack.width; x++)
        {
            for (int y = 0; y < attack.height; y++)
            {
                Color pixelColor = attack.GetPixel(x, y);
                if (pixelColor.a != 0)
                {
                    GameObject attackPoint = new GameObject();
                    attackPoint.AddComponent<BoxCollider>();
                    attackPoint.transform.position = new Vector3(x, 0, y);
                    Vector3 attackPos = attackPoint.transform.position - new Vector3(4, 0, 4);
                    attackPos = attackPos + transform.parent.transform.position;
                    attackPoint.transform.position = attackPos;
                    _attackHitboxes.Add(attackPoint);
                }
            }
        }
        Debug.Log("Created");
    }

    private void DestroyHitBoxes()
    {
        foreach(GameObject attackBox in _attackHitboxes)
        {
            Destroy(attackBox);
        }
        Debug.Log("Destroyed");
    }
}
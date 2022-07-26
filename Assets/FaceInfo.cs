using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceInfo : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer FaceSprite;
    [SerializeField]
    private float _attackDuration = 0;
    private List<GameObject> _attackHitboxes = new List<GameObject>();
    [SerializeField]
    private GameManager _gameManager;
    private LoadVariablesScriptableObject _loadVariables;
    public string direction;
    public PlayerHealth PlayerHealth;
    public bool FinishedAttacking { get; private set; } = true;

    private void Start()
    {
        _loadVariables = _gameManager.GameVariables;
    }

    private void Update()
    {
        
    }

    public IEnumerator UseAbility()
    {
        FinishedAttacking = false; 
        if(FaceSprite.sprite == null)
            yield return 0;

        else if(_loadVariables.GetAbilityType(FaceSprite.sprite.name) == AbilityType.Attack)
        {
            float _activeTime = 0;
            while (_activeTime < _attackDuration)
            {
                if (_activeTime == 0)
                    if (FaceSprite.sprite != null)
                        CreateHitBoxes(_loadVariables.GetAbilityPattern(FaceSprite.sprite.name));

                _activeTime += Time.deltaTime;
                yield return 0;
            }
            DestroyHitBoxes();
        }
        FinishedAttacking = true;
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
                    attackPoint.AddComponent<AttackHitboxBehavior>();
                    attackPoint.AddComponent<BoxCollider>();
                    attackPoint.transform.position = new Vector3(x, 0, y);
                    Vector3 attackPos = attackPoint.transform.position - new Vector3(3, 0, 3);
                    attackPos = attackPos + transform.parent.transform.position;
                    attackPoint.transform.position = attackPos;
                    _attackHitboxes.Add(attackPoint);
                }
            }
        }
    }

    private void DestroyHitBoxes()
    {
        foreach(GameObject attackBox in _attackHitboxes)
        {
            Destroy(attackBox);
            transform.parent.gameObject.transform.GetComponent<PlayerMovement>().canMove = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    Attack,
    Defense,
    Movement
}

[CreateAssetMenu(fileName = "Data", menuName = "LoadVariablesScriptableObject")]
public class LoadVariablesScriptableObject : ScriptableObject
{
    public List<string> FolderNames = new List<string>();

    public Dictionary<string, Texture2D> attackPatterns = new Dictionary<string, Texture2D>();
    public Dictionary<string, AbilityType> abilityTypes = new Dictionary<string, AbilityType>();
    public Dictionary<string, Texture2D> abilityTextures = new Dictionary<string, Texture2D>();

    // Start is called before the first frame update
    public void InitializeVariables()
    {
        Texture2D[] loadedObjects;

        foreach (string folderName in FolderNames)
        {
            loadedObjects = Resources.LoadAll<Texture2D>(folderName);
            if(folderName[0] == 'P')
            {
                foreach (Texture2D attack in loadedObjects)
                {
                    attackPatterns.Add(attack.name, attack);
                }
            }
            else if (folderName[0] == 'G')
            {
                foreach (Texture2D graphic in loadedObjects)
                {
                    abilityTextures.Add(graphic.name, graphic);
                    if (graphic.name[0] == 'A')
                    {
                        abilityTypes.Add(graphic.name, AbilityType.Attack);
                    }
                    if (graphic.name[0] == 'D')
                    {
                        abilityTypes.Add(graphic.name, AbilityType.Defense);
                    }
                    if (graphic.name[0] == 'M')
                    {
                        abilityTypes.Add(graphic.name, AbilityType.Movement);
                    }
                }
            }
        }
    }

    public AbilityType GetAbilityType(string attackName)
    {
        return abilityTypes[attackName];
    }

    public Texture2D GetAbilityTexture(string attackName)
    {
        return abilityTextures[attackName];
    }

    public Texture2D GetAbilityPattern(string attackName)
    {
        return attackPatterns[attackName];
    }


}

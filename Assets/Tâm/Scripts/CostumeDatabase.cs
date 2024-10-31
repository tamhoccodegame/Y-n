using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Costume
{
    public string costumeName;
    public Sprite costumeSprite;
    public string costumerInform;
}

public class CostumeDatabase : MonoBehaviour
{
    public List<Costume> costumeList = new List<Costume>();
    
    public Costume GetCostume(string costumeName)
    {
        Costume costume = costumeList.Find(c => c.costumeName == costumeName);
        if(costume != null) return costume;
        return null;
    }
}

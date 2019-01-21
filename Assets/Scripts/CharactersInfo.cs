using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu]
public class CharactersInfo : ScriptableObject {

	public Character[] characters;

    public void CheckCharacters(List<PlayFab.ClientModels.ItemInstance> itemList)
    {
        int length = itemList.Count;
        for (int i = 0; i < length; i++)
        {
            characters[System.Convert.ToInt32(itemList[i].ItemId)].unlocked = true;
        }
    }
}

[System.Serializable]
public class Character
{
	public string name;
	public Sprite icon;
	public GameObject prefab;
	public string description;
	public bool unlocked;
	public int price;

}

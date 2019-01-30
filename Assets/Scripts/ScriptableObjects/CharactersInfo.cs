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
        for (int i = 1; i < characters.Length; i++)
        {
            characters[i].unlocked = false;
        }
        for (int i = 0; i < length; i++)
        {
            characters[System.Convert.ToInt32(itemList[i].ItemId)].unlocked = true;
        }
    }

    public void GetItemList(List<PlayFab.ClientModels.CatalogItem> items)
    {
        int length = items.Count;
        for (int i = 0; i < length; i++)
        {
            characters[i].name = items[i].DisplayName;
            characters[i].coinPrice = (int)items[i].VirtualCurrencyPrices["CO"];
            characters[i].gemPrice = (int)items[i].VirtualCurrencyPrices["GE"];
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
    //Cosas a hacer
    public bool hide;
	public int coinPrice;
    public int gemPrice;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {


	[SerializeField]
	GameObject shopItemPrefab;
	public CharactersInfo charactersInfo;
	List<ShopItem> instantiatedShopItems = new List<ShopItem>();

	public RectTransform parent;
	public CharacterReferences CR;

	[Header("Variables")]
	public int coins;

	public void Start()
	{
		charactersInfo = CR.charactersInfo;
		InstantiateShopItems();
	}
	void InstantiateShopItems()
	{
		CheckShopItemsToDelete();
		int length = charactersInfo.characters.Length;
		for (int i = 0; i < length; i++)
		{
			ShopItem temp = Instantiate(shopItemPrefab, parent).GetComponent<ShopItem>();
			temp.index = (byte)i;
			temp.SC = this;
			temp.SetVisuals(charactersInfo.characters[i].icon,charactersInfo.characters[i].name);
			instantiatedShopItems.Add(temp);
		}
	}
	void CheckShopItemsToDelete()
	{
		if(instantiatedShopItems.Count != 0)
		{
			int length = instantiatedShopItems.Count;
			for (int i = 0; i < length; i++)
			{
				Destroy(instantiatedShopItems[i].gameObject);
			}
		}
	}
	void SetNewSelected()
	{
		int charSelected = CR.playerInfo.selectedCharacter;
		int length = instantiatedShopItems.Count;
		for (int i = 0; i < length; i++)
		{
			if(charSelected == i)
			{
				instantiatedShopItems[i].selected.SetActive(true);
			}
			else
			{
				instantiatedShopItems[i].selected.SetActive(false);
			}
		}
	}
	public void InstantiateNewCharacter()
	{
		SetNewSelected();
		int charSelected = CR.playerInfo.selectedCharacter;
		Destroy(CR.gameObj);
		GameObject newChar = Instantiate(charactersInfo.characters[charSelected].prefab);
		CR.NewReference(newChar.transform,newChar.GetComponent<TestMovement>(),newChar.GetComponent<PlayerStats>(),newChar.GetComponent<AnimatorController>(),newChar);
	}
	public void GetCoins()
	{
		coins = CR.playerInfo.coins;
	}
	public void UnlockCharacter(int index,ShopItem item)
	{
		GetCoins();
		if (charactersInfo.characters[index].unlocked)
		{
			CR.playerInfo.selectedCharacter = index;
			InstantiateNewCharacter();
		}
		else if(charactersInfo.characters[index].unlocked == false && coins >= charactersInfo.characters[index].price)
		{
			charactersInfo.characters[index].unlocked = true;
			coins -= charactersInfo.characters[index].price;
			item.RefreshPrice();
			CR.playerInfo.selectedCharacter = index;
			InstantiateNewCharacter();
		}
		else if(charactersInfo.characters[index].unlocked == false && coins < charactersInfo.characters[index].price)
		{
			Debug.Log("CantBuy");
		}
	}
}

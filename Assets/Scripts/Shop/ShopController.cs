using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

	#region Singleton
	public static ShopController instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion
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
            if (charactersInfo.characters[i].hide == false)
            {
                ShopItem temp = Instantiate(shopItemPrefab, parent).GetComponent<ShopItem>();
                temp.index = (byte)i;
                temp.SC = this;
                temp.SetVisuals(charactersInfo.characters[i].icon, charactersInfo.characters[i].name);
                instantiatedShopItems.Add(temp);
            }		
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
	public void SetNewSelected()
	{
        string selectedName = CR.charactersInfo.characters[CR.playerInfo.selectedCharacter].name;
        //Debug.Log(selectedName + " DEBUG BUENO");
		int charSelected = CR.playerInfo.selectedCharacter;
		int length = instantiatedShopItems.Count;
		for (int i = 0; i < length; i++)
		{
            int length2 = instantiatedShopItems.Count;
            for (int j = 0; j < length2; j++)
            {
                Debug.Log(instantiatedShopItems[j].itemName);
                if (selectedName == instantiatedShopItems[j].itemName)
                {

                    instantiatedShopItems[j].selected.SetActive(true);
                }
                else
                {
                    instantiatedShopItems[j].selected.SetActive(false);
                }
            }              
		}
	}
	public void InstantiateNewCharacter()
	{
		SetNewSelected();
		int charSelected = CR.playerInfo.selectedCharacter;
        if(CR.ASS != null)
        {
            CR.ASS.DeleteThisFromSpeakers();
        }
		Destroy(CR.gameObj);
		GameObject newChar = Instantiate(charactersInfo.characters[charSelected].prefab);
		CR.NewReference(newChar.transform,newChar.GetComponent<TestMovement>(),newChar.GetComponent<PlayerStats>(),newChar.GetComponent<AnimatorController>(),newChar, newChar.GetComponent<AudioSourceSetter>());
	}
	public void GetCoins()
	{
		coins = CR.playerInfo.coins;
	}
	public void UnlockCharacter(int index,ShopItem item, bool isCoins)
	{
		if (isCoins)
		{
			GetCoins();
			if (charactersInfo.characters[index].unlocked)
			{
				CR.playerInfo.selectedCharacter = index;
				InstantiateNewCharacter();
			}
			else if (charactersInfo.characters[index].unlocked == false && coins >= charactersInfo.characters[index].coinPrice)
			{
				PlayFabLogin.instance.PurchaseItemPlayFab(index, "CO", CharacterReferences.instance.charactersInfo.characters[index].coinPrice); // A MODIFICAR CON GEMS TAMBIEN
				charactersInfo.characters[index].unlocked = true;
				coins -= charactersInfo.characters[index].coinPrice;
				item.RefreshPrice();
				CR.playerInfo.selectedCharacter = index;
				InstantiateNewCharacter();
				EnvironmentController.instance.UpdateEnvironments();
			}
			else if (charactersInfo.characters[index].unlocked == false && coins < charactersInfo.characters[index].coinPrice)
			{
				Debug.Log("CantBuy");
			}
		}
		else
		{
			int gems = CR.playerInfo.gems;
			if (charactersInfo.characters[index].unlocked)
			{
				CR.playerInfo.selectedCharacter = index;
				InstantiateNewCharacter();
			}
			else if (charactersInfo.characters[index].unlocked == false && gems >= charactersInfo.characters[index].gemPrice)
			{
				PlayFabLogin.instance.PurchaseItemPlayFab(index, "GE", CharacterReferences.instance.charactersInfo.characters[index].gemPrice); // A MODIFICAR CON GEMS TAMBIEN
				charactersInfo.characters[index].unlocked = true;
				gems -= charactersInfo.characters[index].gemPrice;
				item.RefreshPrice();
				CR.playerInfo.selectedCharacter = index;
				InstantiateNewCharacter();
				EnvironmentController.instance.UpdateEnvironments();
			}
			else if (charactersInfo.characters[index].unlocked == false && gems < charactersInfo.characters[index].gemPrice)
			{
				Debug.Log("CantBuy");
			}
		}
        MainMenuAnimator.instance.UpdateCoinsText();
	}
}

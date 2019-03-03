using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

	public byte index;
	public GameObject selected;
	public Image icon;
	public Text text;
	public GameObject priceCoinsGO;
    public GameObject priceGemsGO;
    public Text priceCoinsTXT;
    public Text priceGemsTXT;
	public GameObject prices;
    public ShopController SC;
    public string itemName;

	public void SetVisuals(Sprite sprite,string name)
	{
		bool selectedBool;
        itemName = name;
		if(CharacterReferences.instance.playerInfo.selectedCharacter == index)
		{
			selectedBool = true;
		}
		else
		{
			selectedBool = false;
		}
		selected.SetActive(selectedBool);
		icon.sprite = sprite;
		text.text = name;
		if (SC.charactersInfo.characters[index].unlocked == false)
		{
			priceCoinsGO.SetActive(true);
            priceGemsGO.SetActive(true);
            priceCoinsTXT.text = "" + SC.charactersInfo.characters[index].coinPrice;
            priceGemsTXT.text = "" + SC.charactersInfo.characters[index].gemPrice;
        }
		else
		{
			priceCoinsGO.SetActive(false);
            priceGemsGO.SetActive(false);
			prices.SetActive(false);
        }
	}
	public void RefreshPrice()
	{
		if (SC.charactersInfo.characters[index].unlocked == false)
		{
			priceCoinsGO.SetActive(true);
            priceGemsGO.SetActive(true);
            priceCoinsTXT.text = "" + SC.charactersInfo.characters[index].coinPrice;
            priceGemsTXT.text = "" + SC.charactersInfo.characters[index].gemPrice;
        }
		else
		{
			priceCoinsGO.SetActive(false);
            priceGemsGO.SetActive(false);
			prices.SetActive(false);
        }
	}
	public void SelectCharacter(bool coins)
	{
		SC.UnlockCharacter(index,this,coins);
	}
	public void OpenPopUp()
	{
		ShopConfirmer.instance.InstantiateCharacter(index);
		ShopController SC = ShopController.instance;
		if (SC.charactersInfo.characters[index].unlocked)
		{
			SC.CR.playerInfo.selectedCharacter = index;
			SC.InstantiateNewCharacter();
		}
		else
		{
			ShopConfirmer.instance.popUp.SetActive(true);
			ShopConfirmer.instance.SI = this;
		}
		//ShowCharacter
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

	public byte index;
	public GameObject selected;
	public Image icon;
	public Text text;
	public GameObject priceGO;
	public Text priceTXT;
	public ShopController SC;

	public void SetVisuals(Sprite sprite,string name)
	{
		bool selectedBool;
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
			priceGO.SetActive(true);
			priceTXT.text = "" + SC.charactersInfo.characters[index].coinPrice;
		}
		else
		{
			priceGO.SetActive(false);
		}
	}
	public void RefreshPrice()
	{
		if (SC.charactersInfo.characters[index].unlocked == false)
		{
			priceGO.SetActive(true);
			priceTXT.text = "" + SC.charactersInfo.characters[index].coinPrice;
		}
		else
		{
			priceGO.SetActive(false);
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

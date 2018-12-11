using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

	public byte index;
	public GameObject selected;
	public Image icon;
	public Text text;
	public ShopController SC;

	public void SetVisuals(Sprite sprite,string name)
	{
		bool selectedBool;
		if(PlayerPrefs.GetInt("CharacterSelected") == index)
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
	}
	public void SelectCharacter()
	{
		PlayerPrefs.SetInt("CharacterSelected", index);
		SC.InstantiateNewCharacter();
	}
}

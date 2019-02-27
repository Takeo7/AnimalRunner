using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class ShopConfirmer : MonoBehaviour {
	
	#region Singleton
	public static ShopConfirmer instance;
	private void Awake()
	{

		instance = this;
	}
	#endregion

	public GameObject character;
	public GameObject environment;
	public SpineShopAnimator SSA;
	public Transform characterPos;
	public Transform environmentPos;
	public GameObject popUp;
	public ShopItem SI;

	public GameObject cam;
	public GameObject[] characterList;
	public GameObject[] environmentList;

	public bool doit;

    public Text nameText;
    public Text descriptionText;
    public Text coinsText;
    public Text gemsText;

	private void Update()
	{
		if (doit)
		{
			doit = false;
			DoAttack();
		}
	}
	public void PurchaseCharacter(bool coins)
	{
		if (coins)
		{
			//Coin purchase
			SI.SelectCharacter(true);
		}
		else
		{
			//Gem purchase
			SI.SelectCharacter(false);
		}
	}
	public void HideCameraAndChar()
	{
		if(character != null)
		{
			character.SetActive(false);
		}
		environmentPos.gameObject.SetActive(false);
		cam.SetActive(false);

	}
	public void InstantiateCharacter(byte index)
	{
        CharactersInfo chi = CharacterReferences.instance.charactersInfo;
        nameText.text = chi.characters[index].name;
        descriptionText.text = chi.characters[index].description;
        coinsText.text = chi.characters[index].coinPrice.ToString();
        gemsText.text = chi.characters[index].gemPrice.ToString();

        if (character != null)
		{
			Destroy(character);
		}
		if(environment != null)
		{
			Destroy(environment);
		}
		character = Instantiate(characterList[index],characterPos);
		if((index != 0) && (index != 4) && (index != 5))
		{
			environment = Instantiate(environmentList[index], environmentPos);
		}
	}
	public void DoAttack()
	{
		SSA.Attack();
	}
}

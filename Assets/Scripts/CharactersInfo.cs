using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu]
public class CharactersInfo : ScriptableObject {

	public Character[] characters;
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

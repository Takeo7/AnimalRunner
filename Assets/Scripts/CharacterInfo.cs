using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterInfo")]
public class CharacterInfo : ScriptableObject {

	//Base
	[Header("Base")]
	public string playerName;
	public int playerLevel;
	public float currentMedals;
	public float medalsToNextRank;
	[Space]
	//Records
	[Header("Records")]
	public float metersRecord;
	public float pointsRecord;
	[Space]
	//Money
	[Header("Money")]
	public int coins;
	public int gems;
	[Space]
	//Characters
	[Header("Characters")]
	public int selectedCharacter;
	[Space]
	//Lang
	[Header("Languages")]
	public int language;
	[Space]
	//Challenges
	[Header("Challenges")]
	public int[] challengesIndex = new int[3];
	public int[] challengesProgress = new int[3];
}

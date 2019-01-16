using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterInfo")]
public class CharacterInfo : ScriptableObject {

	//Base
	[Header("Base")]
	public string playerName;
	public int playerLevel;
	public byte currentMedals;
	public byte medalsToNextRank;
	public byte medalHolder;//from 0 to 8 first has 3 last has 10
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

    public Dictionary<string,string> GetData()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        //Base
        data.Add("Name", playerName);
        data.Add("Level", playerLevel.ToString());
        data.Add("Medals", currentMedals.ToString());
        data.Add("MedalsLast", medalsToNextRank.ToString());
        data.Add("MedalHolder", medalHolder.ToString());

        //Records
        data.Add("MetersRecord", metersRecord.ToString());
        data.Add("PointsRecord", pointsRecord.ToString());

        //Money
        data.Add("Coins", coins.ToString());
        data.Add("Gems", gems.ToString());

        //Characters
        data.Add("CurrentCharacter", selectedCharacter.ToString());

        return data;
    }

    public void SetData(Dictionary<string,string> data)
    {

    }
}

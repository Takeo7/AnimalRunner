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
        data.Add("Level", playerLevel.ToString());
        data.Add("Medals", currentMedals.ToString());
        data.Add("MedalsLast", medalsToNextRank.ToString());
        data.Add("MedalHolder", medalHolder.ToString());

        //Records
        data.Add("MetersRecord", metersRecord.ToString());
        data.Add("PointsRecord", pointsRecord.ToString());

        //Character
        data.Add("CurrentCharacter", selectedCharacter.ToString());

        return data;
    }

    public void SetData(Dictionary<string,PlayFab.ClientModels.UserDataRecord> data)
    {
        //Base
        playerLevel =  System.Convert.ToInt32(data["Level"].Value);
        currentMedals = (byte)System.Convert.ToInt32(data["Medals"].Value);
        medalsToNextRank = (byte)System.Convert.ToInt32(data["MedalsLast"].Value);
        medalHolder = (byte)System.Convert.ToInt32(data["MedalHolder"].Value);

        //Records
        metersRecord = System.Convert.ToInt32(data["MetersRecord"].Value);
        pointsRecord = System.Convert.ToInt32(data["PointsRecord"].Value);

        //Character
        selectedCharacter = System.Convert.ToInt32(data["CurrentCharacter"].Value);
    }

    public void SetCurrency(int ServerCoins, int ServerGems)
    {
        coins = ServerCoins;
        gems = ServerGems;
    }
}

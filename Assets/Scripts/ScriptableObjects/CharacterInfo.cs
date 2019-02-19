using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterInfo")]
public class CharacterInfo : ScriptableObject {

    //Base
    [Header("Base")]
    public string playerEmail;
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
    //Others
	[Space]
	[Header("Others")]
    public bool loggedWithGoogle = false;
    public bool firstConection = true;
    public bool isLocal = true;
	[Space]
	[Header("Stats")]
	public int totalAttacks;
	public int totalChallengesCompleted;
	public int totalCoinsEarned;
	public int totalDeaths;
	public int totalEnemiesKilled;
	public int totalJumps;
	public int totalMetersRunned;
	public int totalSpecialUsed;

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

		//Stats
		data.Add("TotalAttacks", totalAttacks.ToString());
		data.Add("TotalChallengesCompleted", totalChallengesCompleted.ToString());
		data.Add("TotalCoinsEarned", totalCoinsEarned.ToString());
		data.Add("TotalDeaths", totalDeaths.ToString());
		data.Add("TotalEnemiesKilled", totalEnemiesKilled.ToString());
		data.Add("TotalJumps", totalJumps.ToString());
		data.Add("TotalMetersRunned", totalMetersRunned.ToString());
		data.Add("TotalSpecialUsed", totalSpecialUsed.ToString());

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

		//Stats
		totalAttacks = System.Convert.ToInt32(data["Total attacks"].Value);
		totalChallengesCompleted = System.Convert.ToInt32(data["Total challenges completed"].Value);
		totalCoinsEarned = System.Convert.ToInt32(data["Total coins earned"].Value);
		totalDeaths = System.Convert.ToInt32(data["Total deaths"].Value);
		totalEnemiesKilled = System.Convert.ToInt32(data["Total enemies killed"].Value);
		totalJumps = System.Convert.ToInt32(data["Total jumps"].Value);
		totalMetersRunned = System.Convert.ToInt32(data["Total meters runned"].Value);
		totalSpecialUsed = System.Convert.ToInt32(data["Total special used"].Value);

		LanguajesDic.instance.LoadCurrentLang(language);
        MainMenuAnimator.instance.UpdateTexts();
    }

    public void SetCurrency(int ServerCoins, int ServerGems)
    {
        coins = ServerCoins;
        gems = ServerGems;
        MainMenuAnimator.instance.UpdateCoinsText();
    }

    public void SetLanguage(int i)
    {
        language = i;
    }

    public void ResetLocalData()
    {
        playerName = "";
        playerLevel = 1;
        currentMedals = 0;
        medalsToNextRank = 3;
        medalHolder = 0;

        metersRecord = 0;
        pointsRecord = 0;

        coins = 0;
        gems = 0;

        selectedCharacter = 0;

        language = 0;

        challengesIndex = new int[3];
        challengesProgress = new int[3];

        firstConection = true;
        isLocal = true;
    }
}

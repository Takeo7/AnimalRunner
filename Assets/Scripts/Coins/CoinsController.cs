using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController : MonoBehaviour {

    public static CoinsController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    int coins;
    int gems;
	public CharacterReferences CR;
    private void Start()
    {
		CR = CharacterReferences.instance;
        coins = CR.playerInfo.coins;
        gems = CR.playerInfo.gems;
    }

    public int GetCoins()
    {
        return coins;
    }
    public void SetCoins(int i)
    {
        coins += i;
        CR.playerInfo.coins = coins;
    }

    public int GetGems()
    {
        return gems;
    }
    public void SetGems(int i)
    {
        gems += i;
        CR.playerInfo.gems = gems;
    }
}

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

	public CharacterReferences CR;
	public int coinsOnRun;
    private void Start()
    {
		CR = CharacterReferences.instance;
    }

    public void AddnewCoins()
    {
        SetCoins(coinsOnRun);
    }

    public void SetCoins(int i)
    {
        PlayFabLogin.instance.AddPlayFabVirtualCurrecy(i, "CO");
        CR.playerInfo.coins += i;
    }

    public void SetGems(int i)
    {
        PlayFabLogin.instance.AddPlayFabVirtualCurrecy(i, "GE");
        CR.playerInfo.gems += i;
    }
}

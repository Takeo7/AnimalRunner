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

    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins");
        gems = PlayerPrefs.GetInt("Gems");
    }

    public int GetCoins()
    {
        return coins;
    }
    public void SetCoins(int i)
    {
        coins += i;
        PlayerPrefs.SetInt("Coins", coins);
    }

    public int GetGems()
    {
        return gems;
    }
    public void SetGems(int i)
    {
        gems += i;
        PlayerPrefs.SetInt("Gems", gems);
    }
}

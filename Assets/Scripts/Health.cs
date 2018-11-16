using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public PlayerStats ps;

    public Sprite FullHeart;
    public Sprite EmptyHeart;
    public GameObject HeartPrefab;

    int initialHealth;
    Image[] hearts;

    private void Start()
    {
        initialHealth = ps.Health;
        hearts = new Image[initialHealth];
        CreateHearts(initialHealth);
    }

    void CreateHearts(int h)
    {
        for (int i = 0; i < h; i++)
        {
            GameObject g = Instantiate(HeartPrefab, transform);
            hearts[i] = g.GetComponent<Image>();
        }
    }

    public void UpdateHearts(int h)
    {
        Debug.Log("Health left= " + h);
        for (int i = h; i < initialHealth; i++)
        {
            hearts[i].sprite = EmptyHeart;
            Debug.Log("EmptyHeart");
        }
        if (h > 0)
        {
            for (int i = 0; i < h; i++)
            {
                hearts[i].sprite = FullHeart;
                //Debug.Log("FullHeart");
            }
        }
    }


}

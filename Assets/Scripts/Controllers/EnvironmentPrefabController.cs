using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentPrefabController : MonoBehaviour {

	public SpriteRenderer floorRenderer;
	EnvironmentController EC;
	public BoxCollider2D collider;
    public Transform[] enemySpawnPoints;
    public Transform[] trapSpawnPoints;
    public Transform[] decorationSpawnPoints;
    public GameObject[] enemys;
    public GameObject[] traps;
    public GameObject[] decorations;
    public bool isEnemySpawneable;
    public bool isTrapSpawneable;
    public bool isCave;
    public int numberOfDeco = 2;

    private void Start()
	{
		EC = EnvironmentController.instance;

        if (isEnemySpawneable)
        {
            float isEnemy = Random.Range(0, 3);
            if (isEnemy <= 1)
            {
                CreateNewEnemy();
            }
        }
        if (isTrapSpawneable)
        {
            float isTrap = Random.Range(0, 10);
            if (isTrap <= 1)
            {
                CreateNewTrap();
            }
        }
        try
        {
            if (isCave == false)
            {
                CreateNewDeco(numberOfDeco);
            }
        }
        catch (System.Exception)
        {

            throw;
        } 

    }

    void CreateNewEnemy()
    {
        int rand = Random.Range(0, enemySpawnPoints.Length - 1);
        int randEnemy = Random.Range(0, enemys.Length);
        if (randEnemy == enemys.Length)
        {
            randEnemy = 0;
        }
        GameObject g = Instantiate(enemys[randEnemy], enemySpawnPoints[rand].position, Quaternion.identity);
        g.transform.SetParent(enemySpawnPoints[rand]);
    }
    void CreateNewTrap()
    {
        int rand2 = Random.Range(0, trapSpawnPoints.Length - 1);
        int randTrap = Random.Range(0, traps.Length);
        if (randTrap == traps.Length)
        {
            randTrap = 0;
        }

        GameObject j = Instantiate(traps[randTrap], trapSpawnPoints[rand2].position, Quaternion.identity);
        j.transform.SetParent(trapSpawnPoints[rand2]);
    }
    void CreateNewDeco(int length)
    {
        int temp = 0;
        bool created = false;
        for (int i = 0; i < length; i++)
        {
            int rand3 = Random.Range(0, decorationSpawnPoints.Length);
            if (created && rand3 == temp)
            {
                rand3 = Random.Range(0, decorationSpawnPoints.Length);
            }
            if (rand3 == decorationSpawnPoints.Length)
            {
                rand3 = 0;
            }
            int randDeco = Random.Range(0, decorations.Length);
            if (randDeco == decorations.Length)
            {
                randDeco = 0;
            }
            GameObject deco = Instantiate(decorations[randDeco], decorationSpawnPoints[rand3]);
            created = !created;
            temp = rand3;
        }
        
    }
}

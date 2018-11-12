using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentPrefabController : MonoBehaviour {

	public SpriteRenderer floorRenderer;
	EnvironmentController EC;
	public BoxCollider2D collider;
    public Transform[] SpawnPoints;
    public GameObject enemy;
    public GameObject[] traps;
    public bool isSpawneable;

	private void Start()
	{
		EC = EnvironmentController.instance;
        int rand = Random.Range(0, SpawnPoints.Length - 1);
        int rand2 = Random.Range(0, SpawnPoints.Length - 1);
        if (rand == rand2)
        {
            rand2 = Random.Range(0, SpawnPoints.Length - 1);

        }
        if (isSpawneable)
        {
            int randTrap = Random.Range(0, traps.Length-1);

            float isEnemy = Random.Range(0, 4);
            float isTrap = Random.Range(0, 4);
            if (isEnemy <= 1)
            {
                GameObject g = Instantiate(enemy, SpawnPoints[rand].position, Quaternion.identity);
            }
            if (isTrap <= 1)
            {
                GameObject j = Instantiate(traps[randTrap], SpawnPoints[rand2].position, Quaternion.identity);
            }


            
        }

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentPrefabController : MonoBehaviour {

	public SpriteRenderer floorRenderer;
	EnvironmentController EC;
	public BoxCollider2D collider;
    public Transform[] SpawnPoints;
    public GameObject enemy;
    public bool isSpawneable;

	private void Start()
	{
		EC = EnvironmentController.instance;
        int rand = Random.Range(0, SpawnPoints.Length - 1);
        if (isSpawneable)
        {
            GameObject g = Instantiate(enemy, SpawnPoints[rand].position,Quaternion.identity);
        }

	}
}

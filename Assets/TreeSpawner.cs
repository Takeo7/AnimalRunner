using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour {

	public GameObject[] TreePrefab;
	public Transform spawnPoint;

	private void Start()
	{
		StartCoroutine("SpawnTree");
	}
	IEnumerator SpawnTree()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(3f, 5f));
			GameObject gO = Instantiate(TreePrefab[Random.Range(0,TreePrefab.Length)]);
			gO.transform.position = spawnPoint.position;

		}
	}
}

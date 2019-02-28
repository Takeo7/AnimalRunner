using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingStartRuners : MonoBehaviour {

    public GameObject[] runers;
	void Start () {
        StartCoroutine("StartRuners");

	}
	
	IEnumerator StartRuners()
    {
        int length = runers.Length;
        for (int i = 0; i < length; i++)
        {
            yield return new WaitForSeconds(Random.Range(0, 0.2f));
            runers[i].SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

	[SerializeField]
	GameObject coinsBurst;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {           
            CoinsController.instance.coinsOnRun++;
			CharacterReferences.instance.playerInfo.totalCoinsEarned++;
			Instantiate(coinsBurst, new Vector3(transform.position.x,transform.position.y+0.5f,coinsBurst.transform.position.z), coinsBurst.transform.rotation);
            Destroy(gameObject);
        }
        
    }
}

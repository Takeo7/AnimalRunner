using UnityEngine;
using UnityEngine.UI;

public class ChallengeCompletedUIController : MonoBehaviour {

	public Text text;
	//public Image icon;

	public void Deactivate()
	{
		gameObject.SetActive(false);
	}

}

using UnityEngine;
using UnityEngine.UI;

public class ChallengeCompletedUIController : MonoBehaviour {

	public Text text;
	public Image icon;
	public Sprite[] icons;

	public void Deactivate()
	{
		gameObject.SetActive(false);
	}
	public void SetIcon(byte pos)
	{
		icon.sprite = icons[pos];
	}

}

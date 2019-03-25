using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterX : MonoBehaviour {

    [SerializeField]
    float seconds;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}

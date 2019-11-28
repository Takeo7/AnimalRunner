using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingParallaxScript : MonoBehaviour {

    public RawImage[] elementos;

    public float[] speeds;

    private void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            elementos[i].uvRect = new Rect(elementos[i].uvRect.x + speeds[i] * Time.deltaTime, 0, 1, 1);
        }
    }

}

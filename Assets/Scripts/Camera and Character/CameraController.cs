﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public float dampTime = 0.15f;
	Vector3 velocity = Vector3.zero;
	public float xOffset;
	public float yOffset;
	public Camera cam;
    public bool follow = true;

    private void Start()
    {
        EnvironmentController.instance.gameOverDelegate += EndGameCamera;
    }

    private void Update()
	{
		if (target && follow)
		{
			Vector3 point = cam.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(xOffset, yOffset, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			destination.y = 0;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}

    public void EndGameCamera()
    {
        follow = false;
    }
}

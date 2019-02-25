using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour {

	public SetType set;
	public SpriteRenderer[] sprites;
	public float backgroundSize;
	public float parallaxSpeed;
	public byte index;
	private Transform cameraTransform;
	public Transform[] layers;
	private float viewZone = 10;
	private int leftIndex;
	private int rightIndex;
	private float lastCameraX;
	public float yOffset;
	public float zOffset;
	public bool isParallax;

	//
	public ParallaxType parallaxType;
	public bool inTransition;

	//Desert
	//41.24
	public float desertBackgroundSize;
	public float yOffsetDesert;
	public float zOffsetDesert;
	public float yOffsetIce;
	//LAVA
	[Space]
	public float lavaBackgroundSize;
	public float yOffsetLava;
	public float zOffsetLava;



	void Start()
	{
		cameraTransform = Camera.main.transform;
		lastCameraX = cameraTransform.position.x;
		leftIndex = 0;
		rightIndex = layers.Length - 1;
	}
	void Update()
	{
		if (isParallax)
		{
			float deltaX = cameraTransform.position.x - lastCameraX;
			transform.position += Vector3.right * (deltaX * parallaxSpeed);
			lastCameraX = cameraTransform.position.x;
		}
		if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
		{
			ScrollRight();
		}
	}
	void ScrollRight()
	{
		if(set == SetType.Forest)
		{
			//Debug.Log("scrolledRight");
			int lastLeft = leftIndex;
			layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize) + new Vector3(0, yOffset, zOffset);
			rightIndex = leftIndex;
			leftIndex++;
			if (leftIndex == layers.Length)
			{
				leftIndex = 0;
			}
		}
		else if(set == SetType.Desert)
		{
			//Debug.Log("scrolledRight");
			int lastLeft = leftIndex;
			layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + desertBackgroundSize) + new Vector3(0, yOffsetDesert, zOffsetDesert);
			rightIndex = leftIndex;
			leftIndex++;
			if (leftIndex == layers.Length)
			{
				leftIndex = 0;
			}
		}
		else if(set == SetType.Ice)
		{
			//Debug.Log("scrolledRight");
			int lastLeft = leftIndex;
			layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize) + new Vector3(0, yOffsetIce, zOffsetDesert);
			rightIndex = leftIndex;
			leftIndex++;
			if (leftIndex == layers.Length)
			{
				leftIndex = 0;
			}
		}
		else if(set == SetType.Lava)
		{
			//Debug.Log("scrolledRight");
			int lastLeft = leftIndex;
			layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize) + new Vector3(0, yOffsetLava, zOffsetLava);
			rightIndex = leftIndex;
			leftIndex++;
			if (leftIndex == layers.Length)
			{
				leftIndex = 0;
			}
		}
	}
	public void SetHeight()
	{
		float yOffsetTemp = 0;
		switch (set)
		{
			case SetType.Forest:
				yOffsetTemp = yOffset;
				break;
			case SetType.Desert:
				yOffsetTemp = yOffsetDesert;
				break;
			case SetType.Ice:
				yOffsetTemp = yOffsetIce;
				break;
            case SetType.Lava:
                yOffsetTemp = yOffsetLava;
                break;
            case SetType.Candy:
                yOffsetTemp = yOffsetLava;
                break;

        }
		byte length = (byte)layers.Length;
		for (int i = 0; i < length; i++)
		{
			layers[i].position = new Vector3(layers[i].position.x, yOffsetTemp, layers[i].position.z);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class EnvironmentSet : ScriptableObject {

	public SetType setType;
	public Sprite[] parallaxElements; //0--> Background 1--> Back 2--> Front
	public GameObject[] floorPrefabs;
	public GameObject[] floatingPrefabs;
	public GameObject[] specialPrefabs;

	
}
public enum SetType { Forest,Desert, Ice, Lava, Candy}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChallengesScriptable : ScriptableObject {

	public byte xp;
	public int index;
	new public string name;
	public challengeType type;
	//Meters
	public bool mustBeOnce = false;
	public bool mustBeGrounded = false;
	public int metersToRun;
	//UnitsToKill
	public bool mustBeRanged = false;
	public int unitsToKill;
	//Miscelaneous
	//Andar X Pasos
	//Hacer X Saltos
	//Usar X veces una habilidad en una run
	//Juega X Partidas
	//
}
public enum challengeType { Meters, Killing, Miscelaneous}

using UnityEngine;
using System.Collections;

public class MissonSetter : MonoBehaviour {

	public int curr;
	// Use this for initialization
	void Start () {
	
	}

	// pake int, soalnya udah urut

	void OnMouseDown(){
		GameData.currentMission = curr;
	}
}

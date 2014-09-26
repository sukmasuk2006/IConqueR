using UnityEngine;
using System.Collections;

public class MissonSetter : MonoBehaviour {

	public TextMesh name;

	public int curr;
	// Use this for initialization
	void Start () {
		name.text = GameData.missionList [curr].Name;
	}

	// pake int, soalnya udah urut

	void OnMouseDown(){
		GameData.profile.CurrentMission = curr;
	}
}

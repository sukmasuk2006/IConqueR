using UnityEngine;
using System.Collections;

public class ScreenData : MonoBehaviour {

	public int corridorState;
	public int maxCorridorState;
	public TextMesh maxCorridorStateText;

	void Start(){
		UpdateMaxCorridor ();
	}

	public void UpdateMaxCorridor(){
		maxCorridorStateText.text = "/  " + (maxCorridorState+1).ToString();
	}
}

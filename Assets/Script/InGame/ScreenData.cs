using UnityEngine;
using System.Collections;

public class ScreenData : MonoBehaviour {

	public int corridorState;
	public int maxCorridorState;
	public TextMesh maxCorridorStateText;
	public int shopState = 0;

	void Start(){
		UpdateMaxCorridor ();
	}

	public void UpdateMaxCorridor(){
		maxCorridorStateText.text = "/  " + (maxCorridorState+1).ToString();
	}
}

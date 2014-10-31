using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TutorialObjectInfo : MonoBehaviour {

	public string currState = "Tutorial";
	public List<int> activeState;
	// Use this for initialization
	void Start () {
		if (GameData.profile.TutorialState == activeState [0])
						GameData.gameState = currState;
	}
}

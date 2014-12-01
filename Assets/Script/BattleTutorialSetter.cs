using UnityEngine;
using System.Collections;

public class BattleTutorialSetter : MonoBehaviour {

	private BattleController tutorialObject;
	public int activeAt;
	// Use this for initialization
	void Start () {
		tutorialObject = GameObject.Find("BattleController").GetComponent<BattleController>();
	}
	
	void OnMouseDown(){
	//	Instantiate(
		if ( GameData.profile.TutorialState < 3){
			tutorialObject.DestoryPrefab ();

		}
	}
}

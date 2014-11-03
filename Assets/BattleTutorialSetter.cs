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
		if ( activeAt < GameData.profile.TutorialState ){
			tutorialObject.DestoryPrefab ();

		}
	}
}

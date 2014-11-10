using UnityEngine;
using System.Collections;

public class TutorialSetter : MonoBehaviour {

	private TutorialPrefabList tutorialObject;
	public int activeAt;
	// Use this for initialization
	void Start () {
		tutorialObject = GameObject.Find ("TutorialController").GetComponent<TutorialPrefabList>();
	}
	
	void OnMouseDown(){
	//	Instantiate(
		if ( activeAt == GameData.profile.TutorialState && GameData.profile.TutorialState <= GameConstant.TOTAL_TUTORIAL ){
			tutorialObject.DestoryPrefab ();

		}
	}
}

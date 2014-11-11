using UnityEngine;
using System.Collections.Generic;

public class TutorialPrefabList : MonoBehaviour {

	public List<Transform> listPrefab;
	// Use this for initialization
	void Start () {
		if ( GameData.profile.TutorialState < GameConstant.TOTAL_TUTORIAL )
			SetPrefab ();
	}

	public void SetPrefab(){
		Debug.Log("set tutorial state " + GameData.profile.TutorialState);
		if ( GameData.profile.TutorialState < GameConstant.TOTAL_TUTORIAL ){
			Transform t = listPrefab [GameData.profile.TutorialState-1];
			Instantiate (t, new Vector3(t.position.x,t.position.y,t.position.z), Quaternion.identity);
		}
		GameData.profile.TutorialState++;
	}

	public void DestoryPrefab(){
		GameObject temp = GameObject.FindGameObjectWithTag ("Tutorial");
		Debug.Log ("at tutor destroy " + temp);
		Destroy (temp);
		SetPrefab();//tutorialObject = null;
		if ( GameData.profile.TutorialState > GameConstant.TOTAL_TUTORIAL ){
			this.gameObject.SetActive(false);
			ProfileController p = GameObject.Find("Profile").GetComponent<ProfileController>();
			p.UpdateGoldAndDiamond(0,-5000);
			p.UpdateGoldAndDiamond(1,-10);
			GameData.gameState = "Home";
		}
	}
}

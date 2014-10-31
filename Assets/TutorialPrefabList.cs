using UnityEngine;
using System.Collections.Generic;

public class TutorialPrefabList : MonoBehaviour {

	public List<Transform> listPrefab;
	public List<bool> hiddenOrShow;
	private GameObject tutorialObject;
	// Use this for initialization
	void Start () {
		if ( GameData.profile.TutorialState < GameConstant.TOTAL_TUTORIAL )
			SetPrefab ();
	}

	public void SetPrefab(){
		Debug.Log("set tutorial state " + GameData.profile.TutorialState);
		Transform t = listPrefab [GameData.profile.TutorialState];
		Instantiate (t, new Vector3(t.position.x,t.position.y,t.position.z), Quaternion.identity);

		//if (!hiddenOrShow [GameData.profile.TutorialState]) {
//					this.gameObject.transform.position = new Vector3 (0, -12f, 0);
//					Debug.Log ("at tutorial prefab hidden " + tutorialObject.name);
	//	} 	
		GameData.profile.TutorialState++;
	}

	public void DestoryPrefab(){
		GameObject temp = GameObject.FindGameObjectWithTag ("Tutorial");
		Debug.Log ("at tutor destroy " + temp);
		Destroy (temp);
			SetPrefab();//tutorialObject = null;
		if ( GameData.profile.TutorialState > GameConstant.TOTAL_TUTORIAL ){
			this.gameObject.SetActive(false);
		}
	}
}

using UnityEngine;
using System.Collections;

public class HeroProfileSetter : MonoBehaviour {

	public GameObject tweenedObject;
	public GameObject targetObject;
	public HeroProfileController controller;
	public int id;
	public string name;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (GameData.unitList [id].IsUnlocked) {
						GameData.selectedToViewProfileId = id;
						GameData.selectedToViewProfileName = name;
						GameData.gameState = "HeroProfileScene";
						iTween.MoveTo ( tweenedObject,iTween.Hash("position",targetObject.transform.position,"time", 0.5f,"oncomplete","MoveTarget","oncompletetarget",gameObject));
						iTween.MoveTo (targetObject, tweenedObject.transform.position,0.5f);		
						controller.SetPictureAndStats();
		}
	}
}

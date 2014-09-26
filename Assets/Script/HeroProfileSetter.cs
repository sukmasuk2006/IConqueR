using UnityEngine;
using System.Collections;

public class HeroProfileSetter : MonoBehaviour {

	public GameObject tweenedObject;
	public GameObject targetObject;
	public HeroProfileController controller;
	public int id;
	public string name;
	private Vector3 tempPosition;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		tempPosition = targetObject.transform.position;
		if (GameData.unitList [id].IsUnlocked && GameData.readyToTween) {
						// view profile controller set gambar dan status		
						GameData.selectedToViewProfileId = id;
						GameData.selectedToViewProfileName = name;
						GameData.gameState = "HeroProfileScene";
						GameData.readyToTween = false;
						iTween.MoveTo ( targetObject,iTween.Hash("position",tweenedObject.transform.position,"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
						controller.SetPictureAndStats();
					
		}
	}
	void ReadyTween(){
		GameData.readyToTween = true;
		iTween.MoveTo (tweenedObject, tempPosition,0.3f);		
		Debug.Log ("Oncomplete");
	}
}

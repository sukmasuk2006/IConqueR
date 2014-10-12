using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SetHeroOnFormation : MonoBehaviour {
	
	public List<FormationSetter> listForm;
	public TextMesh infoText;
	public GameObject tweenedObject;
	public GameObject targetObject;
	private Vector3 tempPosition;
	public AudioClip sound;

	// Use this for initialization
	void Start () {
		infoText.text = "Select your hero!";
		}
	

	void OnMouseDown(){
		tempPosition = targetObject.transform.position;
		// SET FORMATION		
				//copy status, dan id biar gampang nanti itung2an expnya setelah battle
//				Debug.Log("slot formasi yang akan di set di barackscreen " + screenData.formationSlot);
		if (GameData.profile.activeHeroes == 0) {
			infoText.text = "Please select at least one hero";
		} else {// slot di formation
				int formationSlot = 0;
			for (int i = 0; i < GameData.profile.unitList.Count; i++) {
							if (i < 5) {
							// BUAT JADI KOSONG DULU
							GameData.profile.formationList [i].SetUnit (99, GameData.profile.unitList [i]);
									listForm [i].ReloadSprite (null);
							}

							// SET HERO JIKA ADA
							if (GameData.profile.unitList [i].IsActive && GameData.profile.formationList [formationSlot].IsUnlocked) {
								GameData.profile.formationList [formationSlot].
								SetUnit (GameData.profile.unitList [i].HeroId,GameData.profile.unitList [i]);
								// load sprite
								listForm [formationSlot].ReloadSprite (GameData.unitSpriteList[GameData.profile.unitList [i].HeroId]);
												formationSlot++;
										}
								}
							infoText.text = "Select your hero!";
							if (GameData.readyToTween ) {
								GameData.readyToTween = false;
								iTween.MoveTo ( targetObject,iTween.Hash("position",tweenedObject.transform.position,"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
							}
			}
		GameData.SaveData ();
	}

	void ReadyTween(){
		GameData.readyToTween = true;
		iTween.MoveTo (tweenedObject, tempPosition,0.3f);		
		Debug.Log ("Oncomplete");	
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);

	}
}

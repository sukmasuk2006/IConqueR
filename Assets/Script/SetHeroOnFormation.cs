using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SetHeroOnFormation : MonoBehaviour {
	
	public List<FormationSetter> listForm;
	public TextMesh infoText;
	public GameObject tweenedObject;
	public GameObject targetObject;
	private Vector3 tempPosition;

	// Use this for initialization
	void Start () {
		infoText.text = "Select your hero!";
		}
	

	void OnMouseDown(){
		tempPosition = targetObject.transform.position;
		// SET FORMATION		
				//copy status, dan id biar gampang nanti itung2an expnya setelah battle
//				Debug.Log("slot formasi yang akan di set di barackscreen " + screenData.formationSlot);
		if (GameData.activeHeroes == 0) {
			infoText.text = "Please select at least one hero";
		} else {
					int slot = 0;
					for (int i = 0; i < GameData.unitList.Count; i++) {
							if (i < 5) {

					// BUAT JADI KOSONG DULU
									GameData.formationList [i].SetUnit (99, GameData.unitList [i]);
									listForm [i].ReloadSprite (null);
							}

				// SET HERO JIKA ADA
							if (GameData.unitList [i].IsActive && GameData.formationList [slot].IsUnlocked) {
									Debug.Log ("slot " + slot);
									GameData.formationList [slot].SetUnit (slot, GameData.unitList [i]);
									listForm [slot].ReloadSprite (GameData.formationList [slot].Unit.Sprites);
									slot++;
							}
					}
			infoText.text = "Select your hero!";
				if (GameData.readyToTween ) {
					GameData.readyToTween = false;
					iTween.MoveTo ( targetObject,iTween.Hash("position",tweenedObject.transform.position,"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
				}
			}
	}

	void ReadyTween(){
		GameData.readyToTween = true;
		iTween.MoveTo (tweenedObject, tempPosition,0.3f);		
		Debug.Log ("Oncomplete");	
	}
}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SetHeroOnFormation : MonoBehaviour {
	
	public GameObject tweenedObject;
	public GameObject targetObject;
	public int id;
	public ScreenData screenData;
	public List<FormationSetter> listForm;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown(){
		if (GameData.unitList [id].IsUnlocked && GameData.readyToTween) {
			if ( GameData.gameState == "SetFormation" ){
				// SET FORMATION		
				//copy status, dan id biar gampang nanti itung2an expnya setelah battle
//				Debug.Log("slot formasi yang akan di set di barackscreen " + screenData.formationSlot);
				GameData.formationList[screenData.formationSlot].SetUnit(id,GameData.unitList[id]);
				listForm[screenData.formationSlot].ReloadSprite(GameData.formationList[screenData.formationSlot].Unit.Sprites);
				iTween.MoveTo ( tweenedObject,iTween.Hash("position",targetObject.transform.position,"time", 0.5f,"oncomplete","MoveTarget","oncompletetarget",gameObject));
				iTween.MoveTo (targetObject, tweenedObject.transform.position,0.5f);					
			}

			
		}
	}

	void ReadyTween(){
		GameData.readyToTween = true;
	}
}

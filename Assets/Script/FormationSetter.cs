using UnityEngine;
using System.Collections;

public class FormationSetter : MonoBehaviour {

	public int slot;
	public TextMesh info;
	public SpriteRenderer spriteRend;
	public GameObject tweenedObject;
	public GameObject targetObject;
	public ScreenData screenData;
	public GameObject heroLock;
	private bool isReload = false;
	public ProfileController profileController;
	// Use this for initialization
	void Start () {
		if (GameData.formationList [slot].IsUnlocked) {
			heroLock.SetActive (false);
			ReloadSprite(GameData.formationList[slot].Unit.Name.Trim());
		}
		Debug.Log ("slot isunlock " + slot + " " + GameData.formationList [slot].IsUnlocked);
	}

	void OnMouseDown(){
		//HOTween.To(tweenedObject,0.5f,"position",targetObject.transform.position);
		GameData.gameState = "SetFormation";		
		if (GameData.readyToTween ) {
			if (!GameData.formationList [slot].IsUnlocked && GameData.gold >= 5000){
				heroLock.SetActive(false);
				GameData.formationList [slot].IsUnlocked = true;
				GameData.gold -= 5000;
				// awal buka kasih knight
				GameData.formationList[slot].SetUnit(slot,GameData.unitList[0]);
				ReloadSprite(GameData.unitList[0].Name);
				profileController.UpdateGoldAndDiamond();
			}
			if (GameData.formationList [slot].IsUnlocked ){
				screenData.formationSlot = slot;
				GameData.readyToTween = false;
				iTween.MoveTo ( tweenedObject,iTween.Hash("position",targetObject.transform.position,"time", 0.5f,"oncomplete","ReadyTween","oncompletetarget",gameObject));
				iTween.MoveTo (targetObject, tweenedObject.transform.position,0.5f);
			}
		}
	//	Debug.Log ("slot formasi yang akan di set " + screenData.formationSlot +" " +  GameData.gameState);
	}


	void ReadyTween(){
		GameData.readyToTween = true;
	}

	public void ReloadSprite(string name){
		Sprite sprite = (Sprite)Resources.Load ("Sprite/Character/Hero/" + name, typeof(Sprite));
		spriteRend.sprite = sprite;
	}
}

using UnityEngine;
using System.Collections;

public class FormationSetter : MonoBehaviour {

	public int slot;
	public TextMesh info;
	public SpriteRenderer spriteRend;
	public GameObject tweenedObject;
	public GameObject barrackScreen;
	public GameObject targetObject;
	public ScreenData screenData;
	public GameObject heroLock;
	private bool isReload = false;
	public ProfileController profileController; // profile master
	public HeroProfileController controller; // screen troops
	// Use this for initialization
	void Start () {
		if (GameData.formationList [slot].IsUnlocked) {
			heroLock.SetActive (false);
			if ( GameData.formationList[slot].Unit.HeroId == 99 )
				ReloadSprite(null);
				else
			ReloadSprite(GameData.formationList[slot].Unit.Sprites);
		}
		Debug.Log ("slot isunlock " + slot + " " + GameData.formationList [slot].IsUnlocked);
	}

	void OnMouseDown(){
		//HOTween.To(tweenedObject,0.5f,"position",targetObject.transform.position);
		GameData.gameState = "SetFormation";		
		if (GameData.readyToTween ) {
			// kalau belum ke unlock

			// ketika id !=99, brarti sudah diset, bisa liat profilnya
			if (GameData.formationList [slot].IsUnlocked && GameData.formationList [slot].Unit.HeroId != 99){
				// view profile controller set gambar dan status		
				GameData.selectedToViewProfileId = GameData.formationList[slot].Unit.HeroId;
				GameData.selectedToViewProfileName = GameData.formationList[slot].Unit.Name;
				GameData.gameState = "HeroProfileScene";
				iTween.MoveTo ( tweenedObject,iTween.Hash("position",targetObject.transform.position,"time", 0.1f,"oncomplete","MoveTarget","oncompletetarget",gameObject));
				iTween.MoveTo (targetObject, tweenedObject.transform.position,0.1f);					
				controller.SetPictureAndStatsFromFormation();
			}
			// set hero

		}
	//	Debug.Log ("slot formasi yang akan di set " + screenData.formationSlot +" " +  GameData.gameState);
	}
	

	public void ReloadSprite(Sprite s){
		spriteRend.sprite = s;
	}
}

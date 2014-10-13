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
	public AudioClip sound;
	// Use this for initialization
	void Start () {

		if (GameData.profile.formationList [slot].IsUnlocked) {
			heroLock.SetActive (false);
			if ( GameData.profile.formationList[slot].Unit.HeroId == 99 )
				ReloadSprite(null);
				else
				ReloadSprite(GameData.unitSpriteList[GameData.profile.formationList[slot].Unit.HeroId]);
		}
//		Debug.Log ("slot isunlock " + slot + " " + GameData.profile.formationList [slot].IsUnlocked);
	}

	void OnMouseDown(){
		//HOTween.To(tweenedObject,0.5f,"position",targetObject.transform.position);
		GameData.gameState = "SetFormation";		
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);
		if (GameData.readyToTween ) {
			// kalau belum ke unlock
//			Debug.Log("masuk 1 " + GameData.profile.formationList [slot].IsUnlocked + " " + GameData.profile.formationList [slot].Unit.HeroId);
			// ketika id !=99, brarti sudah diset, bisa liat profilnya
			if (GameData.profile.formationList [slot].IsUnlocked && GameData.profile.formationList [slot].Unit.HeroId != 99){
//				Debug.Log("masuk 2 " + GameData.profile.formationList [slot].IsUnlocked + " " + GameData.profile.formationList [slot].Unit.HeroId);
				// view profile controller set gambar dan status		
				GameData.selectedToViewProfileId = GameData.profile.formationList[slot].Unit.HeroId;
				GameData.selectedToViewProfileName = GameData.profile.formationList[slot].Unit.Name;
				GameData.selectedToViewProfileIdFromFormation = slot;
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

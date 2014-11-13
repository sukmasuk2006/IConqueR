using UnityEngine;
using System.Collections;

public class FormationSetter : MonoBehaviour {

	public int slot;
	public TextMesh info;
	public SpriteRenderer spriteRend;
	public GameObject barrackScreen;
	public GameObject targetObject;
	public ScreenData screenData;
	public GameObject heroLock;
	private bool isReload = false;
	public ProfileController profileController; // profile master
	public HeroProfileController controller; // screen troops
	public AudioClip sound;
	private Vector3 tempPosition;
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

	}

	void OnMouseUp(){
		tempPosition = targetObject.transform.position;
		info.text = "Select unit";
		if (GameData.readyToTween ) {
			MusicManager.getMusicEmitter().audio.PlayOneShot(sound);
			// kalau belum ke unlock
			//			Debug.Log("masuk 1 " + GameData.profile.formationList [slot].IsUnlocked + " " + GameData.profile.formationList [slot].Unit.HeroId);
			// ketika id !=99, brarti sudah diset, bisa liat profilnya
			if (GameData.profile.formationList [slot].IsUnlocked ){
				//				Debug.Log("masuk 2 " + GameData.profile.formationList [slot].IsUnlocked + " " + GameData.profile.formationList [slot].Unit.HeroId);
				// view profile controller set gambar dan status		
				GameData.unitSlotYangDiSet = slot;
				GameData.gameState = "SelectUnit";
				iTween.MoveTo ( targetObject,iTween.Hash("position",barrackScreen.transform.position,"time", 0.1f,"oncomplete","ReadyTween","oncompletetarget",gameObject));
			}
		}
	}
	
	void ReadyTween(){
		iTween.MoveTo ( barrackScreen,iTween.Hash("position",tempPosition,"time", 0.1f,"oncomplete","ReadyTween2","oncompletetarget",gameObject));
	}

	void ReadyTween2(){
		GameData.readyToTween = true;
	}

	public void ReloadSprite(Sprite s){
		spriteRend.sprite = s;
	}
}

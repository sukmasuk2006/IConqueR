using UnityEngine;
using System.Collections;

public class UnlockHero : MonoBehaviour {

	public int slot;
	public GameObject frame;
	public Sprite sprite;
	public SpriteRenderer renderer;
	public ProfileController profileController;
	public Sprite spriteKuning;
	public Sprite spriteBiru;
	public TextMesh teks;
	public AudioClip sound;
	public TextMesh warningText;
	public int profileLevelRequired = 1;
	public GameObject confirmationScreen;
	public ConfirmUnlockHero confirm;

	void Start(){
		//Debug.Log ("awal2 slot " + slot + " isunlock " + GameData.skillList [slot].IsUnlocked + " selec " + GameData.skillList [slot].IsSelected);
		// aktif dan terunlock
		/*if (GameData.profile.unitList [slot].IsActive && GameData.profile.unitList [slot].IsUnlocked) {
			renderer.sprite = spriteKuning;
			teks.text = "Deselect";
			//tidak aktif tapi terunlock
		} else if (!GameData.profile.unitList [slot].IsActive && GameData.profile.unitList [slot].IsUnlocked) {
			renderer.sprite = spriteBiru;
			teks.text = "Select";		
		} 
		// locked
		else if (!GameData.profile.unitList [slot].IsUnlocked) {
			renderer.sprite = spriteBiru;
			teks.text = "Unlock";		
		} */
		if ( GameData.profile.unitList[slot].IsUnlocked ) teks.text = "Enhance";
	}

	void OnMouseDown(){
		// UNLOCK HERO
		if ( GameData.profile.Level < profileLevelRequired && !GameData.profile.unitList [slot].IsUnlocked ) {
			warningText.text = "Profile must at least level " + profileLevelRequired + ", fight more!";
		}
		else if (GameData.profile.Gold >= GameData.profile.unitList [slot].GoldNeeded 
		         && !GameData.profile.unitList [slot].IsUnlocked) {
			GameData.readyToTween = false;
			confirm.text1.text = "Unlock " + GameData.profile.unitList[slot].Name + " ? ";
			confirm.text2.text = " " + GameData.profile.unitList[slot].GoldNeeded;
			confirm.Slot = slot;
			MusicManager.getMusicEmitter ().audio.PlayOneShot (sound);
			GameData.prevGameState = GameData.gameState;
			GameData.gameState = "UnlockHero";
			iTween.MoveTo (confirmationScreen, iTween.Hash ("position", new Vector3(0,0,confirmationScreen.transform.position.z), "time", 0.1f, "oncomplete", "ReadyTween", "oncompletetarget", gameObject));
		}
		else if (GameData.profile.Gold >= GameData.profile.unitList [slot].GoldNeeded 
		          && GameData.profile.unitList [slot].IsUnlocked) {
			GameData.readyToTween = false;
			confirm.text1.text = "Upgrade " + GameData.profile.unitList[slot].Name + " Job ? ";
			confirm.text2.text = " " + GameData.profile.unitList[slot].GoldNeeded;
			confirm.Slot = slot;
			MusicManager.getMusicEmitter ().audio.PlayOneShot (sound);
			GameData.prevGameState = GameData.gameState;
			GameData.gameState = "UpgradeJob";
			iTween.MoveTo (confirmationScreen, iTween.Hash ("position", new Vector3(0,0,confirmationScreen.transform.position.z), "time", 0.1f, "oncomplete", "ReadyTween", "oncompletetarget", gameObject));
		}
		else if ( GameData.profile.Gold < GameData.profile.unitList [slot].GoldNeeded 
		         && !GameData.profile.unitList [slot].IsUnlocked ) {
			warningText.text = "Not enough Gold to Unlock, fight more!";
		} 
	}
	
	void ReadyTween(){
		GameData.readyToTween = true;
		///GameData.gameState = "ConfirmUnlockHero";
	}
	void OnMouseUp(){
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);
	
	}
}

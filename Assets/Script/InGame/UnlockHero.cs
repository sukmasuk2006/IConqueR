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
	private Unit u;

	void Start(){
		//Debug.Log ("awal2 slot " + slot + " isunlock " + GameData.skillList [slot].IsUnlocked + " selec " + GameData.skillList [slot].IsSelected);
		// aktif dan terunlock
		/*if (u.IsActive && u.IsUnlocked) {
			renderer.sprite = spriteKuning;
			teks.text = "Deselect";
			//tidak aktif tapi terunlock
		} else if (!u.IsActive && u.IsUnlocked) {
			renderer.sprite = spriteBiru;
			teks.text = "Select";		
		} 
		// locked
		else if (!u.IsUnlocked) {
			renderer.sprite = spriteBiru;
			teks.text = "Unlock";		
		} */
		u = GameData.profile.unitList[slot];
		if ( u.IsUnlocked ) teks.text = "Enhance";
		if ( u.CurrentJob > 0 ) this.gameObject.SetActive(false);
	}

	void OnMouseDown(){
		// UNLOCK HERO
		if ( GameData.profile.Level < profileLevelRequired && !u.IsUnlocked ) {
			warningText.text = "Profile must at least level " + profileLevelRequired + ", fight more!";
		}
		else if (GameData.profile.Gold >= u.GoldNeeded 
		         && !u.IsUnlocked) {
			GameData.readyToTween = false;
			confirm.text1.text = "Unlock " + u.Name + " ? ";
			confirm.text2.text = " " + u.GoldNeeded;
			confirm.Slot = slot;
			MusicManager.getMusicEmitter ().audio.PlayOneShot (sound);
			GameData.prevGameState = GameData.gameState;
			GameData.gameState = "UnlockHero";
			iTween.MoveTo (confirmationScreen, iTween.Hash ("position", new Vector3(0,0,confirmationScreen.transform.position.z), "time", 0.1f, "oncomplete", "ReadyTween", "oncompletetarget", gameObject));
		}
		else if (GameData.profile.Gold >= u.GoldNeeded 
		          && u.IsUnlocked && u.Level >= 10 ) {
			GameData.readyToTween = false;
			confirm.text1.text = "Upgrade " + u.Name + " Job ? ";
			confirm.text2.text = " " + u.GoldNeeded;
			confirm.Slot = slot;
			MusicManager.getMusicEmitter ().audio.PlayOneShot (sound);
			GameData.prevGameState = GameData.gameState;
			GameData.gameState = "UpgradeJob";
			iTween.MoveTo (confirmationScreen, iTween.Hash ("position", new Vector3(0,0,confirmationScreen.transform.position.z), "time", 0.1f, "oncomplete", "ReadyTween", "oncompletetarget", gameObject));
		}
		else if ( GameData.profile.Gold < u.GoldNeeded 
		         && !u.IsUnlocked ) {
			warningText.text = "Not enough Gold to Unlock, fight more!";
		} 
		else if ( u.Level < 10 ) warningText.text = "unit level at least level 10";

	}
	
	void ReadyTween(){
		GameData.readyToTween = true;
		///GameData.gameState = "ConfirmUnlockHero";
	}
	void OnMouseUp(){
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);
	
	}
}

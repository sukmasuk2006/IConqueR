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

	void Start(){
		//Debug.Log ("awal2 slot " + slot + " isunlock " + GameData.skillList [slot].IsUnlocked + " selec " + GameData.skillList [slot].IsSelected);
		// aktif dan terunlock
		if (GameData.profile.unitList [slot].IsActive && GameData.profile.unitList [slot].IsUnlocked) {
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
		} 
	}

	void OnMouseDown(){
		// UNLOCK HERO
		if (GameData.profile.Gold >= GameData.profile.unitList [slot].GoldNeeded && !GameData.profile.unitList [slot].IsUnlocked) {
						GameData.profile.unitList [slot].IsUnlocked = true;
						frame.SetActive (false);
						profileController.UpdateGoldAndDiamond(0,GameData.profile.unitList [slot].GoldNeeded);	
						teks.text = "Select";
						renderer.sprite = spriteBiru;
		} else if ( GameData.profile.Gold < GameData.profile.unitList [slot].GoldNeeded 
		           && !GameData.profile.unitList [slot].IsUnlocked ) {
			warningText.text = "Not enough Gold to Unlock, fight more!";
		}
		// gak dipake heronya
		if (GameData.profile.unitList [slot].IsActive && GameData.profile.unitList [slot].IsUnlocked  && GameData.profile.activeHeroes > 0) {
			GameData.profile.activeHeroes--;
			teks.text = "Select";		
			renderer.sprite = spriteBiru;
			GameData.profile.unitList[slot].IsActive = false;
		}
		// make heronya
		else if (!GameData.profile.unitList [slot].IsActive && GameData.profile.unitList [slot].IsUnlocked ){  
	        if  (GameData.profile.activeHeroes < GameData.profile.unlockedSlot) {
				GameData.profile.activeHeroes++;
				teks.text = "Deselect";		
				renderer.sprite = spriteKuning;
				GameData.profile.unitList[slot].IsActive = true;
			}
			else
				warningText.text = "Please deselect another hero first";

		}
		Debug.Log ("aktif hero " + GameData.profile.activeHeroes);

	}

	void OnMouseUp(){
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);
	
	}
}

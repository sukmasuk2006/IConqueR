using UnityEngine;
using System.Collections;

public class UnlockHero : MonoBehaviour {

	public int slot;
	public GameObject frame;
	public Sprite sprite;
	public SpriteRenderer renderer;
	public ProfileController profileController;
	public Sprite unlockedSprite;
	public Sprite spriteBiru;
	public TextMesh teks;
	public AudioClip sound;

	void Start(){
		//Debug.Log ("awal2 slot " + slot + " isunlock " + GameData.skillList [slot].IsUnlocked + " selec " + GameData.skillList [slot].IsSelected);
		// aktif dan terunlock
		if (GameData.profile.unitList [slot].IsActive && GameData.profile.unitList [slot].IsUnlocked) {
			renderer.sprite = unlockedSprite;
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
						renderer.sprite = unlockedSprite;
				} else {
			Debug.Log("gak ada uang");
		}
		// gak dipake heronya
		if (GameData.profile.unitList [slot].IsActive && GameData.profile.unitList [slot].IsUnlocked  && GameData.profile.activeHeroes > 0) {
			GameData.profile.activeHeroes--;
			teks.text = "Select";		
			renderer.sprite = spriteBiru;
			GameData.profile.unitList[slot].IsActive = false;
		}
		// make heronya
		else if (!GameData.profile.unitList [slot].IsActive && GameData.profile.unitList [slot].IsUnlocked  &&  
		         GameData.profile.activeHeroes < GameData.profile.unlockedSlot) {
			GameData.profile.activeHeroes++;
			teks.text = "Deselect";		
			renderer.sprite = unlockedSprite;
			GameData.profile.unitList[slot].IsActive = true;
		}
		Debug.Log ("aktif hero " + GameData.profile.activeHeroes);

	}

	void OnMouseUp(){
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);
	
	}
}

using UnityEngine;
using System.Collections;

public class UnlockHero : MonoBehaviour {

	public int slot;
	public GameObject frame;
	public Sprite sprite;
	public SpriteRenderer renderer;
	public ProfileController profileController;
	public Sprite selectedSprite;
	public Sprite deselectedSprite;
	public AudioClip sound;

	void Start(){
		//Debug.Log ("awal2 slot " + slot + " isunlock " + GameData.skillList [slot].IsUnlocked + " selec " + GameData.skillList [slot].IsSelected);
		if (GameData.profile.unitList [slot].IsActive && GameData.profile.unitList [slot].IsUnlocked) {
			renderer.sprite = selectedSprite;
		} else if (!GameData.profile.unitList [slot].IsActive && GameData.profile.unitList [slot].IsUnlocked) {
			renderer.sprite = deselectedSprite;		
		} 
	}

	void OnMouseDown(){
		// UNLOCK HERO
		if (GameData.profile.Gold >= GameData.profile.unitList [slot].GoldNeeded && !GameData.profile.unitList [slot].IsUnlocked) {
						GameData.profile.unitList [slot].IsUnlocked = true;
						frame.SetActive (false);
						profileController.UpdateGoldAndDiamond(0,GameData.profile.unitList [slot].GoldNeeded);	
						renderer.sprite = deselectedSprite;

				} else {
			Debug.Log("gak ada uang");
		}
		// gak dipake heronya
		if (GameData.profile.unitList [slot].IsActive && GameData.profile.unitList [slot].IsUnlocked  && GameData.profile.activeHeroes > 0) {
			GameData.profile.activeHeroes--;
			renderer.sprite = deselectedSprite;
			GameData.profile.unitList[slot].IsActive = false;
		}
		// make heronya
		else if (!GameData.profile.unitList [slot].IsActive && GameData.profile.unitList [slot].IsUnlocked  &&  
		         GameData.profile.activeHeroes < GameData.profile.unlockedSlot) {
			GameData.profile.activeHeroes++;
			renderer.sprite = selectedSprite;
			GameData.profile.unitList[slot].IsActive = true;
		}
		Debug.Log ("aktif hero " + GameData.profile.activeHeroes);

	}

	void OnMouseUp(){
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);
	
	}
}

using UnityEngine;
using System.Collections.Generic;

public class UnlockFormationSlot : MonoBehaviour {

	public ProfileController profileController; // profile master
	public SpriteRenderer renderer;
	public GameObject lockSprite;
	public AudioClip sound;
	public int price = 500;
	public int state; // 0 => unlock, 1 => dismiss
	public int slot;
	// Use this for initialization
	void Start () {
		if( state == 0 ){
			if (GameData.profile.formationList[slot].IsUnlocked){
				lockSprite.SetActive(false);
				this.gameObject.SetActive (false);
			}
		}
		else if ( state == 1 ) {
			Debug.Log ("at btn "+ name +" slot  " + slot + " heroid " + GameData.profile.formationList [slot].UnitHeroId);
			if ( GameData.profile.formationList[slot].UnitHeroId != 99 ){
				this.gameObject.SetActive(true);
			}
			else 
				this.gameObject.SetActive(false);
		}

	}
	
	void OnMouseDown(){
		if ( state == 0 ){
		if (GameData.profile.Gold >= price) {
			GameData.profile.formationList[slot].IsUnlocked = true;
			lockSprite.SetActive(false);
			this.gameObject.SetActive(false);
			GameData.profile.unlockedSlot++;
			// awal buka kasih knight
			//	ReloadSprite(GameData.unitList[0].Sprites);
			profileController.UpdateGoldAndDiamond(0,price);
			}
		}
		else{
			if ( GameData.profile.activeHeroes > 1 ){
				GameData.profile.unitList[GameData.profile.formationList[slot].Unit.HeroId].IsActive = false;
				GameData.profile.formationList[slot].UnitHeroId = 99;
				renderer.sprite = null;
				GameData.profile.activeHeroes--;
				this.gameObject.SetActive(false);
			}
		}
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);
	}
}

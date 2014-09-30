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

	void Start(){
		//Debug.Log ("awal2 slot " + slot + " isunlock " + GameData.skillList [slot].IsUnlocked + " selec " + GameData.skillList [slot].IsSelected);
		if (GameData.unitList [slot].IsActive && GameData.unitList [slot].IsUnlocked) {
			renderer.sprite = selectedSprite;
		} else if (!GameData.unitList [slot].IsActive && GameData.unitList [slot].IsUnlocked) {
			renderer.sprite = deselectedSprite;		
		} 
	}

	void OnMouseDown(){
		// UNLOCK HERO
		if (GameData.profile.Gold >= GameData.unitList [slot].GoldNeeded && !GameData.unitList [slot].IsUnlocked) {
						GameData.unitList [slot].IsUnlocked = true;
						frame.SetActive (false);
						GameData.profile.Gold -= GameData.unitList [slot].GoldNeeded;
						profileController.UpdateGoldAndDiamond();	
						renderer.sprite = deselectedSprite;

				} else {
			Debug.Log("gak ada uang");
		}
		// gak dipake heronya
		if (GameData.unitList [slot].IsActive && GameData.unitList [slot].IsUnlocked  && GameData.activeHeroes > 0) {
			GameData.activeHeroes--;
			renderer.sprite = deselectedSprite;
			GameData.unitList[slot].IsActive = false;
		}
		// make heronya
		else if (!GameData.unitList [slot].IsActive && GameData.unitList [slot].IsUnlocked  &&  
		         GameData.activeHeroes < GameData.unlockedSlot) {
			GameData.activeHeroes++;
			renderer.sprite = selectedSprite;
			GameData.unitList[slot].IsActive = true;
		}
		Debug.Log ("aktif hero " + GameData.activeHeroes);

	}
}

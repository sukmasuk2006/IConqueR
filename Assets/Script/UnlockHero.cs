using UnityEngine;
using System.Collections;

public class UnlockHero : MonoBehaviour {

	public int slot;
	public GameObject frame;
	public Sprite sprite;
	public SpriteRenderer renderer;
	public ProfileController profileController;

	void OnMouseDown(){
		if (GameData.gold >= GameData.unitList [slot].GoldNeeded && !GameData.unitList [slot].IsUnlocked) {
						GameData.unitList [slot].IsUnlocked = true;
						GameData.unitList [slot].IsActive = true;
						frame.SetActive (false);
						GameData.unlockedHeroes++;
						GameData.gold -= GameData.unitList [slot].GoldNeeded;
						profileController.UpdateGoldAndDiamond();	
						this.gameObject.SetActive (false);
				} else {
					
		}
	}
}

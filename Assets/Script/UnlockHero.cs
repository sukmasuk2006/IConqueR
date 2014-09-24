using UnityEngine;
using System.Collections;

public class UnlockHero : MonoBehaviour {

	public int slot;
	public GameObject frame;
	public Sprite sprite;
	public SpriteRenderer renderer;
	public ProfileController profileController;

	void OnMouseDown(){
		if (GameData.profile.Gold >= GameData.unitList [slot].GoldNeeded && !GameData.unitList [slot].IsUnlocked) {
						GameData.unitList [slot].IsUnlocked = true;
						GameData.unitList [slot].IsActive = true;
						frame.SetActive (false);
						GameData.activeHeroes++;
						GameData.profile.Gold -= GameData.unitList [slot].GoldNeeded;
						profileController.UpdateGoldAndDiamond();	
						this.gameObject.SetActive (false);
				} else {
			Debug.Log("gak ada uang");
		}
		if (GameData.unitList [slot].IsActive && GameData.unitList [slot].IsUnlocked  && GameData.activeHeroes > 0) {
			GameData.totalSkillUsed--;
			renderer.sprite = deselectedSprite;
			GameData.unitList[slot].IsActive = false;
		}
		else if (!GameData.unitList [slot].IsActive && GameData.unitList [slot].IsUnlocked  && GameData.activeHeroes > 0) {
			GameData.totalSkillUsed--;
			renderer.sprite = deselectedSprite;
			GameData.unitList[slot].IsActive = false;
		}

	}
}

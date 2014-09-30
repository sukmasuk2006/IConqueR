using UnityEngine;
using System.Collections;

public class UnlockSkill : MonoBehaviour {

	public int slot;
	public GameObject frame;
	public Sprite lockedSprite;
	public Sprite selectedSprite;
	public Sprite deselectedSprite;
	public SpriteRenderer renderer;
	public TextMesh text;

	public ProfileController profileController;

	void Start(){
		//Debug.Log ("awal2 slot " + slot + " isunlock " + GameData.skillList [slot].IsUnlocked + " selec " + GameData.skillList [slot].IsSelected);
		if (GameData.skillList [slot].IsSelected && GameData.skillList [slot].IsUnlocked) {
						renderer.sprite = selectedSprite;
		} else if (!GameData.skillList [slot].IsSelected && GameData.skillList [slot].IsUnlocked) {
				renderer.sprite = deselectedSprite;		
		} else
				renderer.sprite = lockedSprite;
	}

	void OnMouseDown(){

		//Debug.Log ("slot " + slot + " isunlock " + GameData.skillList [slot].IsUnlocked + " selec " + GameData.skillList [slot].IsSelected);
		if (GameData.profile.Gold >= GameData.skillList[slot].Price && !GameData.skillList [slot].IsUnlocked 
		                && GameData.unitList[GameData.skillList[slot].HeroesRequired].IsUnlocked ) {
						GameData.skillList [slot].IsUnlocked = true;
						GameData.skillList [slot].IsSelected = false;
						frame.SetActive (false);
						profileController.SendMessage("UpdateGoldAndDiamond");	
						renderer.sprite = deselectedSprite;
				
		} else {
			if ( GameData.profile.Gold < GameData.skillList[slot].Price )
				text.text = "Not enough money..";
			else if ( !GameData.unitList[GameData.skillList[slot].HeroesRequired].IsUnlocked )
				text.text = "You must unlock " + GameData.unitList[GameData.skillList[slot].HeroesRequired].Name + " first!";			
		}
		if (GameData.skillList [slot].IsSelected && GameData.skillList [slot].IsUnlocked  && GameData.totalSkillUsed > 0) {
			GameData.totalSkillUsed--;
			renderer.sprite = deselectedSprite;
			GameData.skillList[slot].IsSelected = false;
		}
		else if (!GameData.skillList [slot].IsSelected && GameData.skillList [slot].IsUnlocked && GameData.totalSkillUsed < 2) {
			GameData.totalSkillUsed++;
			renderer.sprite = selectedSprite;		
			GameData.skillList[slot].IsSelected = true;
		}
	}
}

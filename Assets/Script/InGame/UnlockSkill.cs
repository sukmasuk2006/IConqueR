using UnityEngine;
using System.Collections;

public class UnlockSkill : MonoBehaviour {

	public int slot;
	public GameObject frame;
	public Sprite selectedSprite;
	public Sprite deselectedSprite;
	public TextMesh buttonInfo;
	public SpriteRenderer renderer;
	public TextMesh text;
	public AudioClip sound;

	public ProfileController profileController;

	void Start(){
//		Debug.Log ("skill ke " + slot + " unlock " + GameData.profile.skillList [slot].IsUnlocked);
		if (!GameData.profile.skillList [slot].IsUnlocked) {
			renderer.sprite = deselectedSprite;
			buttonInfo.text = "unlock";
		}
		//Debug.Log ("awal2 slot " + slot + " isunlock " + GameData.profile.skillList [slot].IsUnlocked + " selec " + GameData.profile.skillList [slot].IsSelected);
		else if (GameData.profile.skillList [slot].IsSelected && GameData.profile.skillList [slot].IsUnlocked) {
						renderer.sprite = selectedSprite;
						buttonInfo.text = "deselect";
				} else if (!GameData.profile.skillList [slot].IsSelected && GameData.profile.skillList [slot].IsUnlocked) {
						renderer.sprite = deselectedSprite;		
						buttonInfo.text = "select";
				} 
	}

	void OnMouseDown(){
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);

		//Debug.Log ("slot " + slot + " isunlock " + GameData.profile.skillList [slot].IsUnlocked + " selec " + GameData.profile.skillList [slot].IsSelected);
		if (GameData.profile.Gold >= GameData.profile.skillList[slot].Price && !GameData.profile.skillList [slot].IsUnlocked 
		                && GameData.profile.unitList[GameData.profile.skillList[slot].HeroesRequired].IsUnlocked ) {
						GameData.profile.skillList [slot].IsUnlocked = true;
						GameData.profile.skillList [slot].IsSelected = false;
						frame.SetActive (false);
			profileController.UpdateGoldAndDiamond(0,GameData.profile.skillList[slot].Price);
						renderer.sprite = deselectedSprite;
						buttonInfo.text = "select";
		} else {
			if ( GameData.profile.Gold < GameData.profile.skillList[slot].Price )
				text.text = "Not enough money..";
			else if ( !GameData.profile.unitList[GameData.profile.skillList[slot].HeroesRequired].IsUnlocked )
				text.text = "You must unlock " + GameData.profile.unitList[GameData.profile.skillList[slot].HeroesRequired].Name + " first!";			
		}
		if (GameData.profile.skillList [slot].IsSelected && GameData.profile.skillList [slot].IsUnlocked  && GameData.profile.totalSkillUsed > 0) {
			GameData.profile.totalSkillUsed--;
			renderer.sprite = deselectedSprite;
			buttonInfo.text = "select";
			GameData.profile.skillList[slot].IsSelected = false;
		}
		else if (!GameData.profile.skillList [slot].IsSelected && GameData.profile.skillList [slot].IsUnlocked && GameData.profile.totalSkillUsed < 2) {
			GameData.profile.totalSkillUsed++;
			renderer.sprite = selectedSprite;
			buttonInfo.text = "deselect";
			GameData.profile.skillList[slot].IsSelected = true;
		}
	}
}

﻿	using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UnlockSkill : MonoBehaviour {

	public int slot;
	public GameObject frame;
	public Sprite selectedSprite;
	public Sprite deselectedSprite;
	public TextMesh buttonInfo;
	public SpriteRenderer renderer;
	public TextMesh text;

	public TextMesh priceText;
	public TextMesh skillLevel;
	public TextMesh skillEffect;

	public AudioClip sound;

	public ProfileController profileController;

	void Start(){
		Skill s = GameData.profile.skillList [slot];
//		Debug.Log ("di button skill ke " + slot + " unlock " + s.IsUnlocked +
//		           " selected " + s.IsSelected);
		priceText.text = (s.Price * (s.Level + 1)) + "";
		skillLevel.text = "Level " + s.Level + "";
		//skillEffect.text = s.Effect.Amount + " % ";
		if (GameData.profile.skillList [slot].Level < 1 ) {
			renderer.sprite = deselectedSprite;
			buttonInfo.text = "Unlock";
		}
		else if (GameData.profile.skillList [slot].Level > 3 ) {
			renderer.sprite = selectedSprite;
			buttonInfo.text = "Upgrade";
			priceText.text = "";
			frame.SetActive(false);
		}
		skillLevel.text = "Level " + s.Level;
	}

	void OnMouseDown(){
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);

		Skill skill = GameData.profile.skillList[slot];
		//Debug.Log ("slot " + slot + " isunlock " + GameData.profile.skillList [slot].IsUnlocked + " selec " + GameData.profile.skillList [slot].IsSelected);
		if (GameData.profile.Gold >= skill.Price * skill.Level  ){// uang cukup
			Debug.Log("uang cukup");
			if (skill.Level < 1   && GameData.profile.unitList[skill.HeroesRequired].IsUnlocked) {//locked & hero udah dilock
				skill.IsUnlocked = true;
				frame.SetActive (false); 					// UNLOCK
				renderer.sprite = selectedSprite;
				buttonInfo.text = "Upgrade";
				skill.Level++;
				priceText.text = skill.Price * (skill.Level + 1) + "";
				Debug.Log("unlock");
			}
			else if ( skill.Level > 0 && skill.Level < 3 ){// ady unlocked
				skill.Level++;
				priceText.text = skill.Price * (skill.Level + 1) + "";
				//		skillEffect.text = (skill.Effect.Amount * skill.Level) + "";
				profileController.UpdateGoldAndDiamond(0,GameData.profile.skillList[slot].Price);
				Debug.Log("upgrade");
			}
			else if ( !GameData.profile.unitList[GameData.profile.skillList[slot].HeroesRequired].IsUnlocked )
				text.text = "You must unlock " + GameData.profile.unitList[GameData.profile.skillList[slot].HeroesRequired].Name + " first!";		

		}
		else if ( GameData.profile.Gold < skill.Price * skill.Level )
			text.text = "Not enough money..";
		if ( skill.Level > 2 )
		{
			priceText.text = "-";
			text.text = "Max Level";
		}
		skillLevel.text = "Level " + GameData.profile.skillList[slot].Level;

//		Debug.Log("skil end");
		StartCoroutine(alp());
		GameData.SaveData ();
	}

	
	public IEnumerator alp(){
		yield return new WaitForSeconds(1.5f);
		text.text = "";
		
	}
}

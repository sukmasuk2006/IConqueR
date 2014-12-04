using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ConfirmUnlockHero : MonoBehaviour {
	
	public TextMesh text1;
	public TextMesh text2;
	public ProfileController profileController;
	public int state = 0;
	private  int slot;
	public GameObject parent; // balik ke hidden spot si gameobject confirmUnlockHero
	public AudioClip coinSound;
	private AudioSource audio;
	public List<GameObject> frameList;
	public List<GameObject> buttonList;
	public List<HeroSlotController> heroSlot;
	public List<FormationSetter> formationSlot;

	// Use this for initialization
	void Start () {
		audio =	MusicManager.getMusicPlayer().audio;
	}
	
	public int Slot {
		get {
			return slot;
		}
		set {
			slot = value;
		}
	}
	
	void OnMouseDown(){


		if (state == 0 && GameData.gameState == "UnlockHero") { // state 0 -> tombol ok
			ConfirmingBuy();
		} 
		else if (state == 0 && GameData.gameState == "UpgradeJob") { // state 0 -> tombol ok
			ConfirmingUpgradeJob();
		} 
		GameData.gameState = GameData.prevGameState;
		iTween.MoveTo (parent, iTween.Hash ("position", new Vector3 (0, -12f, -7.7f), "time", 0.1f, "oncomplete", "ReadyTween", "oncompletetarget", gameObject));
		
	}
	
	void ConfirmingBuy(){
		GameData.profile.unitList [slot].IsUnlocked = true;
		Debug.Log("Berhasil unlock hero " + slot + " "  + GameData.profile.unitList [slot].IsUnlocked);
		frameList[slot].SetActive (false);
		profileController.UpdateGoldAndDiamond(0,GameData.profile.unitList [slot].GoldNeeded);
		buttonList[slot].GetComponentInChildren<TextMesh>().text = "Enhance";
		heroSlot[slot].ReloadData();
	}

	void ConfirmingUpgradeJob(){
		Unit u = GameData.profile.unitList [slot];
		u.IsUnlocked = true;
		Debug.Log("Berhasil unlock hero " + slot + " "  + GameData.profile.unitList [slot].IsUnlocked);
		frameList[slot].SetActive (false);
		profileController.UpdateGoldAndDiamond(0,GameData.profile.unitList [slot].GoldNeeded);
		u.EnhanceJob();
		for ( int i = 0 ; i < 5 ; i++ ){
			if ( GameData.profile.formationList[i].UnitHeroId == u.HeroId )
				formationSlot[i].ReloadSprite(u.JobList[u.CurrentJob]);
		}
		buttonList[slot].SetActive(false); // temp
		heroSlot[slot].ReloadData();

	}
	

	void ReadyTween(){
		GameData.readyToTween = true;
	}
}

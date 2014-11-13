using UnityEngine;
using System.Collections.Generic;

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
		
		if (state == 0) { // state 0 -> tombol ok
			ConfirmingBuy();
		} 
		iTween.MoveTo (parent, iTween.Hash ("position", new Vector3 (0, -12f, -6f), "time", 0.1f, "oncomplete", "ReadyTween", "oncompletetarget", gameObject));
		
	}
	
	void ConfirmingBuy(){
		GameData.profile.unitList [slot].IsUnlocked = true;
		frameList[slot].SetActive (false);
		profileController.UpdateGoldAndDiamond(0,GameData.profile.unitList [slot].GoldNeeded);	
	}
	

	void ReadyTween(){
		GameData.readyToTween = true;
	}
}

using UnityEngine;
using System.Collections.Generic;

public class UnlockFormationSlot : MonoBehaviour {

	public ProfileController profileController; // profile master
	public List<GameObject> lockSprite;
	public AudioClip sound;
	// Use this for initialization
	void Start () {
		if (GameData.profile.unlockedSlot == 5)
						this.gameObject.SetActive (false);
				else
						SetPos ();
	}
	
	void OnMouseDown(){
		if (GameData.profile.unlockedSlot < 5 && GameData.profile.Gold >= GameConstant.UNLOCK_SLOT_PRICE) {
			GameData.profile.formationList[GameData.profile.unlockedSlot].IsUnlocked = true;
			lockSprite[GameData.profile.unlockedSlot++].SetActive(false);
			// awal buka kasih knight
			//	ReloadSprite(GameData.unitList[0].Sprites);
				profileController.UpdateGoldAndDiamond(0,GameConstant.UNLOCK_SLOT_PRICE);
			SetPos();
			if (GameData.profile.unlockedSlot == 5)
				this.gameObject.SetActive (false);
		}
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);

	}

	private void SetPos(){
		if (GameData.profile.unlockedSlot < 5 )
		this.gameObject.transform.position = new Vector3 (lockSprite [GameData.profile.unlockedSlot].transform.position.x,
		                                                  this.gameObject.transform.position.y,
		                                                  this.gameObject.transform.position.z);

	}
}

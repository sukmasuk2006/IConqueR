using UnityEngine;
using System.Collections.Generic;

public class UnlockFormationSlot : MonoBehaviour {

	public ProfileController profileController; // profile master
	public List<GameObject> lockSprite;
	// Use this for initialization
	void Start () {
		if (GameData.unlockedSlot == 5)
						this.gameObject.SetActive (false);
				else
						SetPos ();
	}
	
	void OnMouseDown(){
		if (GameData.unlockedSlot < 5 && GameData.profile.Gold >= GameConstant.UNLOCK_SLOT_PRICE) {
			GameData.formationList[GameData.unlockedSlot].IsUnlocked = true;
			lockSprite[GameData.unlockedSlot++].SetActive(false);
			GameData.profile.Gold -= GameConstant.UNLOCK_SLOT_PRICE;
			// awal buka kasih knight
			//	ReloadSprite(GameData.unitList[0].Sprites);
			profileController.UpdateGoldAndDiamond();
			SetPos();
			if (GameData.unlockedSlot == 5)
				this.gameObject.SetActive (false);
		}
	}

	private void SetPos(){
		if (GameData.unlockedSlot < 5 )
		this.gameObject.transform.position = new Vector3 (lockSprite [GameData.unlockedSlot].transform.position.x,
		                                                  this.gameObject.transform.position.y,
		                                                  this.gameObject.transform.position.z);

	}
}

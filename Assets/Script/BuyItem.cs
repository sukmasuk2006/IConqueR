using UnityEngine;
using System.Collections;

public class BuyItem : MonoBehaviour {

	//public 
	public ScreenData data;
	public int slot;
	public ProfileController profileController;
	// Use this for initialization
	void Start () {
		
	}

	void OnMouseDown(){
		if (GameData.profile.Gold - GameData.shopList [(data.corridorState * 4) + slot].Price >= 0) {
						GameData.profile.Gold -= GameData.shopList [(data.corridorState * 4) + slot].Price;
						GameData.inventoryList.Add (GameData.shopList [(data.corridorState * 4) + slot]);
						profileController.SendMessage("UpdateGoldAndDiamond");	
						Debug.Log ("purchased in inventory " + GameData.inventoryList [GameData.inventoryList.Count-1].Name);
				} else {
			Debug.Log("not enough money");		
		}

	}
}

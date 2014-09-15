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
		if (GameData.gold - GameData.shopList [(data.corridorState * 4) + slot].Price >= 0) {
						GameData.gold -= GameData.shopList [(data.corridorState * 4) + slot].Price;
						GameData.inventoryList.Add (GameData.shopList [(data.corridorState * 4) + slot]);
						profileController.SendMessage("UpdateGoldAndDiamond");	
						Debug.Log ("purchased " + GameData.shopList [(data.corridorState * 4) + slot].Name);
				} else {
			Debug.Log("not enough money");		
		}

	}
}

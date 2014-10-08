using UnityEngine;
using System.Collections;

public class BuyItem : MonoBehaviour {

	//public 
	public ScreenData data;
	public int slot;
	public ProfileController profileController;
	public ScreenData inventoryData;
	public AudioClip sound;
	// Use this for initialization

	void Start () {
		
	}

	void OnMouseDown(){
		Item i = GameData.shopList [(data.corridorState * 4) + slot];
		int money = profileController.GetMoneyValue (i.PriceType);
		if (money - i.Price >= 0) {
			int value = GameData.shopList [(data.corridorState * 4) + slot].Price;
			profileController.UpdateGoldAndDiamond(i.PriceType,value);
			GameData.profile.inventoryList.Add (GameData.shopList [(data.corridorState * 4) + slot]);
//			Debug.Log("uang terpakai gold " + GameData.profile.Gold +" diam " + GameData.profile.Diamond 
//			          +" mon " + money );
//			Debug.Log ("purchased in inventory " + GameData.profile.inventoryList [GameData.profile.inventoryList.Count-1].Name);
			inventoryData.maxCorridorState = (GameData.profile.inventoryList.Count/4);
			if (GameData.profile.inventoryList.Count % 4 == 0)
							inventoryData.maxCorridorState--;
						inventoryData.UpdateMaxCorridor();
				} else {
			Debug.Log("not enough money");		
		}
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);

	}
}

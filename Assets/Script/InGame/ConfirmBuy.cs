using UnityEngine;
using System.Collections.Generic;

public class ConfirmBuy : MonoBehaviour {

	public TextMesh text1;
	public TextMesh text2;
	public ScreenData data;
	public ProfileController profileController;
	public ScreenData inventoryData;
	public int state = 0;
	private  int slot;
	public GameObject parent;
	public List<InventorySetter> inventoryList;
	public AudioClip coinSound;
	public AudioClip failSound;
	private AudioSource audio;

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
			if (GameData.gameState == "ConfirmBuy") {
				ConfirmingBuy();
			} else if (GameData.gameState == "ConfirmSell") {
				ConfirmSell();			
			}

		} else {// state 1 -> tombol no
			if (GameData.gameState == "ConfirmBuy") // ketika user mau beli tapi gak jadi 
				GameData.gameState = "Buy";
			else 
				GameData.gameState = "Sell";// ketika user mau jual tapi gak jadi

		}
		iTween.MoveTo (parent, iTween.Hash ("position", new Vector3 (0, -12f, -6f), "time", 0.1f, "oncomplete", "ReadyTween", "oncompletetarget", gameObject));

	}

	void ConfirmingBuy(){
		Debug.Log ("stae " + data.corridorState + " slot " + slot);
		Item i = GameData.shopList [(data.corridorState * 4) + slot];
		int money = profileController.GetMoneyValue (i.PriceType);
		if (money - i.Price >= 0) {
			int value = GameData.shopList [(data.corridorState * 4) + slot].Price;
			profileController.UpdateGoldAndDiamond (i.PriceType, value);
			GameData.profile.inventoryList.Add (GameData.shopList [(data.corridorState * 4) + slot]);
			//			Debug.Log("uang terpakai gold " + GameData.profile.Gold +" diam " + GameData.profile.Diamond 
			//			          +" mon " + money );
			//			Debug.Log ("purchased in inventory " + GameData.profile.inventoryList [GameData.profile.inventoryList.Count-1].Name);
			inventoryData.maxCorridorState = (GameData.profile.inventoryList.Count / 4);
			if (GameData.profile.inventoryList.Count % 4 == 0)
				inventoryData.maxCorridorState--;
			inventoryData.UpdateMaxCorridor ();
			audio.PlayOneShot(coinSound);
			GameData.gameState = "Buy";
		} else {
			Debug.Log ("not enough money");		
			audio.PlayOneShot(failSound);
		}
	}

	void ConfirmSell(){
		Debug.Log ("sell slot " + (inventoryData.corridorState * 4) + slot);
		Item j = GameData.profile.inventoryList [(inventoryData.corridorState * 4) + slot];
		profileController.UpdateGoldAndDiamond (j.PriceType, -j.Price / 2); // - berarti menjual
		GameData.profile.inventoryList.RemoveAt ((inventoryData.corridorState * 4) + slot);
		for (int i = 0; i < inventoryList.Count; i++) {
			inventoryList [i].UpdateSlotForSell ();
		}
		data.corridorState = 0;
		GameData.gameState = "Sell";
		audio.PlayOneShot(coinSound);
	}

	void ReadyTween(){
	//	GameData.readyToTween = true;
	}
}

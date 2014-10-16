using UnityEngine;
using System.Collections;

public class ConfirmBuy : MonoBehaviour {

	public TextMesh text1;
	public TextMesh text2;
	public ScreenData data;
	public ProfileController profileController;
	public ScreenData inventoryData;
	public int state = 0;
	private  int slot;
	public GameObject parent;
	// Use this for initialization
	void Start () {
	
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
		if (state == 0) {
			Debug.Log("stae " + data.corridorState +" slot " + slot );
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
			} else {
				Debug.Log ("not enough money");		
			}
		iTween.MoveTo (parent, iTween.Hash ("position", new Vector3 (0, -12f, -5f), "time", 0.1f, "oncomplete", "ReadyTween", "oncompletetarget", gameObject));
		}
	}

	void ReadyTween(){
		GameData.readyToTween = true;
		GameData.gameState = "Shop";
	}
}

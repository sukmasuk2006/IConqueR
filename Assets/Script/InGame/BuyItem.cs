using UnityEngine;
using System.Collections;

public class BuyItem : MonoBehaviour {

	//public 
	public ScreenData data;
	public int slot;
	public ProfileController profileController;
	public ScreenData inventoryData;
	public AudioClip sound;
	public ConfirmBuy confirm;
	public GameObject confirmScreen;
	// Use this for initialization

	void Start () {
		
	}

	void OnMouseUp(){
		Item i = GameData.shopList [(data.corridorState * 4) + slot];
		int money = profileController.GetMoneyValue (i.PriceType);

		if (GameData.gameState != "confirm" && GameData.readyToTween && money >= i.Price) {
			GameData.readyToTween = false;
						confirm.text1.text = "Buy " + i.Name;
						confirm.text2.text = "For " + i.Price + " ? ";
						confirm.Slot = slot;
						MusicManager.getMusicEmitter ().audio.PlayOneShot (sound);
						iTween.MoveTo (confirmScreen, iTween.Hash ("position", new Vector3(0,0,confirmScreen.transform.position.z), "time", 0.1f, "oncomplete", "ReadyTween", "oncompletetarget", gameObject));
				}
	}
	
	void ReadyTween(){
		GameData.readyToTween = true;
		GameData.gameState = "confirm";
	}
}

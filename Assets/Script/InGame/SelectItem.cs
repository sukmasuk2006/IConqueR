using UnityEngine;
using System.Collections;
//PADA BUTTON
public class SelectItem : MonoBehaviour {

	public UpgradeWeaponController controller;
	public int slot;
	public ScreenData data;
	public GameObject upgradeScreen;
	public GameObject confirmScreen;
	public GameObject targetObject;
	public string gameStateTarget;
	public AudioClip sound;
	private Vector3 tempPosition;
	public ConfirmBuy confirm;
	public SpriteRenderer priceType;
	public Sprite gold;
	public Sprite diamond;
	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown(){
		
			//copy
			//Item[] itemlist = new Item[GameData.profile.inventoryList.Count];
			//GameData.profile.inventoryList.CopyTo(itemlist);
			// pasang di slot upgrade
		//	Debug.Log("slot " + controller.SlotList.Count+ " itemlistke " + itemlist[(4 * data.corridorState)+slot]);
			// pasang gem di slot yang di upgrade dengan item yang dipilih
			if (GameData.gameState == "Upgrade") {
						controller.SlotList [controller.UpgradedSlot] = controller.queriedList [(4 * data.corridorState) + slot];
						// pasang gambar gem di slot yang diupgrade
						controller.UpdateSlot (controller.queriedList [(4 * data.corridorState) + slot].Id);
						//biar gak dobel pas nyari lagi di invent
						GameData.profile.inventoryList.Remove (controller.queriedList [(4 * data.corridorState) + slot]);
						// UPDATE SLOT DI CHOOSE GEM SCREEN ke slot
						data.corridorState = 0;controller.UpdateSemuaGambarDiInventory ();
						data.maxCorridorState = (GameData.profile.inventoryList.Count / 4);
						if (GameData.profile.inventoryList.Count % 4 == 0)
								data.maxCorridorState--;
						data.UpdateMaxCorridor ();
				}
	}

	void OnMouseUp(){
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);
		//HOTween.To(tweenedObject,0.5f,"position",targetObject.transform.position);
		tempPosition = targetObject.transform.position;
//		GameObject tweenedObject = GameData.gameState == "Upgrade" ? upgradeScreen : 	
		if (GameData.readyToTween  ) {
			GameData.readyToTween = false;
			if ( GameData.gameState == "Upgrade" ){
				iTween.MoveTo ( targetObject,iTween.Hash("position",upgradeScreen.transform.position,"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
				GameData.gameState = "Upgrade";	
			}
				else if ( GameData.gameState == "Sell" ){
				iTween.MoveTo ( confirmScreen,iTween.Hash("position",new Vector3(0,0,-7),"time", 0.1f,"onComplete","ReadyTween2","onCompleteTarget",gameObject));
				Item i = GameData.profile.inventoryList [(4 * data.corridorState) + slot];
				confirm.Slot = slot;
				priceType.sprite = i.PriceType == 0 ? gold : diamond;
				confirm.text1.text = "Sell " + i.Name;
				confirm.text2.text = "For " + i.Price/2 + " ? ";
				GameData.gameState = "ConfirmSell";	
			}
			//sound.audio.PlayOneShot (sound.audio.clip);
			data.corridorState = 0;
		}
	}
	
	void ReadyTween(){
		iTween.MoveTo ( upgradeScreen,iTween.Hash("position",tempPosition,"time", 0.1f,"onComplete","ReadyTween2","onCompleteTarget",gameObject));
		
	}
	
	void ReadyTween2(){
		GameData.readyToTween = true;
	}
}

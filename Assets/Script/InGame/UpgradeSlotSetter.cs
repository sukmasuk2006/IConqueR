using UnityEngine;
using System.Collections.Generic;

// DI SLOT UPGRADE
public class UpgradeSlotSetter : MonoBehaviour {

	public UpgradeWeaponController controller;
	public GameObject tweenedObject;
	public GameObject targetObject;
	public int slot;
	private Vector3 tempPosition;
	public AudioClip sound;
	public TextMesh infoText;
	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown(){
		//HOTween.To(tweenedObject,0.5f,"position",targetObject.transform.position);
			// jika udah ada isinya, masukin invent lagi
		if (GameData.gameState != "confirm") {
						tempPosition = targetObject.transform.position;
						if (controller.SlotList [slot] is Gem || controller.SlotList [slot] is Catalyst) {
								controller.RemoveASlot (slot);
								controller.UpdateSemuaGambarDiInventory ();
						} else if (GameData.readyToTween) {
								GameData.readyToTween = false;
								controller.UpgradedSlot = slot;
								// cek kalau slot 0 itu gem, dll itu catalyst yg di on-kan buttonya

								iTween.MoveTo (targetObject, iTween.Hash ("position", tweenedObject.transform.position, "time", 0.1f, "onComplete", "ReadyTween", "onCompleteTarget", gameObject));

						}
						infoText.text = "";
				}
	}
	
	void OnMouseUp(){
		//MusicManager.getMusicEmitter().audio.PlayOneShot(sound);

	}
	
	void ReadyTween(){
		GameData.gameState = targetObject.name;
		GameData.readyToTween = true;
		iTween.MoveTo (tweenedObject, tempPosition,0.3f);		
//		Debug.Log ("Oncomplete");
		foreach (InventorySetter i in controller.selectItemButton){
			i.CheckButton ();
		}
	}
}

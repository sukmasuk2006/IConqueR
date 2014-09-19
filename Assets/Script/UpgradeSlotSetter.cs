using UnityEngine;
using System.Collections.Generic;

// DI SLOT UPGRADE
public class UpgradeSlotSetter : MonoBehaviour {

	public UpgradeWeaponController controller;
	public GameObject tweenedObject;
	public GameObject targetObject;
	public int slot;
	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown(){
		//HOTween.To(tweenedObject,0.5f,"position",targetObject.transform.position);
			// jika udah ada isinya, masukin invent lagi
		if (controller.SlotList [slot] is Gem || controller.SlotList [slot] is Catalyst) {
			controller.RemoveASlot(slot);
			controller.UpdateSemuaGambarDiInventory();
		}
		else if (GameData.readyToTween) {
			GameData.readyToTween = false;
			controller.UpgradedSlot = slot;
			// cek kalau slot 0 itu gem, dll itu catalyst yg di on-kan buttonya
			foreach (InventorySetter i in controller.selectItemButton){
				
				i.CheckButton ();
			}
			iTween.MoveTo ( tweenedObject,iTween.Hash("position",targetObject.transform.position,"time", 0.1f,"oncomplete","ReadyTween","oncompletetarget",gameObject));
			iTween.MoveTo (targetObject, tweenedObject.transform.position,0.1f);		
			                                                      
		}
	}
	
	void OnMouseUp(){
		
	}
	
	void ReadyTween(){
		GameData.readyToTween = true;
		Debug.Log ("ready " +GameData.readyToTween + "  gamestate " + tweenedObject.name +" " +  GameData.gameState);
	}
}

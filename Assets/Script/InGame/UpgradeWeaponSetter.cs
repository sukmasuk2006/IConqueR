using UnityEngine;
using System.Collections.Generic;

public class UpgradeWeaponSetter : MonoBehaviour {

	// DI PROFILE
	public UpgradeWeaponController controller;
	public List<InventorySetter> listInv;
	public TextMesh infoTop;
	public TextMesh infoBot;
	// Use this for initialization
	void Start () {
		
	}

	void OnMouseUp(){
		infoTop.text = "Tap to fill a slot";
		infoBot.text = "";
		controller.InitializeWeapon ();
		for ( int i = 0 ; i < listInv.Count ; i++ ){
			listInv[i].UpdateSlotForUpgrade();
		}
	}
}

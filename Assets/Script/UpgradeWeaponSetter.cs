using UnityEngine;
using System.Collections.Generic;

public class UpgradeWeaponSetter : MonoBehaviour {

	// DI PROFILE
	public UpgradeWeaponController controller;
	public List<InventorySetter> listInv;
	// Use this for initialization
	void Start () {
		
	}

	void OnMouseDown(){
		controller.SetWeapon ();
		for ( int i = 0 ; i < listInv.Count ; i++ ){
			listInv[i].UpdateSlot();
		}
	}
}

using UnityEngine;
using System.Collections;

public class CancelUpgrade : MonoBehaviour {

	public UpgradeWeaponController controller;
	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown(){
		controller.RemoveSlot ();
	}
}

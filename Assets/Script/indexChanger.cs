using UnityEngine;
using System.Collections;

public class indexChanger : MonoBehaviour {

	public bool isIncrease = true;
	public GameObject controller;
	InventoryController invent;
	// Use this for initialization
	void Start () {
		invent = controller.GetComponent<InventoryController> ();
		Debug.Log ("GO");
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown(){
		Debug.Log (isIncrease);
		if (isIncrease) {
			Debug.Log("naik");

			if ( GameData.inventoryList.Count+4 > (GameData.selectedToViewProfileId+1) * 4 ){
				GameData.selectedToViewProfileId++;
				Debug.Log("naik 1");
			}
		} 
		else {
			Debug.Log("turun");
			if (  GameData.selectedToViewProfileId > 1 )
				GameData.selectedToViewProfileId--;
		}
		invent.SendMessage ("ShowItem");
	}


}

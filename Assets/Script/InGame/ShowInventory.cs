using UnityEngine;
using System.Collections.Generic;

public class ShowInventory : MonoBehaviour {

	public List<InventorySetter> inventoryList;
	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown(){
		for (int i = 0; i < inventoryList.Count; i++) {
			inventoryList[i].UpdateSlotForSell();
		}
	}
}

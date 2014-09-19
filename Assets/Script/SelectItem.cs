using UnityEngine;
using System.Collections;
//PADA BUTTON
public class SelectItem : MonoBehaviour {

	public UpgradeWeaponController controller;
	public int slot;
	public ScreenData data;
	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown(){
		
		if (GameData.inventoryList.Count > (4 * data.corridorState)+slot) {
			//copy
			Item[] itemlist = new Item[GameData.inventoryList.Count];
			GameData.inventoryList.CopyTo(itemlist);
			// pasang di slot upgrade
			Debug.Log("slot " + controller.SlotList.Count+ " itemlistke " + itemlist[(4 * data.corridorState)+slot]);
			controller.SlotList[controller.UpgradedSlot] = itemlist[(4 * data.corridorState)+slot];
			controller.UpdateSlot(controller.UpgradedSlot);
			//biar gak dobel pas nyari lagi di invent
			GameData.inventoryList.RemoveAt((4 * data.corridorState)+slot);
			// UPDATE SLOT DI CHOOSE GEM SCREEN ke slot
			controller.UpdateSemuaGambarDiInventory();
		}// else {
		//	if (GameData.inventoryList [slot] is Gem) {
		//		controller.SlotList.Add (GameData.inventoryList [(4 * data.corridorState)+slot]);
			
		//	}

		//}
	}
}

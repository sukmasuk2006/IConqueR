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
		
			//copy
			Item[] itemlist = new Item[GameData.profile.inventoryList.Count];
			GameData.profile.inventoryList.CopyTo(itemlist);
			// pasang di slot upgrade
		//	Debug.Log("slot " + controller.SlotList.Count+ " itemlistke " + itemlist[(4 * data.corridorState)+slot]);
			// pasang gem di slot yang di upgrade dengan item yang dipilih
			controller.SlotList[controller.UpgradedSlot] = itemlist[(4 * data.corridorState)+slot];
			// pasang gambar gem di slot yang diupgrade
			controller.UpdateSlot(itemlist[(4 * data.corridorState)+slot].Id);
			//biar gak dobel pas nyari lagi di invent
			GameData.profile.inventoryList.RemoveAt((4 * data.corridorState)+slot);
			// UPDATE SLOT DI CHOOSE GEM SCREEN ke slot
			controller.UpdateSemuaGambarDiInventory();
			data.maxCorridorState = (GameData.profile.inventoryList.Count/4);
			if (GameData.profile.inventoryList.Count % 4 == 0)
						data.maxCorridorState--;
			data.UpdateMaxCorridor();
	}
}

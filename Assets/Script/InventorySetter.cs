using UnityEngine;
using System.Collections;
using System.Linq;

// DI SLOT CHOOSE GEM/CATALYST
public class InventorySetter : MonoBehaviour {

	public UpgradeWeaponController controller;
	public ScreenData data;
	public SpriteRenderer spriteRenderer;
	public int slot;
	public TextMesh teks;
	private string path = "";
	public GameObject selectButton;

	// Use this for initialization
	void Start () {
		UpdateSlot ();
	}

	public void UpdateSlot(){
		data.maxCorridorState = GameData.inventoryList.Count / 4;
		Sprite s = null;
		teks.text = "";
		try {
			s = GameData.inventoryList [(4 * data.corridorState)+slot].Sprites;
			teks.text = GameData.inventoryList [(4 * data.corridorState)+slot].Name; 
		}
		catch{
			teks.text = "";
		}
		spriteRenderer.sprite = s;
	}


	public void CheckButton(){
		Debug.Log("Cek buton");
		selectButton.SetActive (false);
		try {
			if (controller.UpgradedSlot == 0 && GameData.inventoryList [(4 * data.corridorState) + slot] is Gem
			    || controller.UpgradedSlot != 0 && GameData.inventoryList [(4 * data.corridorState) + slot] is Catalyst){
					selectButton.SetActive (true); 
				Debug.Log("slot " + slot + " aktif");
			}
		}
		catch{
			return;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}

﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
// DI SCREEN UPGRADE
public class UpgradeWeaponController : MonoBehaviour {

	private Weapon weaponData;
	public SpriteRenderer spriteRenderer;
	public TextMesh damageText;
	public TextMesh strText;
	public TextMesh agiText;
	public TextMesh vitText;// Use this for initialization
	// gambar di upgrade
	public List<SpriteRenderer> upgradeSlot;
	// gambar button di choose inventory
	public List<InventorySetter> selectItemButton;
	private float totalStr;
	private float totalAgi;
	private float totalVit;
	public GameObject confirmationScreen;
	private int upgradedSlot;
	private List<Item> slotList;
	private float percentages;
	public TextMesh fromText;
	public TextMesh toText;

	void Start () {
		InitilaizeSlot ();
	}

	private void InitilaizeSlot(){
		slotList = null;
		slotList = new List<Item> (4);
		slotList.Add (new Item (""));
		slotList.Add (new Item (""));
		slotList.Add (new Item (""));
		slotList.Add (new Item (""));

	}

	public void SetWeapon(){
		totalAgi = totalStr = totalVit = 0;
		weaponData = GameData.unitList [GameData.selectedToViewProfileId].Weapon;
		spriteRenderer.sprite = weaponData.Sprites;
		damageText.text = weaponData.Damage.ToString();
		fromText.text = weaponData.Rank.ToString();
		toText.text = (weaponData.Rank + 1).ToString ();
	}

	// update gambar di slot upgrade
	public void UpdateSlot(int slot){
		try {
			upgradeSlot [slot].sprite = slotList [slot].Sprites;
		}
		catch {
			upgradeSlot [slot].sprite = null;
		}
	}

	public void UpdateSemuaGambarDiInventory(){
		for ( int i = 0 ; i < selectItemButton.Count ; i++ )
		selectItemButton[i].UpdateSlot();
	}

	// jika sudah ada gem/catalyst terpasang, di klik akan balek ke invent
	public void RemoveASlot(int i){
		if (slotList [i] is Gem || slotList [i] is Catalyst)
			GameData.inventoryList.Add (slotList [i]);
		slotList [i]  = new Item("");
		UpdateSlot (i);
	}

	public void RemoveSlot(){
		for (int i = 0; i < slotList.Count; i++) {
						if (slotList [i] is Gem || slotList [i] is Catalyst)
								GameData.inventoryList.Add (slotList [i]);
			upgradeSlot [i].sprite = null;
		}

		slotList.RemoveRange(0,4);
		InitilaizeSlot ();
	}

	public void AfterUpgradeAttempt(){
		for (int i = 0; i < slotList.Count; i++) {
			upgradeSlot [i].sprite = null;
		}
		slotList.RemoveRange(0,4);
		InitilaizeSlot ();
	}

	public void StartCrafting(){
		bool success = false;
		float temp = Random.Range (0, 100) - weaponData.SuccessRate;
		if (temp < percentages) {
			success = true;
			weaponData.SuccessRate = 0;
			weaponData.Upgrade();
			SetWeapon();
		}Debug.Log("temp " + temp);
		AfterUpgradeAttempt();

	}

	public int UpgradedSlot {
		get {
			return upgradedSlot;
		}
		set {
			upgradedSlot = value;
		}
	}

	public List<Item> SlotList {
		get {
			return slotList;
		}
		set {
			slotList = value;
		}
	}

	public float Percentages {
		get {
			return percentages;
		}
		set {
			percentages = value;
		}
	}

	public Weapon WeaponData {
		get {
			return weaponData;
		}
		set {
			weaponData = value;
		}
	}
}
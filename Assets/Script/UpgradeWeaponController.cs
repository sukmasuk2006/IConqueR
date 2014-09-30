using UnityEngine;
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

	//upgrade data di profile
	public HeroProfileController profileController;

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

	public void InitializeWeapon(){
		weaponData = GameData.unitList [GameData.selectedToViewProfileId].Weapon;
		strText.text = weaponData.WeaponStats.Str + "";
		agiText.text = weaponData.WeaponStats.Agi + "";
		vitText.text = weaponData.WeaponStats.Vit +"";
		spriteRenderer.sprite = weaponData.Sprites;
		damageText.text = weaponData.Damage.ToString();
		fromText.text = weaponData.Rank.ToString();
		toText.text = (weaponData.Rank + 1).ToString ();
	}

	public void UpdateWeaponInfo(){
		// JIKA SUKSES UPDATE INFO SENJATA DI UNIT DAN FORMATION
		GameData.unitList [GameData.selectedToViewProfileId].Weapon = weaponData; 
		GameData.formationList [GameData.selectedToViewProfileId].Unit.Weapon = weaponData; 

		// update stats unit
		GameData.unitList [GameData.selectedToViewProfileId].SetStats ();
		GameData.formationList [GameData.selectedToViewProfileId].Unit.SetStats (); 
		profileController.SetPictureAndStatsFromFormation ();

		spriteRenderer.sprite = weaponData.Sprites;
		damageText.text = weaponData.Damage.ToString();
		fromText.text = weaponData.Rank.ToString();
		toText.text = (weaponData.Rank + 1).ToString ();
		strText.text = weaponData.WeaponStats.Str.ToString();
		agiText.text = weaponData.WeaponStats.Agi.ToString ();
		vitText.text = weaponData.WeaponStats.Vit.ToString ();
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

	// START UPGRADE!
	public void StartCrafting(){
		bool success = false;
		float temp = Random.Range (0, 100) - weaponData.SuccessRate;
		if (temp < percentages) {
			success = true;
			weaponData.SuccessRate = 0;
			Gem g = (Gem)slotList[0];
			weaponData.Upgrade(g.Stats);
			UpdateWeaponInfo();
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

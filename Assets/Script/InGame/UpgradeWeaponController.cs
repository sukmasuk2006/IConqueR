using UnityEngine;
using System.Collections.Generic;
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
	public List<Item> queriedList;

	private float totalStr;
	private float totalAgi;
	private float totalVit;
	public GameObject confirmationScreen;
	public GameObject progressbar;
	private int upgradedSlot;
	private List<Item> slotList;
	private float percentages;
	public TextMesh fromText;
	public TextMesh upgradeInfoText;
	public TextMesh toText;
	private bool success;
	public ParticleSystem particleSystem;
	public TutorialPrefabList tutorialObject;

	void Start () {
		success = false;
		tutorialObject = GameObject.Find ("TutorialController").GetComponent<TutorialPrefabList>();

		InitilaizeSlot ();
	}

	private void InitilaizeSlot(){

		slotList = null;
		slotList = new List<Item> (4);
		slotList.Add (new Item (99,""));
		slotList.Add (new Item (99,""));
		slotList.Add (new Item (99,""));
		slotList.Add (new Item (99,""));
		
	}

	public void InitializeWeapon(){
		weaponData = GameData.profile.unitList [GameData.selectedToViewProfileId].Weapon;
		spriteRenderer.sprite = GameData.weaponSpriteList [GameData.selectedToViewProfileId];
		strText.text = weaponData.WeaponStats.Str + "";
		agiText.text = weaponData.WeaponStats.Agi + "";
		vitText.text = weaponData.WeaponStats.Vit +"";
		//spriteRenderer.sprite = GameData.weaponSpriteList[weaponData.Id];
		damageText.text = weaponData.Damage.ToString();
		fromText.text = weaponData.Rank.ToString();
		toText.text = (weaponData.Rank + 1).ToString ();
	}

	public void UpdateWeaponInfo(){
		// JIKA SUKSES UPDATE INFO SENJATA DI UNIT DAN FORMATION
		GameData.profile.unitList [GameData.selectedToViewProfileId].Weapon = weaponData; 
		GameData.profile.formationList [GameData.selectedToViewProfileIdFromFormation].Unit.Weapon = weaponData; 

		// update stats unit
		GameData.profile.unitList [GameData.selectedToViewProfileId].SetStats ();
		GameData.profile.formationList [GameData.selectedToViewProfileIdFromFormation].Unit.SetStats (); 
		profileController.SetPictureAndStatsFromFormation ();

		spriteRenderer.sprite = GameData.weaponSpriteList[weaponData.Id];
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
//			Debug.Log("A SPRITE");
			// temp slot yang diupgrade, udah dipasang/diremove gemnya
			Item i = slotList[upgradedSlot];

			// pasang/remove gambar
			if ( i is Gem )
				upgradeSlot [upgradedSlot].sprite = GameData.gemSpriteList[slot];
			else
				upgradeSlot [upgradedSlot].sprite = GameData.catalystSpriteList[slot];

			Debug.Log(" slot " + upgradedSlot + " idnya  " + i.Id);
			// jika id 99
			if ( i.Id == 99 )
				upgradeSlot [upgradedSlot].sprite = null;
		}
		catch {
			Debug.Log("gk ada SPRITE");
			upgradeSlot [slot].sprite = null;
		}
	}

	public void UpdateSemuaGambarDiInventory(){
		for ( int i = 0 ; i < selectItemButton.Count ; i++ )
			selectItemButton[i].UpdateSlotForUpgrade();
	}

	// jika sudah ada gem/catalyst terpasang, di klik akan balek ke invent
	public void RemoveASlot(int i){
		if (slotList [i] is Gem || slotList [i] is Catalyst)
			GameData.profile.inventoryList.Add (slotList [i]);
		slotList [i]  = new Item(99,"");
		UpdateSlot (i);
	}

	public void RemoveSlot(){
		for (int i = 0; i < slotList.Count; i++) {
						if (slotList [i] is Gem || slotList [i] is Catalyst)
				GameData.profile.inventoryList.Add (slotList [i]);
			upgradeSlot [i].sprite = null;
		}

		slotList.RemoveRange(0,4);
		InitilaizeSlot ();
	}

	public void AfterUpgradeAttempt(){
		percentages = 0;
		for (int i = 0; i < slotList.Count; i++) {
			upgradeSlot [i].sprite = null;
		}
		slotList.RemoveRange(0,4);
		InitilaizeSlot ();
	}

	// START UPGRADE!
	public void StartCrafting(){
		GameData.readyToTween = false;
		int chances = 0;
		int penentu = 0;
		float failpercentages;
		progressbar.transform.localScale = new Vector3 (0f, progressbar.transform.localScale.y,
		                                               progressbar.transform.localScale.z);

		Debug.Log (" process upgrade " + GameData.readyToTween);
		for (int i = 0; i < 100; i++) {
			penentu = Random.Range(0,100);
			chances = penentu % 2 == 1 ? chances+1 : chances;
		}
		failpercentages = 100 - percentages;
		Debug.Log ("chances " + chances + " fail " + failpercentages);
		success = chances > failpercentages ? true : false;

		iTween.ScaleTo (progressbar, iTween.Hash("scale", new Vector3(1.5f,progressbar.transform.localScale.y,
		                                                              progressbar.transform.localScale.z),
		                                    "time", 5f,
		                                    "easetype","linear","oncomplete","ReadyTween","oncompletetarget",gameObject));
		// misal dapat 100-succes rate 10 => 90 , semakin kecil, kemungkinan naik semakin besar
		//float temp = Random.Range (0, 100) - weaponData.SuccessRate;
		//Debug.Log ("persentase " + temp + " " + percentages);
	

	}

	void ReadyTween(){
		if (success) {
			//		success = true;
			weaponData.SuccessRate = 0;
			Gem g = (Gem)slotList [0];
			weaponData.Upgrade (g.Stats);
			upgradeInfoText.text = "SUCCESSFULLY UPGRADED!";
			particleSystem.Emit(weaponData.Rank*10);
		} else {
			/// jika gagal naik dong persentasinya
			/// 
			upgradeInfoText.text = "FAILED!";
			weaponData.SuccessRate ++;		
		}
		/**
tutorial
		 */
		if ( GameData.profile.TutorialState > 14 && GameData.profile.TutorialState <= GameConstant.TOTAL_TUTORIAL )
			tutorialObject.DestoryPrefab ();
		/**/

	    UpdateWeaponInfo ();
		GameData.readyToTween = true;
		Debug.Log (" habis upgrade " + GameData.readyToTween);

		// hilanghkan semua gem+catalyst
		AfterUpgradeAttempt();
		GameData.gameState = "Upgrade";
		GameData.SaveData ();
		GameData.readyToTween = true;
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

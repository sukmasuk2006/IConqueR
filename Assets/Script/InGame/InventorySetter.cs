using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// DI SLOT CHOOSE GEM/CATALYST
public class InventorySetter : MonoBehaviour {

	public UpgradeWeaponController controller;
	public ScreenData data;
	public SpriteRenderer spriteRenderer;
	public int slot;
	public TextMesh nameTxt;
	public TextMesh grade;
	public TextMesh str;
	public TextMesh agi;
	public TextMesh vit;
	public TextMesh price;
	private string path = "";
	public GameObject selectButton;
	private Color defaultColor;
	private bool tes;
	private Sprite s;

	// Use this for initialization
	void Start () {
		UpdateSlotForSell ();
		defaultColor = grade.color;
	}

	public void UpdateSlotForUpgrade(){
		data.maxCorridorState = GameData.profile.inventoryList.Count / 4;
		if (GameData.profile.inventoryList.Count % 4 == 0)
			data.maxCorridorState--;
		s = null;
		//CheckButton ();
		tes = controller.UpgradedSlot == 0 ? true : false;
		if (tes) {
//			Debug.Log("upgrade slot ke " + controller.upgradeSlot + " cou " + GameData.profile.inventoryList.Count);
			controller.queriedList = GameData.profile.inventoryList.Where (x => x is Gem).ToList();	
			UpdateGem(controller.queriedList);
				} else {
			controller.queriedList = GameData.profile.inventoryList.Where (x => x is Catalyst).ToList();
			UpdateCatalyst(controller.queriedList);
				}
		spriteRenderer.sprite = s;
	}

	public void UpdateSlotForSell(){
		data.maxCorridorState = GameData.profile.inventoryList.Count / 4;
		if (GameData.profile.inventoryList.Count % 4 == 0)
			data.maxCorridorState--;
		try {
		Item i = GameData.profile.inventoryList [(4 * data.corridorState) + slot];
		tes = i is Gem ? true : false;
//			Debug.Log("masuk try");	
		if (tes)
				UpdateGem ( GameData.profile.inventoryList);
			else
				UpdateCatalyst ( GameData.profile.inventoryList);
		}
		catch{
			s = null;
			nameTxt.text = "";
			grade.text = "";
			str.text = "";
			agi.text = "";
			vit.text = "";
			price.text = ""; 
//			Debug.Log("masuk catch");	
		}
		CheckButtonForSell ();
		spriteRenderer.sprite = s;
	}

	private void UpdateGem(List<Item> item){
	//	Debug.Log("CARI GEM");
		try {
			Gem g = (Gem)item[(4 * data.corridorState) + slot];
			s = GameData.gemSpriteList[g.Id];
			nameTxt.text = g.Name; 
			grade.text = g.Grade;
			grade.color = GetColor(g.Grade);
			str.text =  g.Stats.Str.ToString();
			agi.text =  g.Stats.Agi.ToString();
			vit.text =  g.Stats.Vit.ToString();
			price.text = "";
		}
		catch{
//			Debug.Log("GAK NEMU GEM");
			s = null;
			nameTxt.text = "";
			grade.text = "";
			str.text = "";
			agi.text = "";
			vit.text = "";
			price.text = ""; 
		}
	}

	private void UpdateCatalyst(List<Item> item){
		Debug.Log("CARI CATALYST");
		try {
			Catalyst g = (Catalyst)item[(4 * data.corridorState) + slot];
			s = GameData.catalystSpriteList[g.Id];
			nameTxt.text = g.Name; 
			grade.text = g.Desc;
			grade.color = GetColor(g.Name);
			str.text = "Increase upgrade";
			agi.text = "success rate";
			vit.text = "by " + g.SuccessRate.ToString();
			price.text = ""; 
		}
		catch{
			Debug.Log("GAK NEMU CATALYUST");
			s = null;
			nameTxt.text = "";
			grade.text = "";
			str.text = "";
			agi.text = "";
			vit.text = "";
			price.text = ""; 
		}
	}

	public void CheckButtonForUpgrade(){
		selectButton.SetActive (false);
		try {// slot 0=> gem atau slot 1,2,3 => catalyst
			Debug.Log("buton try");
			if (controller.UpgradedSlot == 0 && controller.queriedList [(4 * data.corridorState) + slot] is Gem
			    || controller.UpgradedSlot != 0 && controller.queriedList [(4 * data.corridorState) + slot] is Catalyst){
				selectButton.SetActive (true); 
				
			}
		}
		catch{
			Debug.Log("buton catch");
			return;
		}
	}

	public void CheckButtonForSell(){
		selectButton.SetActive (false);
		try {// slot 0=> gem atau slot 1,2,3 => catalyst
			if (GameData.profile.inventoryList [(4 * data.corridorState) + slot] is Gem
			    || GameData.profile.inventoryList [(4 * data.corridorState) + slot] is Catalyst){
				selectButton.SetActive (true); 
			}
		}
		catch{
			return;
		}
	}

	Color GetColor(string tipe){
		Color ret = new Color();
//		Debug.Log ("UPDET COLOR " + tipe);
		
		switch (tipe.Trim()) {
		case "Common" : ret = Color.white;break;//white
		case "Uncommon" : ret = new Color(0,1,1,1);break; //biru muda
		case "Rare" : ret = defaultColor;break; // 
		case "Mythical" : ret = new Color(1,1,0);break;
		case "Legendary" : ret = Color.green;break;//new Color(19,255,0);break;		
		}
		
		return ret;
	}
}

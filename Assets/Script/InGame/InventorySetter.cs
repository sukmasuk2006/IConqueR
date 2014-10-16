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

	// Use this for initialization
	void Start () {
		UpdateSlot ();
		defaultColor = grade.color;
	}

	void Compare(){
	
	}

	public void UpdateSlot(){
		data.maxCorridorState = GameData.profile.inventoryList.Count / 4;
		if (GameData.profile.inventoryList.Count % 4 == 0)
			data.maxCorridorState--;
		Sprite s = null;
		//CheckButton ();
		bool tes = controller.UpgradedSlot == 0 ? true : false;
		if (tes) {
			Debug.Log("CARI GEM");
				controller.queriedList = GameData.profile.inventoryList.Where (x => x is Gem).ToList();	
					try {
						Gem g = (Gem)controller.queriedList[(4 * data.corridorState)+slot];
						s = GameData.gemSpriteList[g.Id];
						nameTxt.text = g.Name; 
						grade.text = g.Grade;
						grade.color = GetColor(g.Grade);
						str.text = "Str + " + g.Stats.Str.ToString();
						agi.text = "Agi + " + g.Stats.Agi.ToString();
						vit.text = "Vit + " + g.Stats.Vit.ToString();
						price.text = "";
					}
					catch{
				Debug.Log("GAK NEMU GEM");
				s = null;
						nameTxt.text = "";
						grade.text = "";
						str.text = "";
						agi.text = "";
						vit.text = "";
						price.text = ""; 
					}
				} else {
			Debug.Log("CARI CATALYST");
			controller.queriedList = GameData.profile.inventoryList.Where (x => x is Catalyst).ToList();
						try {
				Catalyst g = (Catalyst)controller.queriedList[(4 * data.corridorState)+slot];
							s = GameData.catalystSpriteList[g.Id];
							nameTxt.text = g.Desc; 
							grade.text = g.Name;
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
		spriteRenderer.sprite = s;
	}


	public void CheckButton(){
		selectButton.SetActive (false);
		try {// slot 0=> gem atau slot 1,2,3 => catalyst
			if (controller.UpgradedSlot == 0 && controller.queriedList [(4 * data.corridorState) + slot] is Gem
			    || controller.UpgradedSlot != 0 && controller.queriedList [(4 * data.corridorState) + slot] is Catalyst){
					selectButton.SetActive (true); 
			}
		}
		catch{
			return;
		}
	}

	Color GetColor(string tipe){
		Color ret = new Color();
		Debug.Log ("UPDET COLOR " + tipe);
		
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

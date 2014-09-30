using UnityEngine;
using System.Collections;
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

	// Use this for initialization
	void Start () {
		UpdateSlot ();
	}

	public void UpdateSlot(){
		data.maxCorridorState = GameData.inventoryList.Count / 4;
		if (GameData.inventoryList.Count % 4 == 0)
			data.maxCorridorState--;
		Sprite s = null;
		//CheckButton ();
		try {
				if ( GameData.inventoryList[(4 * data.corridorState)+slot] is Gem ){
					Gem g = (Gem)GameData.inventoryList[(4 * data.corridorState)+slot];
					s = g.Sprites;
					nameTxt.text = g.Name; 
					grade.text = g.Grade;
					str.text = "Str + " + g.Stats.Str.ToString();
					agi.text = "Agi + " + g.Stats.Agi.ToString();
					vit.text = "Vit + " + g.Stats.Vit.ToString();
					price.text = "";
				}
				else{
					Catalyst g = (Catalyst)GameData.inventoryList[(4 * data.corridorState)+slot];
					s = g.Sprites;
					nameTxt.text = g.Desc; 
					grade.text = g.Name;
					str.text = "Increase upgrade";
					agi.text = "success rate";
					vit.text = "by " + g.SuccessRate.ToString();
					price.text = ""; 
				}
		}
		catch{
			nameTxt.text = "";
			grade.text = "";
			str.text = "";
			agi.text = "";
			vit.text = "";
			price.text = ""; 
		}
		spriteRenderer.sprite = s;
	}


	public void CheckButton(){
		Debug.Log("Cek buton");
		selectButton.SetActive (false);
		try {// slot 0=> gem atau slot 1,2,3 => catalyst
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

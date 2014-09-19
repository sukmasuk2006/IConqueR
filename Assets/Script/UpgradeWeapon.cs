using UnityEngine;
using System.Collections;

// DI TOMBOL UPGRADE 
public class UpgradeWeapon : MonoBehaviour {

	//public 
	public UpgradeWeaponController controller;
	private float percentages;
	public GameObject confirmScreen;
	public TextMesh amount;
	public TextMesh info;
	private Gem gem;
	// Use this for initialization
	void Start () {
			
	}
	
	void OnMouseDown(){
		// cek apakah slot 0 itu gem kagak
		if (controller.SlotList [0] is Gem) {
			gem = (Gem)controller.SlotList[0];
			CheckRequirement();
		}else
			info.text = "Please insert a Gemstone to upgrade";
	}

	void CheckRequirement(){
		Weapon w = controller.WeaponData;
		if (w.Rank < 10) {
			bool rankreq = w.CheckUpgradeReq(gem.Grade);
			if ( rankreq ){
				CountingPercentages();
			}
			else
				info.text = "Use suitable gem to upgrade!";
		}
		else
			info.text = "Cannot upgrade more than +10";
	}

	void CountingPercentages(){
		Gem g = (Gem)controller.SlotList [0];
		percentages += g.SuccessRate;
		for (int i = 1; i < controller.SlotList.Count; i++) {
			if ( controller.SlotList[i] is Catalyst ){
				Catalyst c = (Catalyst)controller.SlotList[i];
				percentages += c.SuccessRate;
			}
		}
		if (percentages >= 100)
						percentages = 100;
		amount.text = percentages.ToString ();
		if ( GameData.readyToTween )
			controller.Percentages = percentages;
			iTween.MoveTo (confirmScreen,new Vector3(0f,0f,confirmScreen.transform.position.z),0.1f);
	}
}

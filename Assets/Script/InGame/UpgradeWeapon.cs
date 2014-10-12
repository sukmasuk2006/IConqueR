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
	public AudioClip sound;
	// Use this for initialization
	void Start () {
		percentages = 0;
	}
	
	void OnMouseDown(){
		CheckRequirement ();
				MusicManager.getMusicEmitter().audio.PlayOneShot(sound);

	}

	void CheckRequirement(){
		
		// cek apakah slot 0 itu gem kagak
		Weapon w = controller.WeaponData;
		
		if (w.Rank < 10) {
			// check dulu + brp 
			int minLevelReq = (w.Rank + 1) * 2;
			Unit u = GameData.profile.formationList [GameData.selectedToViewProfileIdFromFormation].Unit;
			if (u.Level >= minLevelReq) {
				if (controller.SlotList [0] is Gem) {
					gem = (Gem)controller.SlotList [0];
					// min senjata level = 2 x unit level
					bool rankreq = w.CheckUpgradeReq(gem.Grade);
					// kalau udah bisa up, cek gemnya bener nggak
					if ( rankreq ){
						CountingPercentages();
					}
					else
						info.text = "Use suitable gem to upgrade!";
					
				} else 
					info.text = "Please insert a Gemstone to upgrade";
			}
			else{
				info.text = GameData.selectedToViewProfileName +" at least level "+ minLevelReq +" to upgrade.";
			}
		}
		else
			info.text = "Cannot upgrade more than +10";
	}

	void CountingPercentages(){
		Gem g = (Gem)controller.SlotList [0];
		percentages = 0;
		Debug.Log("gem, persenet awal + " + percentages + " gem rate " + g.SuccessRate);
		percentages += g.SuccessRate;
		for (int i = 1; i < controller.SlotList.Count; i++) {
			if ( controller.SlotList[i] is Catalyst ){
				Catalyst c = (Catalyst)controller.SlotList[i];
				percentages += c.SuccessRate;
				Debug.Log("ada catalyst, persent + " + percentages);
			}
		}
		if (percentages >= 100)
						percentages = 100;
		amount.text = percentages.ToString ();
		if ( GameData.readyToTween )
			controller.Percentages = percentages;
		GameData.gameState = "confirm";
		iTween.MoveTo (confirmScreen,new Vector3(0f,0f,confirmScreen.transform.position.z),0.1f);
	}
}

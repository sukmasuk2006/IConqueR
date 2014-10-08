using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProfileController : MonoBehaviour {

	// Use this for initialization
	
	public TextMesh levelText;
	public TextMesh goldText;
	public TextMesh diamondText;
	public Transform expBar;
	private float scaleAwal = 1.1f;
	private float scale = 0f;
	private float expTujuan;

	void Start () {
		//Debug.Log ("profile contr current gold " + GameData.gold);
		levelText.text = "Level " + GameData.profile.Level.ToString ();
		UpdateGoldAndDiamond (0,0);
		expBar.localScale = new Vector3 (scaleAwal * GameData.profile.CurrentExp / GameData.profile.NextExp
		                                 , expBar.localScale.y,
		                                expBar.localScale.z);
//		Debug.Log ("MUSIC PLAYED " + MusicManager.getMusicPlayer ().audio.clip.name);
		if ( MusicManager.getMusicPlayer().audio.clip.name != "royal")
			MusicManager.play ("Music/royal");
	}

	public void UpdateGoldAndDiamond(int type, int money){
		if (type == 0)
			GameData.profile.Gold -= money;
		else
			GameData.profile.Diamond -= money;


		diamondText.text = GameData.profile.Diamond.ToString ();
		goldText.text = GameData.profile.Gold.ToString ();
		SaveLoad.Save ();
	}

	public int GetMoneyValue(int type){
		int money = 0;
		if (type == 0)
			money = GameData.profile.Gold;
		else
			money = GameData.profile.Diamond;
		return money;
	}

}

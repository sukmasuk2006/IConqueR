using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ProfileController : MonoBehaviour {

	// Use this for initialization
	
	public TextMesh levelText;
	public TextMesh titleText;
	public TextMesh goldText;
	public TextMesh diamondText;
	public Transform expBar;
	public SpriteRenderer renderer;
	private float scaleAwal = 1.1f;
	private float scale = 0f;
	private float expTujuan;


	void Start () {
		GameData.gameState = "Map";
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
		//SaveLoad.Save ();
		var m = GameData.profile.questList.Where (x => x.Target.Contains ("gold")).ToList ();
		foreach (Quest q in m)
						q.CurrentQuantity = GameData.profile.Gold;
		SetTitle ();
		GameData.SaveData ();
	}

	void SetTitle ()
	{
		int i = GameData.profile.Title;
		string teks = "";
		switch (i) {
		case 0 : teks = "Soldier"; break;
		case 1 : teks = "General"; break;
		case 2 : teks = "Commander"; break;
		case 3 : teks = "Baron"; break;
		case 4 : teks = "King"; break;
		case 5 : teks = "Hero"; break;		
		}
		titleText.text = teks;
		renderer.sprite = GameData.titleSpriteList [i];
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

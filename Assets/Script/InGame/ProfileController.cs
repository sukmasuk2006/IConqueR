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
	private float scaleAwal = 1f;
	private float expTujuan;
	public GameObject confirmExitScreen;
	public SpriteRenderer questShade;

	void Start () {
		if (GameData.profile.TutorialState > GameConstant.TOTAL_TUTORIAL)
						GameData.gameState = "Map";
				else
						GameData.gameState = "Tutorial";
		//Debug.Log ("profile contr current gold " + GameData.gold);
		levelText.text = "Level " + GameData.profile.Level.ToString ();
		UpdateGoldAndDiamond (0,0);
		Debug.Log ("Profile, state " + GameData.gameState);
		//Debug.Log ("awal profile " + scaleAwal * GameData.profile.CurrentExp / GameData.profile.NextExp);
		expBar.localScale = new Vector3 (scaleAwal * GameData.profile.CurrentExp / GameData.profile.NextExp
		                                 , expBar.localScale.y,
		                                expBar.localScale.z);
//		Debug.Log ("MUSIC PLAYED " + MusicManager.getMusicPlayer ().audio.clip.name);
		if ( MusicManager.getMusicPlayer().audio.clip.name != "royal")
			MusicManager.play ("Music/royal");
	}

	public void CheckIsCompletedAchievement(){
		int cek = GameData.profile.questList.Where( x => !x.IsRewardTaken && x.IsCompleted ).ToList().Count;
		questShade.enabled =  cek > 0 ? true : false;
	}

	public void UpdateGoldAndDiamond(int type, int money){
		if (type == 0)
			GameData.profile.Gold -= money;
		else
			GameData.profile.Diamond -= money;


		diamondText.text = GameData.profile.Diamond.ToString ();
		goldText.text = GameData.profile.Gold.ToString ();
		//SaveLoad.Save ();
		questShade.enabled =  GameData.UpdateGoldQuest ();
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

	void Update(){
		if (GameData.profile.TutorialState > GameConstant.TOTAL_TUTORIAL) {
			if (Input.GetKeyDown (KeyCode.Escape) && GameData.gameState != "ConfirmExit") {
					iTween.MoveTo (confirmExitScreen, iTween.Hash ("position", new Vector3 (0f, 0f, -7f), "time", 0.1f, "onComplete", "ReadyTween", "onCompleteTarget", gameObject));
					//sound.audio.PlayOneShot (sound.audio.clip);
					GameData.prevGameState = GameData.gameState; // backup state
					GameData.gameState = "ConfirmExit";	
					GameData.readyToTween = false;	
			} else if (Input.GetKeyDown (KeyCode.Escape) && GameData.readyToTween 
					&& GameData.gameState == "ConfirmExit") {
					iTween.MoveTo (confirmExitScreen, iTween.Hash ("position", new Vector3 (0f, -12f, -7f), "time", 0.1f, "onComplete", "ReadyTween", "onCompleteTarget", gameObject));
					GameData.gameState = GameData.prevGameState;	
			}
		}
	}

	void ReadyTween(){
		GameData.readyToTween = true;
	}

}

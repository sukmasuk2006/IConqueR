using UnityEngine;
using System.Collections.Generic;

public class GameData : MonoBehaviour {

	// HERO STATS
	public static int currentLevel = 1;
	public static float currentExp = 0;
	public static float nextExp = 10;
	public static int gold = 0;
	public static int diamond = 0;
	public static string name = "";

	public static List<Item> inventoryList;
	public static List<string> heroesList;

	// GAME STATS
	public static bool isFirstPlay = true;
	public static float gameSpeed = 1f;
	public static string gameState = "LOGO";
	public static float raidTime = 60;
	public static int unlockedHeroes = 1;
	public static int unlockHeroCost = 1000;
	public static int unlockSkillCost = 1000;
	public static int selectedToViewProfileId = 5;
	public static string selectedToViewProfileName = "";
	private string[] linesFromFile;
	
	public static List<Unit> unitList;
	public static List<Unit> enemyList;
	public static List<Skill> skillList;

	// Use this for initialization
	void Start () {

	}

	void Awake(){
		LoadData ();
		InitializeGameData ();
	}

	void LoadData(){
		if (PlayerPrefs.HasKey ("name")) {
			name = PlayerPrefs.GetString ("name");
			isFirstPlay = false;
			Debug.Log("nama " + name + " is first " + isFirstPlay);
		} 
		else {
			isFirstPlay = true;				
			Debug.Log("first play");
		}
		if (PlayerPrefs.HasKey ("level")) {
			currentLevel = PlayerPrefs.GetInt ("level");
			Debug.Log("level diatas 1");
		} 
		if (PlayerPrefs.HasKey ("exp")) {
			currentExp = PlayerPrefs.GetFloat ("exp");
			Debug.Log("ada exp");
		} 
		/*
			next exp = load dari data di notepad
		 */

		if (PlayerPrefs.HasKey ("gold")) {
			diamond = PlayerPrefs.GetInt("gold");	
		}
		if (PlayerPrefs.HasKey ("diamond")) {
			diamond = PlayerPrefs.GetInt("diamond");	
		}


	}

	void InitializeGameData(){
		/*INIT*/
		GameData.unlockHeroCost = GameConstant.BASE_PRICE * GameData.unlockedHeroes * 2;

		/*HERO DATA*/
		unitList = new List<Unit> ();
		TextAsset txt = (TextAsset)Resources.Load ("Data/Unit/hero", typeof(TextAsset));
		string content = txt.text;
		linesFromFile = content.Split ("\n" [0]);
		
		for (int i = 0; i < 5; i++) {
			unitList.Add(new Unit(i,"","","",linesFromFile[i][0] - '0',linesFromFile[i][1] - '0' ,linesFromFile[i][2] - '0'));		
		}
		unitList [4].IsUnlocked = true;

		/*ENEMY DATA*/
		enemyList = new List<Unit> ();

		for (int i = 4; i >= 0; i--) {
			enemyList.Add(new Unit(i,"","","",linesFromFile[i][0] - '0',linesFromFile[i][1] - '0' ,linesFromFile[i][2] - '0'));		
		}
		enemyList [4].IsUnlocked = true;

		/*SKILL DATA*/
		linesFromFile = null;
		skillList = new List<Skill> ();
		TextAsset skillTxt = (TextAsset)Resources.Load ("Data/Skill/list", typeof(TextAsset));
		string skillContent = skillTxt.text;
		linesFromFile = skillContent.Split ("\n"[0]);
		for (int i = 0; i < linesFromFile.Length; i++) {
			Debug.Log ("len " + linesFromFile[i]);
			skillList.Add(new Skill(1,linesFromFile[i]));		
		}
		skillList [0].IsUnlocked = true;

		// TESTING
		gold = 10000;
		//GameData.unlockedHeroes = GameData.unitList.Count;

	}

	public static void UpdateRaidTime(){
		raidTime -= (Time.deltaTime * gameSpeed);
	}
	
	// Update is called once per frame
	void Update () {

	}
}

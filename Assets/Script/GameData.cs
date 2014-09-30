using UnityEngine;
using System.Collections.Generic;

public class GameData : MonoBehaviour {
	
	// HERO STATS

	public static ProfileData profile;

	// GAME STATS
	public static bool isFirstPlay = true;
	public static bool readyToTween = true;
	public static float raidTime = 60;
	public static int unlockedSlot = 1; // slot formasi yang terbuka
	public static int activeHeroes = 1; // hero yang aktif
	public static int unlockSkillCost = 1000;
	public static int selectedToViewProfileId = 0; // hero yg ditampilkan di troopprofile
	public static int totalSkillUsed = 0; // skill yg aktif
	public static string selectedToViewProfileName = "knight"; // nama hero yang ditampilkan
	public static string gameState = "LOGO"; // untuk state dari layar
	public static string missionType = "Fortress";
	// apakah serang fort atau castle, untuk dicek achivementnya

	private string[] linesFromFile;
	
	public static List<Item> shopList;
	public static List<Item> inventoryList;
	public static List<Gem> weaponSlotContentList;
	public static List<Quest> questList;
	public static List<int> expList;
	public static List<Mission> missionList;
	public static List<Unit> unitList;
	public static List<Unit> enemyList;
	public static List<FormationUnit> formationList;
	public static List<Skill> skillList;


	// Use this for initialization
	void Start () {

	}

	void Awake(){
		InitializeGameData ();
		LoadData ();
//		Debug.Log ("Data initialized " + profile.Gold);
	}

	void LoadData(){
		if (PlayerPrefs.HasKey ("name")) {
			profile.Name = PlayerPrefs.GetString ("name");
			isFirstPlay = false;
	//		Debug.Log("nama " + name + " is first " + isFirstPlay);
		} 
		else {
			isFirstPlay = true;				
	//		Debug.Log("first play");
		}
		if (PlayerPrefs.HasKey ("level")) {
			profile.Level = PlayerPrefs.GetInt ("level");
	//		Debug.Log("level diatas 1");
		} 
		if (PlayerPrefs.HasKey ("exp")) {
			profile.CurrentExp = PlayerPrefs.GetInt ("exp");
	//		Debug.Log("ada exp");
		} 
		/*
			next exp = load dari data di notepad
		 */

	//	if (PlayerPrefs.HasKey ("gold")) {
	//		profile.Gold = PlayerPrefs.GetInt("gold");	
	//	}
		if (PlayerPrefs.HasKey ("diamond")) {
			profile.Diamond = PlayerPrefs.GetInt("diamond");	
		}


	}

	void InitializeGameData(){
		/*INIT*/

		expList = new List<int> ();
		TextAsset etxt = (TextAsset)Resources.Load ("Data/Exp/hero", typeof(TextAsset));
		string econtent = etxt.text;
		linesFromFile = econtent.Split ("\n" [0]);
		for (int i = 0; i < linesFromFile.Length; i++) {
			expList.Add(int.Parse(linesFromFile[i]));		
		}
		profile = new ProfileData ();
		profile.NewData ("");
		linesFromFile = null;

		linesFromFile = null;
		missionList = new List<Mission> ();
		TextAsset metxt = (TextAsset)Resources.Load ("Data/Mission/list", typeof(TextAsset));
		string mecontent = metxt.text;
		linesFromFile = mecontent.Split ("\n" [0]);
		for (int i = 0; i < linesFromFile.Length; i++) {
			missionList.Add(new Mission(linesFromFile[i]));		
		}

		
		/*HERO DATA & FORMATIOn*/
		linesFromFile = null;
		unitList = new List<Unit> ();
		formationList = new List<FormationUnit> ();
		TextAsset txt = (TextAsset)Resources.Load ("Data/Unit/list", typeof(TextAsset));
		string content = txt.text;
		linesFromFile = content.Split ("\n" [0]);
		
		for (int i = 0; i < linesFromFile.Length; i++) {
			unitList.Add(new Unit(i,linesFromFile[i].Trim()));		
			if ( i < 5 )
				formationList.Add(new FormationUnit(new Unit(99,linesFromFile[0].Trim())));		
		}
		unitList [0].IsUnlocked = true;
		unitList [0].IsActive = true;
		formationList [0].IsUnlocked = true;
		formationList [0].Unit.HeroId = 0;

		/*ENEMY DATA*/
		//enemyList = new List<Unit> ();

		/*SKILL DATA*/
		linesFromFile = null;
		skillList = new List<Skill> ();
		TextAsset skillTxt = (TextAsset)Resources.Load ("Data/Skill/list", typeof(TextAsset));
		string skillContent = skillTxt.text;
		linesFromFile = skillContent.Split ("\n"[0]);
		for (int i = 0; i < linesFromFile.Length; i++) {
	//		Debug.Log ("len " + linesFromFile[i]);
			skillList.Add(new Skill(1,linesFromFile[i]));		
		}
		skillList [0].IsUnlocked = true;
		skillList [0].IsSelected = true;
		totalSkillUsed = 1;

		/*SHOP*/
		linesFromFile = null;
		shopList = new List<Item> ();
		TextAsset shopTxt = (TextAsset)Resources.Load ("Data/Gem/list", typeof(TextAsset));
		string shopContent = shopTxt.text;
		linesFromFile = shopContent.Split ("\n"[0]);
		for (int i = 0; i < linesFromFile.Length; i++) {
			//		Debug.Log ("len " + linesFromFile[i]);
			shopList.Add(new Gem(linesFromFile[i]));		
		}

		linesFromFile = null;
		TextAsset shop2Txt = (TextAsset)Resources.Load ("Data/Catalyst/list", typeof(TextAsset));
		string shop2Content = shop2Txt.text;
		linesFromFile = shop2Content.Split ("\n"[0]);
		for (int i = 0; i < linesFromFile.Length; i++) {
			//		Debug.Log ("len " + linesFromFile[i]);
			shopList.Add(new Catalyst(linesFromFile[i]));		
		}


		/*INVENTORY*/
		inventoryList = new List<Item> ();

		/*WEAPON SLOT*/
		weaponSlotContentList = new List<Gem> ();

		/*QUEST*/
		linesFromFile = null;
		questList = new List<Quest> ();
		TextAsset questTxt = (TextAsset)Resources.Load ("Data/Quest/list", typeof(TextAsset));
		string questContent = questTxt.text;
		linesFromFile = questContent.Split ("\n"[0]);
		for (int i = 0; i < linesFromFile.Length; i++) {
	//		Debug.Log ("len " + linesFromFile[i]);
			questList.Add(new Quest(linesFromFile[i]));		
		}
		// TESTING
		profile.Gold = 50000;
	//	Debug.Log ("gold skrg " + profile.Gold);
		//GameData.unlockedHeroes = GameData.unitList.Count;

	}

	public static void UpdateRaidTime(){
	//	raidTime -= (Time.deltaTime * gameSpeed);
	}
	
	// Update is called once per frame
	void Update () {

	}
}

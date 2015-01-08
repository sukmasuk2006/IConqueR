 using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameData : MonoBehaviour {
	
	// HERO STATS

	public static ProfileData profile;
	public static int tesId = 0;
	public static bool isDrag = false;

	// GAME STATS
	public static bool isPremium = true;

	public static bool isFirstPlay = true;
	public static bool readyToTween = true;
	public static int selectedToViewProfileId = 0; // hero yg ditampilkan di troopprofile
	public static string selectedToViewProfileName = "knight"; // nama hero yang ditampilkan
	public static int unitSlotYangDiSet = 0; // formasi slot ke berapa
	public static string gameState = "LOGO"; // untuk state dari layar
	public static string prevGameState = "LOGO"; // untuk state dari layar
	public static int currentMission = 0; // cek misi yang dijalankan
	public static string missionType = "Fortress";
	// apakah serang fort atau castle, untuk dicek achivementnya

	private string[] linesFromFile;
	
	public static List<Item> shopList;
	public static List<TutorialText> tutorialTextList;
	public static  List<Mission> missionList;
	public static List<int> expList;
	public static List<Sprite> unitSpriteList;
	public static List<Sprite> unitIconList;
	public static List<Sprite> weaponSpriteList;
	public static List<Sprite> gemSpriteList;
	public static List<Sprite> catalystSpriteList;
	public static List<Sprite> skillSpriteList;
	public static List<Sprite> titleSpriteList;
	public static List<SkeletonDataAsset> skeleteonDataAssetList;

	// Use this for initialization
	void Start () {
		Initialize();
		LoadData();
	}

	void Awake(){
	//	Initialize();
	//	profile.Gold = 150000;
	//	profile.TutorialState = 22;
	//	LoadData ();
		//profile.Gold = 99750;
		//profile.Level = 15;
	}

	void Initialize(){
		InitializePersistent ();
		InitializeGameData ();
		profile.StoryCompleted = true;
	//	Cheat();
	}

	private void Cheat(){
		//PlayerPrefs.DeleteAll ();
		profile.StoryCompleted = true;
		profile.Gold = 500000;
		profile.TutorialState = GameConstant.TOTAL_TUTORIAL + 1;
		profile.Level = 15;
		profile.NextMission = 49;

	}

	public static bool CheckPrefs(string key){
		bool ret = PlayerPrefs.HasKey (key) ? true : false;
		//Debug.Log ("CEK PREFS " + key + " " + ret);
		return ret;
	}

	public static void SaveData(){
		profile.SaveData ();
	}

	public void LoadData(){
		if (PlayerPrefs.HasKey ("level"+GameData.tesId)) {
			profile.LoadData ();
			Debug.Log("LOADED");
		}
		else{
			SaveData();
			Debug.Log("NEW GAME");
		}
		if ( GameData.profile.TutorialState < GameConstant.TOTAL_TUTORIAL ){
			profile = null;
			Initialize();
			SaveData();	Debug.Log("TUTORIAL NOT DONE");
		}
	}

	void InitializePersistent(){
		profile = new ProfileData ();

		expList = new List<int> ();
		TextAsset etxt = (TextAsset)Resources.Load ("Data/Exp/hero", typeof(TextAsset));
		string econtent = etxt.text;
		linesFromFile = econtent.Split ("\n" [0]);
		for (int i = 0; i < linesFromFile.Length; i++) {
			expList.Add(int.Parse(linesFromFile[i]));		
		}
			linesFromFile = null;

		/*SHOP*/
		linesFromFile = null;
		shopList = new List<Item> ();
		gemSpriteList = new List<Sprite> ();
		for (int i = 0; i < 36; i++) {
			//		Debug.Log ("len " + linesFromFile[i]);
			shopList.Add(new Gem(i));
			Gem g = (Gem)shopList[i];
			gemSpriteList.Add(LoadGemSprite(g.Grade.Trim()+"/"+g.Name.Trim()));
		}

//		Debug.Log ("Jumlah gambar gem " + gemSpriteList.Count);
		linesFromFile = null;
		TextAsset shop2Txt = (TextAsset)Resources.Load ("Data/Catalyst/list", typeof(TextAsset));
		string shop2Content = shop2Txt.text;
		linesFromFile = shop2Content.Split ("\n"[0]);
		catalystSpriteList = new List<Sprite> ();
		for (int i = 0; i < linesFromFile.Length; i++) {
			//		Debug.Log ("len " + linesFromFile[i]);
			shopList.Add(new Catalyst(i,linesFromFile[i]));		
			catalystSpriteList.Add(LoadCatalystSprite(linesFromFile[i].Trim()));
		}

		// sprite, icon, and skeleton, weapon
		//UNIT
		linesFromFile = null;
		TextAsset txt = (TextAsset)Resources.Load ("Data/Unit/list", typeof(TextAsset));
		string content = txt.text;
		linesFromFile = content.Split ("\n" [0]);
		unitSpriteList = new List<Sprite> ();
		unitIconList = new List<Sprite> ();
		skeleteonDataAssetList = new List<SkeletonDataAsset> ();
		weaponSpriteList = new List<Sprite> ();
		for (int i = 0; i < linesFromFile.Length; i++) {
			unitSpriteList.Add(LoadCharacterSprite(linesFromFile[i].Trim()));
			profile.unitList.Add(new Unit(i,linesFromFile[i].Trim()));	
			unitIconList.Add(LoadIconList(linesFromFile[i].Trim()));
			skeleteonDataAssetList.Add(LoadSkeleton(linesFromFile[i].Trim()));
			weaponSpriteList.Add(LoadWeaponSprite(linesFromFile[i].Trim()));
			if ( i < 5 )
				profile.formationList.Add(new FormationUnit(99,new Unit(i,linesFromFile[0].Trim())));	
			// buat list kayak unit cuman, idnya 99 semua
		}
		profile.unitList [0].IsUnlocked = true;
		profile.unitList [0].IsActive = true;
		profile.formationList [0].IsUnlocked = true;
		profile.formationList [1].IsUnlocked = true;
		profile.formationList[0].UnitHeroId = 0;
//		Debug.Log ("JUm sprite char " + unitSpriteList.Count);

		/*QUEST*/
		titleSpriteList = new List<Sprite> ();
		linesFromFile = null;
		TextAsset questTxt = (TextAsset)Resources.Load ("Data/Quest/list", typeof(TextAsset));
		string questContent = questTxt.text;
		linesFromFile = questContent.Split ("\n"[0]);
		for (int i = 0; i < linesFromFile.Length; i++) {
			//		Debug.Log ("len " + linesFromFile[i]);
			profile.questList.Add(new Quest(i,linesFromFile[i]));		
			if ( i < 6 ) // 6 jumlah titel
				titleSpriteList.Add(LoadTitleSprite(i));
		}

		skillSpriteList = new List<Sprite> ();
		for (int i = 1; i <= 10; i++) {
			skillSpriteList.Add(LoadSkillSprite(i));
		}

	}

	private Sprite LoadTitleSprite(int id){
		Sprite sprites = null;
		//		Debug.Log ("load sprite " + name);
		sprites = (Sprite)Resources.Load ("Sprite/Title/" + id, typeof(Sprite));
		return sprites;
	}

	private Sprite LoadCharacterSprite(string name){
		Sprite sprites = null;
		sprites = (Sprite)Resources.Load ("Sprite/Character/Hero/" + name, typeof(Sprite));
		return sprites;
	}

	private Sprite LoadSkillSprite(int id){
		Sprite sprites = null;
		sprites = (Sprite)Resources.Load ("Sprite/Skill/skill_" + id, typeof(Sprite));
		return sprites;
	}
	
	private Sprite LoadIconList(string name){
		Sprite sprites = null;
		sprites = (Sprite)Resources.Load ("Sprite/Character Icon/" + name, typeof(Sprite));
		return sprites;
	}

	private SkeletonDataAsset LoadSkeleton(string name){
		SkeletonDataAsset playerData;
		playerData = ScriptableObject.CreateInstance<SkeletonDataAsset> ();
		playerData = (SkeletonDataAsset)Resources.Load ("Sprite/Character/"+name+"/mySkeldata",typeof(SkeletonDataAsset));
		
		playerData.scale = 0.006f;
		playerData.defaultMix = 0f;
		return playerData;
	}
	
	private Sprite LoadWeaponSprite(string name){
		Sprite sprites = null;
		sprites = (Sprite)Resources.Load ("Sprite/Weapon/" + name, typeof(Sprite));
		return sprites;
	}

	private Sprite LoadGemSprite(string name){
		Sprite sprites = null;
		sprites = (Sprite)Resources.Load ("Sprite/Gems/"+name.Trim(), typeof(Sprite));
		return sprites;
	}

	private Sprite LoadCatalystSprite(string name){
		Sprite sprites = null;
		sprites = (Sprite)Resources.Load ("Sprite/Catalyst/" + name, typeof(Sprite));
		return sprites;
	}

	/*void LoadData(){
		SaveLoad.Load ();
	
		try {
			Debug.Log ("ARMY " +GameData.profile.DefeatedArmy);
		}
		catch{
			Debug.Log ("FAIL TO LOAD");
		}
	}*/

	void InitializeGameData(){
		/*INIT*/
		linesFromFile = null;
		missionList = new List<Mission> ();
		for (int i = 0; i < GameConstant.MISSION_LIST; i++) {
			missionList.Add(new Mission(i));		
		}
		/*SKILL DATA*/
		linesFromFile = null;
		TextAsset skillTxt = (TextAsset)Resources.Load ("Data/Skill/list", typeof(TextAsset));
		string skillContent = skillTxt.text;
		linesFromFile = skillContent.Split ("\n"[0]);
		for (int i = 0; i < linesFromFile.Length; i++) {
			profile.skillList.Add(new Skill(i,linesFromFile[i]));	
		}
		profile.skillList [0].IsUnlocked = true;
		profile.skillList [0].IsSelected = true;
		profile.skillList [0].Level = 1;
		profile.totalSkillUsed = 1;
//		Debug.Log ("first play ");
		//SaveLoad.Save ();
	}

	public static bool UpdateGoldQuest(){
		bool ret = false;
		var m = profile.questList.Where (x => x.Target.Contains ("gold") && !x.IsRewardTaken ).ToList ();
		foreach (Quest q in m){
			q.CurrentQuantity = profile.Gold;
			if ( q.IsCompleted  ) ret = true;
		}
		if ( !GameData.gameState.Contains("Sell"))
			ret = false;
		return ret;
	}
}

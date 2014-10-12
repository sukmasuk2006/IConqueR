using UnityEngine;
using System.Collections.Generic;

public class ProfileData 
{
	private string name;
	private int level;
	private int nextMission;
	private int currentExp;
	private int nextExp;
	private int gold;
	private int diamond;
	//private int nextStage;

	// untuk quest
	private int defeatedArmy;
	private int fortressDestroyed;
	private int castleDestroyed;
	private int unlockedTroop;
	private int mapPosition;

	// gameplay
	public int unlockedSlot; // slot formasi yang terbuka
	public int activeHeroes; // hero yang aktif
	public int totalSkillUsed; // skill yg aktif
	public  List<FormationUnit> formationList; // disave isunlockednya
	public  List<Skill> skillList;
	public  List<Item> inventoryList; // save idnya aja
	private int totalInvent;
	public  List<Quest> questList;
	public  List<Unit> unitList;

	private const int base_exp = 75;
	public ProfileData(){

	}

	public void SaveNonList(){
		PlayerPrefs.SetInt ("level", level);
		PlayerPrefs.SetInt("nextMission",nextMission);
		PlayerPrefs.SetInt("currentExp", currentExp);
		PlayerPrefs.SetInt("gold", gold);
		PlayerPrefs.SetInt("diamond", diamond);
		PlayerPrefs.SetInt("defeatedArmy", defeatedArmy);
		PlayerPrefs.SetInt("fortressDestroyed", fortressDestroyed);
		PlayerPrefs.SetInt("castleDestroyed", castleDestroyed);
		PlayerPrefs.SetInt("unlockedTroop",  unlockedTroop);
		PlayerPrefs.SetInt("mapPosition", mapPosition);
		PlayerPrefs.SetInt("unlockedSlot", unlockedSlot);
		PlayerPrefs.SetInt("activeHeroes",  activeHeroes);
		PlayerPrefs.SetInt("totalSkillUsed", totalSkillUsed);
		Debug.Log ("save unlocked slot " + unlockedSlot);
		Debug.Log ("save activeheroes " + activeHeroes);
		for (int q = 0; q < 5; q++) {
			formationList[q].Save(q);		
		}
		for (int i = 0; i < 10; i ++) 
			unitList[i].Save();		
			
		for ( int j = 0 ; j < skillList.Count ;j++){
				skillList[j].Save();
		}
		for ( int k = 0 ; k < questList.Count ;k++){
			questList[k].Save();
		}
		totalInvent = inventoryList.Count;
		PlayerPrefs.SetInt ("totalInvent", totalInvent);
		for ( int l = 0 ; l < totalInvent ;l++){
		//	inventoryList[l].Save();
			PlayerPrefs.SetInt("inventoryId"+l+"is"+GameData.tesId,inventoryList[l].Id);
			string namaItem = "";
			if ( inventoryList[l] is Gem ){
				namaItem = "Gem";
			}
			else{
				namaItem = "Catalyst";
			}
			PlayerPrefs.SetString("inventoryName"+l+"is"+GameData.tesId,inventoryList[l].Name+"\n"+namaItem);
		}
		//private int nextStage;
	}

	public void LoadData(){
		level = PlayerPrefs.GetInt("level");
		nextMission = PlayerPrefs.GetInt("nextMission");
		currentExp = 	PlayerPrefs.GetInt("currentExp");
		gold = PlayerPrefs.GetInt("gold");
		diamond = PlayerPrefs.GetInt("diamond");
		defeatedArmy = PlayerPrefs.GetInt("defeatedArmy");
		fortressDestroyed = PlayerPrefs.GetInt("fortressDestroyed");
		castleDestroyed = PlayerPrefs.GetInt("castleDestroyed");
		unlockedTroop =  PlayerPrefs.GetInt("unlockedTroop");
		mapPosition = PlayerPrefs.GetInt("mapPosition");
		unlockedSlot = PlayerPrefs.GetInt("unlockedSlot");
		activeHeroes = PlayerPrefs.GetInt("activeHeroes");
		totalSkillUsed = PlayerPrefs.GetInt("totalSkillUsed");
		nextExp = base_exp;
		for (int i = 0; i < level; i++)
						nextExp *= 2;
		for (int i = 0; i < 10; i++)
			unitList [i].Load ();

		Debug.Log ("load unlocked slot " + unlockedSlot);
		for (int i = 0; i < unlockedSlot; i++) {
			formationList [i].IsUnlocked = true;
		}
	//	Debug.Log("slot " + slot + " isunlock " + formationList[slot].IsUnlocked);
		int slot = 0;
		Debug.Log ("load active heroes " + activeHeroes);
		for (int i = 0; i < 10; i++) {
			if ( unitList[i].IsActive ){
				formationList[slot].SetUnit(i,unitList[i]);
				slot++;
			}
		}
		for ( int j = 0 ; j < skillList.Count ;j++){
			skillList[j].Load();
		}
		for ( int k = 0 ; k < questList.Count ;k++){
			questList[k].Load();
		}
		totalInvent = PlayerPrefs.GetInt ("totalInvent");
		int inventId = 0;
		string namaItem = "";
		for ( int l = 0 ; l < totalInvent ;l++){
			//	inventoryList[l].Save();
			inventId = PlayerPrefs.GetInt("inventoryId"+l+"is"+GameData.tesId);
			namaItem = PlayerPrefs.GetString("inventoryName"+l+"is"+GameData.tesId);
			if ( namaItem.Contains("Gem") ){
				inventoryList.Add(new Gem(inventId,namaItem.Split("\n"[0])[0]));
				Debug.Log("added gem TO INVNET " + namaItem );
			}
			else{
				inventoryList.Add(new Catalyst(inventId,namaItem.Split("\n"[0])[0]));
				Debug.Log("added CATALYST TO INVNET" + namaItem);
			}

		}
		//for (int i = 0; i < activeHeroes; i++) //  5 doang yo langsung ae
		
		

	}

	public void NewData(string n){
		name = "Floo";
		level = 1;
		nextMission = 0;
		currentExp = 0;
		gold = 500;
		diamond = 0;
		//nextStage = 1;
		unlockedSlot = 1;
		activeHeroes = 1;
		totalSkillUsed = 1;
		nextExp = base_exp;
		formationList = new List<FormationUnit> ();
		skillList = new List<Skill> ();
		inventoryList = new List<Item> ();
		questList = new List<Quest> ();
		unitList = new List<Unit> ();
//			Debug.Log ("next exp " + nextExp);
	}

	public void CheckQuestAchievement(){
		foreach (Quest q in questList) {
			if ( q.Target.Contains("defeat"))
				if ( defeatedArmy >= q.QuantityNeeded )
					q.IsCompleted = true;
			if ( q.Target.Contains("fortress"))
				if ( fortressDestroyed >= q.QuantityNeeded )
					q.IsCompleted = true;
			if ( q.Target.Contains("castle"))
				if ( castleDestroyed >= q.QuantityNeeded )
					q.IsCompleted = true;
			if ( q.Target.Contains("gold"))
				if ( Gold >= q.QuantityNeeded )
					q.IsCompleted = true;

		}
	}

	public void ProfileLevelUp(){
		currentExp -= nextExp;
		level++;
		nextExp *= 3;//GameData.expList [level];
		Debug.Log ("Profile level up " + level + " " + nextExp);
	}


	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}

	public int Level {
		get {
			return level;
		}
		set {
			level = value;
		}
	}

	public int NextMission {
		get {
			return nextMission;
		}
		set {
			nextMission = value;
		}
	}

	public int MapPosition {
		get {
			return mapPosition;
		}
		set {
			mapPosition = value;
		}
	}

	public int DefeatedArmy {
		get {
			return defeatedArmy;
		}
		set {
			defeatedArmy = value;
		}
	}

	public bool IsLevelUp(int gotExp){
		bool ret =false;
		if (currentExp+gotExp >= nextExp)
			ret = true;
		return ret;
	}
	
	public int CurrentExp {
		get {
			return currentExp;
		}
		set {
			currentExp = value;
			if ( currentExp >= nextExp ){
				ProfileLevelUp();
			}
		}
	}

	public int UnlockedTroop {
		get {
			return unlockedTroop;
		}
		set {
			unlockedTroop = value;
		}
	}

	public int Gold {
		get {
			return gold;
		}
		set {
			gold = value;
		}
	}

	public int FortressDestroyed {
		get {
			return fortressDestroyed;
		}
		set {
			fortressDestroyed = value;
		}
	}

	public int Diamond {
		get {
			return diamond;
		}
		set {
			diamond = value;
		}
	}

	public int CastleDestroyed {
		get {
			return castleDestroyed;
		}
		set {
			castleDestroyed = value;
		}
	}

	public int NextExp {
		get {
			return nextExp;
		}
		set {
			nextExp = value;
		}
	}
}


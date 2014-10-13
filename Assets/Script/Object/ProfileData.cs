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
		PlayerPrefs.SetInt ("level"+GameData.tesId, level);
		PlayerPrefs.SetInt("nextMission"+GameData.tesId,nextMission);
		PlayerPrefs.SetInt("currentExp"+GameData.tesId, currentExp);
		PlayerPrefs.SetInt("gold"+GameData.tesId, gold);
		PlayerPrefs.SetInt("diamond"+GameData.tesId, diamond);
		PlayerPrefs.SetInt("defeatedArmy"+GameData.tesId, defeatedArmy);
		PlayerPrefs.SetInt("fortressDestroyed"+GameData.tesId, fortressDestroyed);
		PlayerPrefs.SetInt("castleDestroyed"+GameData.tesId, castleDestroyed);
		PlayerPrefs.SetInt("unlockedTroop"+GameData.tesId,  unlockedTroop);
		PlayerPrefs.SetInt("mapPosition"+GameData.tesId, mapPosition);
		PlayerPrefs.SetInt("unlockedSlot"+GameData.tesId, unlockedSlot);
		PlayerPrefs.SetInt("activeHeroes"+GameData.tesId,  activeHeroes);
		PlayerPrefs.SetInt("totalSkillUsed"+GameData.tesId, totalSkillUsed);
		Debug.Log ("save castle destroyed  " + castleDestroyed);
//		Debug.Log ("save activeheroes " + activeHeroes);
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
		PlayerPrefs.SetInt ("totalInvent"+GameData.tesId, totalInvent);
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

		level =  GameData.CheckPrefs("level"+GameData.tesId) ? PlayerPrefs.GetInt("level"+GameData.tesId): level;
		nextMission = GameData.CheckPrefs("nextMission"+GameData.tesId) ? PlayerPrefs.GetInt("nextMission"+GameData.tesId): nextMission;
		currentExp = GameData.CheckPrefs("currentExp"+GameData.tesId) ? PlayerPrefs.GetInt("currentExp"+GameData.tesId) :currentExp;
		gold = GameData.CheckPrefs("gold"+GameData.tesId) ? PlayerPrefs.GetInt("gold"+GameData.tesId) : gold;
		diamond = GameData.CheckPrefs("diamond"+GameData.tesId) ? PlayerPrefs.GetInt("diamond"+GameData.tesId) : diamond;
		defeatedArmy = GameData.CheckPrefs("defeatedArmy"+GameData.tesId) ? PlayerPrefs.GetInt("defeatedArmy"+GameData.tesId) : defeatedArmy;
		fortressDestroyed = GameData.CheckPrefs("fortressDestroyed"+GameData.tesId) ? 
			PlayerPrefs.GetInt("fortressDestroyed"+GameData.tesId) : fortressDestroyed;
		castleDestroyed = GameData.CheckPrefs("castleDestroyed"+GameData.tesId) ? PlayerPrefs.GetInt("castleDestroyed"+GameData.tesId) : castleDestroyed;
		unlockedTroop =  GameData.CheckPrefs("unlockedTroop"+GameData.tesId) ? PlayerPrefs.GetInt("unlockedTroop"+GameData.tesId) : unlockedTroop;
		mapPosition = GameData.CheckPrefs("mapPosition"+GameData.tesId) ? PlayerPrefs.GetInt("mapPosition"+GameData.tesId) : mapPosition ;
		unlockedSlot = GameData.CheckPrefs("unlockedSlot"+GameData.tesId) ? PlayerPrefs.GetInt("unlockedSlot"+GameData.tesId) : unlockedSlot;
		activeHeroes = GameData.CheckPrefs("activeHeroes"+GameData.tesId) ? PlayerPrefs.GetInt("activeHeroes"+GameData.tesId) : activeHeroes;
		totalSkillUsed = GameData.CheckPrefs("totalSkillUsed"+GameData.tesId) ? PlayerPrefs.GetInt("totalSkillUsed"+GameData.tesId) : totalSkillUsed;
		nextExp = base_exp;
		Debug.Log ("Load Castle destroyed  " + castleDestroyed);
		for (int i = 0; i < level; i++)
						nextExp *= 2;
		for (int i = 0; i < 10; i++)
			unitList [i].Load ();

//		Debug.Log ("load unlocked slot " + unlockedSlot);
		for (int i = 0; i < unlockedSlot; i++) {
			formationList [i].IsUnlocked = true;
		}
	//	Debug.Log("slot " + slot + " isunlock " + formationList[slot].IsUnlocked);
		int slot = 0;
//		Debug.Log ("load active heroes " + activeHeroes);
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
		totalInvent = PlayerPrefs.GetInt ("totalInvent"+GameData.tesId);
		int inventId = 0;
		string namaItem = "";
		for ( int l = 0 ; l < totalInvent ;l++){
			//	inventoryList[l].Save();
			inventId = PlayerPrefs.GetInt("inventoryId"+l+"is"+GameData.tesId);
			namaItem = PlayerPrefs.GetString("inventoryName"+l+"is"+GameData.tesId);
			if ( namaItem.Contains("Gem") ){
				inventoryList.Add(new Gem(inventId,namaItem.Split("\n"[0])[0]));
//				Debug.Log("added gem TO INVNET " + namaItem );
			}
			else{
				inventoryList.Add(new Catalyst(inventId,namaItem.Split("\n"[0])[0]));
//				Debug.Log("added CATALYST TO INVNET" + namaItem);
			}

		}
		CheckQuestAchievement ();
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


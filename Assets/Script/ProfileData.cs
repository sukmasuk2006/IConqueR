using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ProfileData 
{
	private string name;
	private int level;
	private int nextMission;
	private int currentExp;
	private int nextExp;
	private int gold;
	private int diamond;
	private int nextStage;

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
	public  List<FormationUnit> formationList;
	public  List<Skill> skillList;
	public  List<Item> inventoryList;
	public  List<Quest> questList;
	public  List<Unit> unitList;

	public ProfileData(){

	}

	public void NewData(string n){
		name = name;
		level = 1;
		nextMission = 0;
		currentExp = 0;
		gold = 500;
		diamond = 0;
		nextStage = 1;
		unlockedSlot = 1;
		activeHeroes = 1;
		totalSkillUsed = 1;
		nextExp = GameData.expList [level];
		formationList = new List<FormationUnit> ();
		skillList = new List<Skill> ();
		inventoryList = new List<Item> ();
		questList = new List<Quest> ();
		unitList = new List<Unit> ();
		Debug.Log ("next exp " + nextExp);
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

	public void LoadData(){
	
	}

	public void ProfileLevelUp(){
		currentExp -= nextExp;
		level++;
		nextExp = GameData.expList [level];
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


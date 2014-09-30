using UnityEngine;
using System.Collections;

public class ProfileData 
{
	private string name;
	private int level;
	private int currentMission;
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

	public ProfileData(){

	}

	public void NewData(string n){
		name = name;
		level = 1;
		currentMission = 1;
		currentExp = 0;
		gold = 0;
		diamond = 0;
		nextStage = 1;
	
		nextExp = GameData.expList [level];
	}

	public void CheckQuestAchievement(){
		foreach (Quest q in GameData.questList) {
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
		level++;
		nextExp = GameData.expList [level];
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

	public int CurrentMission {
		get {
			return currentMission;
		}
		set {
			currentMission = value;
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

	public int CurrentExp {
		get {
			return currentExp;
		}
		set {
			currentExp = value;
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


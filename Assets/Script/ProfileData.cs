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

	public int CurrentExp {
		get {
			return currentExp;
		}
		set {
			currentExp = value;
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

	public int Diamond {
		get {
			return diamond;
		}
		set {
			diamond = value;
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


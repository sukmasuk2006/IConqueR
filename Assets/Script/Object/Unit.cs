using UnityEngine;
using System.Collections;

public class Unit : UnitStatus {

	private int heroId;
	private string job;
	private int currentExp;
	private int nextExp;
	private bool isUnlocked;

	public Unit(int id,string name,string job,string desc,float str,
	            float agi,float vit):
	base(name,desc,str,agi,vit){
		heroId = id;
		nextExp = 10;
		currentExp = 0;
		isUnlocked = false;
	}

	public string Job {
		get {
			return job;
		}
		set { 
			job = value;
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

	public int NextExp {
		get {
			return nextExp;
		}
		set {
			nextExp = value;
		}
	}

	public bool IsUnlocked {
				get {
						return isUnlocked;
				}
				set {
						isUnlocked = value;
				}
		}
}

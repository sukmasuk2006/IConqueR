using UnityEngine;
using System.Collections;

public class Hero : Unit {

	private string job;
	private int currentExp;
	private int nextExp;

	public Hero(string iconPath,string name,string job,string desc,float health,float atk,float def,
	            float spd):
	base(iconPath,name,desc,health,atk,def,spd){
		nextExp = 10;
		currentExp = 0;
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

}

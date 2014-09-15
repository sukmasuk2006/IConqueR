using UnityEngine;
using System;
using System.Collections;

public class Unit : UnitStatus {

	private int heroId;
	private string job;
	private int currentExp;
	private int nextExp;
	private bool isUnlocked;
	private bool isActive;
	private int goldNeeded;

	public Unit(string name):
	base(){
		this.name = name;
		InitializeHero ();
	}

	private void InitializeHero(){
		TextAsset txt = (TextAsset)Resources.Load ("Data/Unit/" + name.Trim(), typeof(TextAsset));
		string content = txt.text;
		string[] linesFromFile = content.Split ("\n" [0]);

		isActive = false;
		nextExp = 100;
		currentExp = 0;
		isUnlocked = false;
		this.level = 1;
		this.description = linesFromFile [0];
		this.goldNeeded = int.Parse( linesFromFile [1]);
		this.str = int.Parse( linesFromFile [2]);
		this.agi = int.Parse( linesFromFile [3]);
		this.vit = int.Parse( linesFromFile [4]);
		//status = new Status (str, agi, intel, dex, vit);

		SetStats ();
	}
	public void CopyStats(Unit u){
		name = u.name;
		this.str = u.str;
		this.agi = u.agi;
		this.vit = u.vit;
		SetStats ();
	}

	private void SetStats(){
		healthPoint = maxHealthPoint = (str + vit * 2) * 3;
		attackPoint = str  *3;
		defensePoint = vit * 3;
		//
		attackSpeed = float.Parse( Round(2f - (agi - 1f) * 0.015f).ToString());
		critical =  float.Parse( Round((str + agi *2 ) / 3).ToString());
		evasionRate = float.Parse(Round((vit + agi *2 ) / 3).ToString());
		movement = float.Parse(Round((agi + agi *2 )).ToString());
	}

	public void Refresh(){
		healthPoint = maxHealthPoint;
		Debug.Log ("refresh");
	}
	
	private double Round(float value){
		return	Math.Round (value, 2);
	}

	public int GoldNeeded {
		get {
			return goldNeeded;
		}
		set {
			goldNeeded = value;
		}
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
			if ( currentExp >= nextExp ){
				currentExp -= nextExp;
				level++;
				nextExp *= 2;
			}
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

	public bool IsActive {
		get {
			return isActive;
		}
		set {
			isActive = value;
		}
	}
}

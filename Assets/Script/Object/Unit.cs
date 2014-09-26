using UnityEngine;
using System;
using System.Collections.Generic;

public class Unit : UnitStatus {

	private int heroId;
	private string job;
	private int currentExp;
	private int nextExp;
	private bool isUnlocked;
	private bool isActive;
	private int goldNeeded;
	private Weapon weapon;
	private Sprite sprites;
	private Sprite icon;
	private bool isCritical;

	public Unit(int id,string name):
	base(){
		this.name = name;
		heroId = id;
		InitializeHero ();
	}

	private void InitializeHero(){
		TextAsset txt = (TextAsset)Resources.Load ("Data/Unit/" + name.Trim(), typeof(TextAsset));
		string content = txt.text;
		string[] linesFromFile = content.Split ("\n" [0]);
		
		sprites = (Sprite)Resources.Load ("Sprite/Character/Hero/" + name.Trim (), typeof(Sprite));
		icon = (Sprite)Resources.Load ("Sprite/Character Icon/" + name.Trim (), typeof(Sprite));
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
		weapon = new Weapon (name ,float.Parse(linesFromFile[5]),float.Parse(linesFromFile[6]));
		//status = new Status (str, agi, intel, dex, vit);

		SetStats ();
	}
	public void CopyStats(int id,Unit u){
		name = u.name;
		heroId = id;
		this.str = u.str;
		this.agi = u.agi;
		this.vit = u.vit;
		this.sprites = u.sprites;
		this.icon = u.icon;
		this.level = u.level;
		this.weapon = u.weapon;
		SetStats ();
	}

	private void SetStats(){
		isCritical = false;
		healthPoint = maxHealthPoint = ((str + vit) * 5); //  min 117 max 999
		attackPoint = str/2 + weapon.Damage; // min 59 max 255
		defensePoint = vit/2; // min 59 max 255
		//
		attackSpeed = float.Parse( Round(2.5f - (agi - 1f) * 0.020408f).ToString()); // min 2.5f max 0.5f  
		if (weapon.Range == 5)
						AttackSpeed *= 1.5f;
		critical =  float.Parse( Round(((str + agi *2 ) / 3)*0.55f).ToString()); // min 1 max 55 
		critChance = critical;
		evasionRate = float.Parse(Round(((vit + agi *2 ) / 3)*0.55f).ToString()); // min 1 max 55
		movement = float.Parse(Round(( (Agi * 2 ) + 200f )).ToString()); // 200f 400f
		pushForce = (0.18f * agi)+ 2f; // min 2f max 20 
	}
	private void LevelUp(){
		currentExp -= nextExp;
		level++;
		nextExp *= 2;
		str++;
		agi++;
		vit++;
		SetStats ();
	}

	public bool IsCritical {
		get {
			return isCritical;
		}
		set {
			isCritical = value;
		}
	}

	public float Damage{
		get 
		{
			float dmg = attackPoint + weapon.Damage;
			int isCrit = UnityEngine.Random.Range(0,99);
			if ( isCrit < CritChance ){
				dmg*=2;
				critChance = critical;
				isCritical = true;
			//	Debug.Log("CRITICAL ? " + isCrit+ " " + CritChance);
				// jika critical, chance balik ke awal
			}
			else{
				critChance++;
				isCritical = false;
				// jika tidak crit, tingkatkan tingkat critical
			}
			return dmg;
		}
	}

	public int HeroId {
		get {
			return heroId;
		}
		set {
			heroId = value;
		}
	}

	public float ReceiveDamage(float dmg, bool crit){
		int isEvade = UnityEngine.Random.Range(0,99);
		float returnDamage = dmg;
				if (isEvade >= evasionRate || crit) 
				{
					returnDamage -= defensePoint;
						if ( returnDamage <= 0 ) returnDamage = 1;
					HealthPoint -= returnDamage;
					if ( healthPoint <= 0 ) healthPoint = 0;
						// jika gagal eva, kena dmg
				} else if (isEvade < evasionRate && !crit) {
					returnDamage = 0;
			// jika sukses dan tidak crit
				} 
		return returnDamage;
	}

	public Sprite Icon {
		get {
			return icon;
		}
		set {
			icon = value;
		}
	}

	public void Refresh(){
		healthPoint = maxHealthPoint;
//		Debug.Log ("refresh");
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
				LevelUp();
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


	public Weapon Weapon {
		get {
			return weapon;
		}
		set {
			weapon = value;
		}
	}

	public Sprite Sprites {
		get {
			return sprites;
		}
		set {
			sprites = value;
		}
	}
}

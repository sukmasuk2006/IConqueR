	using UnityEngine;
using System.Collections;

public class UnitStatus  {
	
	protected string spriteName;
	protected string name;
	protected string description;
	protected int level;
	protected const int maxLevel = 10;
	//upgradable stats
	protected float str;

	public float Str {
		get {
			return str;
		}
		set {
			str = value;
		}
	}

	protected float agi;

	public float Agi {
		get {
			return agi;
		}
		set {
			agi = value;
		}
	}

	protected float vit;

	public float Vit {
		get {
			return vit;
		}
		set {
			vit = value;
		}
	}

	// automatic stats
	protected float healthPoint; /* (1 str + 2 vit )x 10*/
	protected float attackPoint; /* 3 str */
	protected float defensePoint; /* 3 vit */
	protected float attackSpeed; /* 3 agi */
	protected float critical;  /* 1 str + 2 agi */
	protected float evasionRate; /* 2 agi + 1 vit */
	protected float movement;
	public float Movement {
		get {
			return movement;
		}
		set {
			movement = value;
		}
	}

 /* 1 agi + 2 vit */

	public UnitStatus(string name,string desc, float str,float agi, float vit){
		this.name = name;
		this.description = desc;
		this.level = 1;
		this.str = str;
		this.agi = agi;
		this.vit = vit;
		//status = new Status (str, agi, intel, dex, vit);
		healthPoint = (str + vit * 2) * 10;
		attackPoint = str  *3;
		defensePoint = vit * 3;
		attackSpeed = 3.5f - (( agi - 1f ) * 0.025f);
		critical = (str + agi *2 ) / 3;
		evasionRate = (vit + agi *2 ) / 3;
		movement = (agi + agi *2 ) / 3;
	}

	public float EvasionRate {
		get {
			return evasionRate;
		}
		set {
			evasionRate = value;
		}
	}

	public string IconPath {
		get {
			return spriteName;
		}
		set {
			spriteName = value;
		}
	}

	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}

	public string Description {
		get {
			return description;
		}
		set {
			description = value;
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

	public float HealthPoint {
		get {
			return healthPoint;
		}
		set {
			healthPoint = value;
		}
	}

	public float AttackPoint {
		get {
			return attackPoint;
		}
		set {
			attackPoint = value;
		}
	}

	public float DefensePoint {
		get {
			return defensePoint;
		}
		set {
			defensePoint = value;
		}
	}

	public float AttackSpeed {
		get {
			return attackSpeed;
		}
		set {
			attackSpeed = value;
		}
	}

	public float Critical {
		get {
			return critical;
		}
		set {
			critical = value;
		}
	}


}

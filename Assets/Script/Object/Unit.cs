using UnityEngine;
using System.Collections;

public class Unit  {

	protected string spriteName;
	protected string name;
	protected string description;
	protected int level;
	protected const int maxLevel = 10;
	protected float healthPoint;
	protected float attackPoint;
	protected float defensePoint;
	protected float speedPoint; // aspd
	protected float critical;
	protected float hitRate;
	protected float evasionRate;


	public Unit(string iconPath,string name,string desc, float health, float atk,float def,float speed){
		this.spriteName = iconPath;
		this.name = name;
		this.description = desc;
		this.level = 1;
		//status = new Status (str, agi, intel, dex, vit);
		healthPoint = health;
		attackPoint = atk;
		defensePoint = def;
		speedPoint = speed;
		critical = 10;
		hitRate = attackPoint + speedPoint;
		evasionRate = defensePoint + speedPoint;
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

	public float MovementPoint {
		get {
			return speedPoint;
		}
		set {
			speedPoint = value;
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

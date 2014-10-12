using UnityEngine;

public class UnitStatus  {
	
	public void SaveStatus(string key)
	{
		PlayerPrefs.SetInt (key + "level" + GameData.tesId, level);
		PlayerPrefs.SetFloat(key+"str"+GameData.tesId,str);
		PlayerPrefs.SetFloat(key+"agi"+GameData.tesId,agi);
		PlayerPrefs.SetFloat(key+"vit"+GameData.tesId,vit);

	}
	
	public void LoadStatus (string key)
	{
		level = PlayerPrefs.GetInt (key + "level" + GameData.tesId);
		str = PlayerPrefs.GetFloat(key+"str"+GameData.tesId);
		agi = PlayerPrefs.GetFloat(key+"agi"+GameData.tesId);
		vit = PlayerPrefs.GetFloat(key+"vit"+GameData.tesId);
	}

	protected string name;
	//protected string description;
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
	protected float maxHealthPoint;
	protected float attackPoint; /* 3 str */
	protected float defensePoint; /* 3 vit */
	protected float attackSpeed; /* 3 agi */
	protected float critical;  /* 1 str + 2 agi */
	protected float evasionRate; /* 2 agi + 1 vit */
	protected float movement;
	protected float maxMovement;
	protected float pushForce;
	protected float critChance;

	public float CritChance {
		get {
			return critChance;
		}
		set {
			critChance = value;
		}
	}

	public float PushForce {
		get {
			return pushForce;
		}
		set {
			pushForce = value;
		}
	}

	public float MaxMovement {
		get {
			return maxMovement;
		}
		set {
			maxMovement = value;
		}
	}

	public float MaxHealthPoint {
		get {
			return maxHealthPoint;
		}
		set {
			maxHealthPoint = value;
		}
	}

	public float Movement {
		get {
			return movement;
		}
		set {
			movement = value;
		}
	}

 /* 1 agi + 2 vit */

	public UnitStatus(){
		str = 0;
		agi = 0;
		vit = 0;
	}


	public float EvasionRate {
		get {
			return evasionRate;
		}
		set {
			evasionRate = value;
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

	/*public string Description {
		get {
			return description;
		}
		set {
			description = value;
		}
	}*/

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

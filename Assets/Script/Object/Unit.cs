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
	private SkeletonDataAsset playerData;
	private AtlasAsset atlasdata;

	public SkeletonDataAsset PlayerData {
		get {
			return playerData;
		}
	}

	public Unit(int id,string name):
	base(){
		this.job = this.name = name;
		heroId = id;
		InitializeHero ();
	}

	private void InitializeHero(){
		TextAsset txt = (TextAsset)Resources.Load ("Data/Unit/" + job, typeof(TextAsset));
		string content = txt.text;
		string[] linesFromFile = content.Split ("\n" [0]);
		
		sprites = (Sprite)Resources.Load ("Sprite/Character/Hero/" + job, typeof(Sprite));
		icon = (Sprite)Resources.Load ("Sprite/Character Icon/" + job, typeof(Sprite));
		//SetSpine ();	
		isActive = false;
		nextExp = 100;
		currentExp = 0;
		isUnlocked = false;
		this.name = linesFromFile [0];
		this.level = 1;
		this.goldNeeded = int.Parse( linesFromFile [1]);
		weapon = new Weapon (name ,float.Parse(linesFromFile[5]),float.Parse(linesFromFile[6]));
		this.str = int.Parse (linesFromFile [2]) + weapon.WeaponStats.Str;
		this.agi = int.Parse( linesFromFile [3]) + weapon.WeaponStats.Agi;
		this.vit = int.Parse( linesFromFile [4]) + weapon.WeaponStats.Vit;
		SetStats ();
	
	}

	public void SetSpine(){
		/*atlasdata = ScriptableObject.CreateInstance<AtlasAsset> ();
		playerData = ScriptableObject.CreateInstance<SkeletonDataAsset> ();
		playerData.fromAnimation = new string[0];
		playerData.toAnimation = new string[0];
		playerData.duration = new float[0];
		playerData.scale = 0.01f;
		playerData.defaultMix = 0.15f;
		
		
		atlasdata.atlasFile = (TextAsset)Resources.Load ("Sprite/Character/"+name+"/skeleton.atlas", typeof(TextAsset));
		Material[] materials = new Material[1];
		materials [0] = new Material (Shader.Find("Transparent/Diffuse"));
		Texture aa = (Texture)Resources.Load ("Sprite/Character/"+name+"/skeleton", typeof(Texture2D));
		materials [0].mainTexture = aa;
		
		atlasdata.materials = materials;
		
		playerData.atlasAsset = atlasdata;
		playerData.skeletonJSON = (TextAsset)Resources.Load ("Sprite/Character/"+name+"/skeleton.json", typeof(TextAsset));
		Debug.Log ("NAME " + name);
*/

		playerData = ScriptableObject.CreateInstance<SkeletonDataAsset> ();
		playerData = (SkeletonDataAsset)Resources.Load ("Sprite/Character/"+job+"/mySkeldata",typeof(SkeletonDataAsset));
		
		playerData.scale = 0.006f;
		playerData.defaultMix = 0.55f;

	}

	/**/
	public void CopyStats(int id,Unit u){
		name = u.name;
		job = u.job;
		heroId = id;
		this.str = u.str;
		this.agi = u.agi;
		this.vit = u.vit;
		this.sprites = u.sprites;
		this.icon = u.icon;
		this.level = u.level;
		this.weapon = u.weapon;
		this.playerData = u.playerData;
		SetStats ();

	}
	/**/


	public void SetStats(){
		isCritical = false;
		// tidak bisa ditambah langsung, salah
		float tempStr = str + weapon.WeaponStats.Str;
		float tempAgi = agi + weapon.WeaponStats.Agi;
		float tempVit = vit + weapon.WeaponStats.Vit;

		healthPoint = maxHealthPoint = ((tempStr + tempVit) * 5); //  min 117 max 999
		attackPoint = tempStr/2 + weapon.Damage; // min 59 max 255
		defensePoint = tempVit/2; // min 59 max 255
		//
		attackSpeed = float.Parse( Round(2.5f - (tempAgi - 1f) * 0.020408f).ToString()); // min 2.5f max 0.5f  
		if (weapon.Range == 5)
						AttackSpeed *= 1.5f;
		critical =  float.Parse( Round(((tempStr + tempAgi *2 ) / 3)*0.55f).ToString()); // min 1 max 55 
		critChance = critical;
		evasionRate = float.Parse(Round(((tempVit + tempAgi *2 ) / 3)*0.55f).ToString()); // min 1 max 55
		movement = float.Parse(Round(( (tempAgi * 2 ) + 200f )).ToString()); // 200f 400f
		pushForce = (0.18f * tempAgi)+ 2f; // min 2f max 20 
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

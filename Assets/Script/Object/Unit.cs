using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Unit : UnitStatus {

	
	const int STR = 0; // MAX STATS 99
	const int AGI = 1;
	const int VIT = 2;

	private int heroId;
	private string job;
	private int currentExp;
	private int nextExp;
	private bool isUnlocked;
	private bool isActive;
	private int goldNeeded;
	private bool isCritical;
	private int statsType;
	private List<string> jobList;
	private int currentJob;
	private const int base_exp = 30;

	private Weapon weapon;

	public Unit(int id,string name):
	base(){
		this.job  = name.Trim();
		this.currentJob = 0;
		heroId = id;
		InitializeHero ();
	}

	private void InitializeHero(){
		jobList = new List<string>();
		//SetSpine ();	
		TextAsset etxt = (TextAsset)Resources.Load ("Data/Unit/"+job, typeof(TextAsset));
		string econtent = etxt.text;
		string[] linesFromFile = econtent.Split ("\n" [0]);
		isActive = false;
		nextExp = base_exp * level * 2 * ((heroId/2)+1);
		currentExp = 0;
		isUnlocked = false;
		this.name = linesFromFile [0];
		this.level = 1;
		this.goldNeeded = int.Parse( linesFromFile [1]);
		this.jobList.Add(linesFromFile[9].Trim());
		this.jobList.Add(linesFromFile[10].Trim());
		weapon = new Weapon (heroId,jobList[currentJob] ,linesFromFile[8].Trim(),float.Parse(linesFromFile[5]),float.Parse(linesFromFile[6]));
		this.statsType = int.Parse(linesFromFile[7]);
		this.str = int.Parse (linesFromFile [2]) + weapon.WeaponStats.Str;
		this.agi = int.Parse( linesFromFile [3]) + weapon.WeaponStats.Agi;
		this.vit = int.Parse( linesFromFile [4]) + weapon.WeaponStats.Vit;
		SetStats ();

	}

	public void EnhanceJob(){
		currentJob++;
		goldNeeded *= 2;
		str += (heroId *statsType == 0 ? 10 : 5);   
		agi += (heroId * statsType == 1 ? 10 : 5);
		vit +=  (heroId *statsType == 2 ? 10 : 5); 
		
	}

	/**/
	public void CopyStats(Unit u){
		name = u.name;
		job = u.job;
		heroId = u.heroId;
		this.str = u.str;
		this.agi = u.agi;
		this.vit = u.vit;
		this.level = u.level;
		this.weapon = u.weapon;
		this.currentExp = u.currentExp;
		this.nextExp = u.nextExp;
		this.level = u.level;
		this.statsType = u.statsType;
		this.CurrentJob = u.CurrentJob;
		this.JobList = u.JobList;
		SetStats ();

	}
	/**/
	private void SetLevelUp(){
		string jobType = jobList[0];
		switch (jobType) {
		case "Knight" :  str++; vit++; if ( level % 2 == 0 ) agi++;   // knight
			break; 
		case "Warrior" : str++; agi++; if ( level % 2 == 0 ) vit++;
			break;
		case "Archer" :  str++; agi++; vit++;
			break;
		case "Tribe" : str++; agi++; vit++;
			break;	
		case "Monk" :  str+=2; agi+=2; if ( level % 2 == 0 ) vit++;   
			break; 
		case "Thief" : str+=2; agi+=2; if ( level % 2 == 0 ) vit++;   // knight
			break;
		case "Assasin" : str+=2; agi+=2; if ( level % 2 == 0 ) vit++;
			break;
		case "Hunter" : str+=2; agi+=2; if ( level % 2 == 0 ) vit++;
			break;
		case "Ninja" :  str+=3; agi+=3; if ( level % 2 == 0 ) vit++;   
			break;
		case "Ksatria" :  str+=3; agi+=3; vit+=3;   // knight
			break;
		}	
	}

	private void SetConstraint(){
		string jobType = jobList[0];
		switch (jobType) {
		case "Knight" :  critical *= 0.65f; evasionRate *= 0.65f; attackSpeed *= 1.35f; healthPoint*= 1.35f;
			break;
		case "Tribe" : evasionRate *= 1.35f; defensePoint *= 0.65f;
			break;	
		case "Archer" :  critical *= 0.65f; evasionRate *= 0.65f; attackSpeed *= 0.65f; attackPoint *= 1.35f;
			break;
		case "Monk" :  evasionRate *= 1.35f;  defensePoint *= 0.65f; healthPoint *= 0.65f; attackPoint *= 1.35f;
			break; 
		case "Thief" : evasionRate *= 1.35f;  critical*= 1.35f;   healthPoint *= 0.65f;// knight
			break;
		case "Assasin" : evasionRate *= 1.35f;  critical*= 1.55f; attackSpeed *= 0.65f; attackPoint *= 0.65f; defensePoint *= 0.65f; healthPoint *= 0.65f;
			break;
		case "Hunter" : critical *= 1.35f;
			break;
		case "Ninja" :  evasionRate *= 1.35f;  critical*= 1.45f ; attackSpeed *= 0.65f; defensePoint *= 0.65f; healthPoint *= 0.65f;  
			break;
		case "Ksatria" :  critical *= 1.55f; evasionRate *= 0.65f; attackPoint *= 1.35f; defensePoint *= 1.35f; healthPoint *= 1.35f;
			break;
		}	
		if ( attackSpeed <= 0.5f ) attackSpeed = 0.5f;
		if ( critical >= 60f ) critical = 60f;
		if ( evasionRate >= 55f ) evasionRate = 55f;
	}

	public void SetStats(){
		isCritical = false;
		// tidak bisa ditambah langsung, salah
		float tempStr = str + weapon.WeaponStats.Str;
		float tempAgi = agi + weapon.WeaponStats.Agi;
		float tempVit = vit + weapon.WeaponStats.Vit;

		// min 
		healthPoint = maxHealthPoint = (tempVit * 2) + (tempStr *  2); //  min 117 max 999
		attackPoint = tempStr + weapon.Damage; // min 59 max 255
		defensePoint = tempVit; // min 59 max 255
		//
		attackSpeed = float.Parse( Round(1.5f - (tempAgi  * 0.0067f)).ToString()); // min 1f max 0.5f  
		if (weapon.Range == 5)
				AttackSpeed *= 0.75f;
		critical =  float.Parse( Round((( tempAgi * 2 ) / 3)*0.55f).ToString()); // min 1 max 55 
		critChance = critical;
		evasionRate = float.Parse(Round(( tempAgi *2  / 3)*0.55f).ToString()); // min 1 max 55
		movement = float.Parse(Round(( (tempAgi * 2 ) + 150f )).ToString()); // 200f 400f
		if ( AttackSpeed < 0.5f ) attackSpeed = 0.5f;
		if ( movement > 250f ) movement = 250f;
		pushForce =  (tempStr/149 * 75) + 50f;
		SetConstraint();
//		Debug.Log(jobList[currentJob] + " str " + str + " health " + healthPoint);
	}

	public void Save ()
	{
		SaveStatus (job); // save level
		PlayerPrefs.SetInt(job+"currentExp"+GameData.tesId,currentExp);
//		Debug.Log ("save job " + job + " " + currentExp);
		PlayerPrefs.SetInt(job+"isUnlocked"+GameData.tesId,(isUnlocked ? 1 : 0 ));
		PlayerPrefs.SetInt(job+"currentJob"+GameData.tesId,CurrentJob);
		PlayerPrefs.SetInt(job+"isActive"+GameData.tesId,(isActive ? 1 : 0 ));
		weapon.Save();
	}

	public void Load(){
	 	currentJob = PlayerPrefs.GetInt(job+"currentJob"+GameData.tesId);
		LoadStatus (job); // load job status
		currentExp = PlayerPrefs.GetInt(job+"currentExp"+GameData.tesId);
	//	Debug.Log ("load job " + job + " " + currentExp);
		isUnlocked = (PlayerPrefs.GetInt(job+"isUnlocked"+GameData.tesId) != 0);
		isActive = (PlayerPrefs.GetInt(job+"isActive"+GameData.tesId) != 0);
		nextExp = base_exp * level * 2 * ((heroId/4)+1);

		weapon.Load ();
	}

	public void LevelUp(){
		if ( level < 25 ){
			currentExp -= nextExp;
			level++;
			nextExp = base_exp * level * 2 * ((heroId/4)+1);
			SetLevelUp();
			SetStats ();
		}
		else if ( level == 25 ){
			nextExp = 999999;
		}
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

	public float ReceiveDamage(float dmg, bool crit, int dealerType){
		// dmg : damage yg diterima, crit : chance critical dari musuh, dealertype : type musuh yg nggebuk
		int isEvade = UnityEngine.Random.Range(0,99);
		float returnDamage = dmg;
		float multiplier = CheckDealerType(dealerType);
		returnDamage *= multiplier;
		if (isEvade >= evasionRate || crit) 
				{
					// jaga2 biar gak splash attack
					if ( returnDamage >= healthPoint ){
						returnDamage = healthPoint;
						healthPoint = 0;
					}
					else{
							returnDamage -= defensePoint;
							if ( returnDamage <= 0 )
								returnDamage = 1;

							HealthPoint -= returnDamage;
							
							if ( healthPoint <= 0 ){
								healthPoint = 0;
							}
					}
						// jika gagal eva, kena dmg
				} 
				else if (isEvade < evasionRate && !crit) {
					returnDamage = 0;
			// jika sukses dan tidak crit
				}
//		Debug.Log(statsType + " digepuk sama " + dealerType + " damage " + multiplier + " hasl " + returnDamage); 
		return returnDamage;
	}

	private float CheckDealerType(int dealerType){
		float ret = 1f;
		/*if ( StatsType == STR ){
			if ( dealerType == AGI ) ret = 0.5f;
			else if ( dealerType == VIT ) ret = 1.5f;
		}
		else if ( StatsType == AGI ){
			if ( dealerType == VIT ) ret = 0.5f;
			else if ( dealerType == STR ) ret = 1.5f;
		}
		else if ( statsType == VIT ){
			if ( dealerType == STR ) ret = 0.5f;
			else if ( dealerType == AGI ) ret = 1.5f;
		}*/
		return ret;
	}

	public void Refresh(){
		healthPoint = maxHealthPoint;
//		Debug.Log ("refresh");
	}
	
	private double Round(float value){
		return	System.Math.Round (value, 2);
	}

	public int StatsType {
		get {
			return statsType;
		}
		set {
			statsType = value;
		}
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

	public bool IsLevelUp(int gotExp){
		bool ret =false;
		if (currentExp+gotExp >= nextExp)
						ret = true;
		return ret;
	}

	public int CurrentExp {
		get {
			return currentExp;
		}
		set {
			currentExp = value;
			while ( currentExp >= nextExp ){
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

	public int CurrentJob {
		get {
			return currentJob;
		}
		set {
			currentJob = value;
		}
	}

	public List<string> JobList {
		get {
			return jobList;
		}
		set {
			jobList = value;
		}
	}
}

using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class Weapon : Item {

	private float damage;
	private float range;
	private int rank;
	private UnitStatus weaponStats;
	private string gemRequired;
	private string soundEffectName;

	public string GemRequired {
		get {
			return gemRequired;
		}
	}

	//private ArrayList<Item> equippedGem;

	public Weapon(int id,string name,string soundEffName, float damage, float range):
		base(id,name){
		this.damage = damage;
		this.range = range;
		soundEffectName = soundEffName;
		weaponStats = new UnitStatus ();
		SuccessRate = 0;
		rank = 0;
		gemRequired = "Common";
	}

	public void Save(){
		PlayerPrefs.SetFloat (name + "damage" + GameData.tesId,damage);
		PlayerPrefs.SetInt (name + "rank" + GameData.tesId,rank);
		weaponStats.SaveStatus (name+"weapon");
	}

	public void Load ()
	{
		damage = PlayerPrefs.GetFloat (name + "damage" + GameData.tesId);
		rank = PlayerPrefs.GetInt (name + "rank" + GameData.tesId);
		weaponStats.LoadStatus(name+"weapon");
	}

	public float Damage {
		get {
			return damage;
		}set{
			damage = value;		
		}
	}

	public float Range {
		get {
			return range;
		}
		set {
			range = value;
		}
	}

	public void Upgrade(UnitStatus s){
		rank++;
		weaponStats.Str += s.Str;
		weaponStats.Agi += s.Agi;
		weaponStats.Vit += s.Vit;
		if (rank == 1 || rank == 2) {
			damage += 2;
			if ( rank == 2 )
				gemRequired = "Uncommon";
		}
		if (rank == 3 || rank == 4) {
				damage += 3;
			if ( rank == 4 )
				gemRequired = "Rare";
		}
		if (rank == 5 || rank == 6) {
				damage += 6;
			if ( rank == 6 )
				gemRequired = "Mythical";
		}
		if (rank == 7 || rank == 8) {
			damage += 10;
			if ( rank == 8 )
				gemRequired = "Legendary";
		}
		if (rank == 9 || rank == 10)
			damage += 15;
	}

	public bool CheckUpgradeReq(string grade){
		bool ret = false;
		if (rank == 0 || rank == 1)
			if ( grade.Contains("Common"))
						ret = true;
		if (rank == 2 || rank == 3)
			if ( grade.Contains("Uncommon"))
				ret = true;
		if (rank == 4 || rank == 5)
			if ( grade.Contains("Rare"))
				ret = true;
		if (rank == 6 || rank == 7)
			if ( grade.Contains("Mythical"))
				ret = true;
		if (rank == 8 || rank == 9)
			if ( grade.Contains("Legendary"))
				ret = true;
		return ret;
	}

	public string SoundEffectName {
		get {
			return soundEffectName;
		}
	}

	public int CheckMinLevelReq(){
		return (rank + 1) * 3;
	}

	public int Rank {
		get {
			return rank;
		}
		set {
			rank = value;
		}
	}

	public UnitStatus WeaponStats {
		get {
			return weaponStats;
		}
		set {
			weaponStats = value;
		}
	}
}

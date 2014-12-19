using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
	public class Weapon : Item {

	private float damage;
	private float range;
	private int rank;
	private UnitStatus weaponStats; // MAX STATS 50
	private string[] gemRequired;
	private string soundEffectName;
	
	//private ArrayList<Item> equippedGem;

	public Weapon(int id,string name,string soundEffName, float damage, float range):
		base(id,name){
		this.damage = damage;
		this.range = range;
		soundEffectName = soundEffName;
		weaponStats = new UnitStatus ();
		SuccessRate = 0;
		rank = 0;
		gemRequired = new string[10]{"Common","Common","Uncommon","Uncommon","Rare","Rare"
			,"Mythical","Mythical","Legendary","Legendary"};
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
		int nextRank = rank+1;
		if (nextRank == 1 || nextRank == 2) {
			damage += 1;
		}
		else if (nextRank == 3 || nextRank == 4) {
			damage += 2;
		}
		else if (nextRank == 5 || nextRank == 6) {
			damage += 5;
		}
		else if (nextRank == 7 || nextRank == 8) {
			damage += 7;
		}
		else if (nextRank > 8 )
			damage += 10;
		rank++;
		weaponStats.Str += s.Str;
		weaponStats.Agi += s.Agi;
		weaponStats.Vit += s.Vit;
	
	}

	public bool CheckUpgradeReq(string grade){
		bool ret = false;
		if (rank == 0 || rank == 1)
			if ( grade == "Common" )
						ret = true;
		else if (rank == 2 || rank == 3)
			if ( grade == "Uncommon" )
				ret = true;
		else if (rank == 4 || rank == 5)
			if ( grade == "Rare" )
				ret = true;
		else if (rank == 6 || rank == 7)
			if ( grade == "Mythical" )
				ret = true;
		else if (rank == 8 || rank == 9)
			if ( grade == "Legendary" )
				ret = true;
		return ret;
	}

	public string SoundEffectName {
		get {
			return soundEffectName;
		}
	}

	public int CheckMinLevelReq(){
		return (rank) * 2 + 1;
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

	public string GemRequired {
		get {
			return gemRequired[rank];
		}
	}
}

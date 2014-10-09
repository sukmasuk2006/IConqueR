﻿using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class Weapon : Item {

	private float damage;
	private float range;
	private int rank;
	private UnitStatus weaponStats;
	//private ArrayList<Item> equippedGem;

	public Weapon(int id,string name,float damage, float range):
		base(id,name){
		this.damage = damage;
		this.range = range;
		weaponStats = new UnitStatus ();
		SuccessRate = 0;
		rank = 0;
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
		if (rank == 1 || rank == 2)
			damage += 2;
		if (rank == 3 || rank == 4)
			damage += 3;
		if (rank == 5 || rank == 6)
			damage += 6;
		if (rank == 7 || rank == 8)
			damage += 10;
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

using UnityEngine;
using System.Collections.Generic;

public class Weapon : Item {

	private float damage;
	private float range;
	private int rank;
	
	//private ArrayList<Item> equippedGem;

	public Weapon(string name,float damage, float range):
		base(name){
		sprites = (Sprite)Resources.Load ("Sprite/Weapon/" + name.Trim (), typeof(Sprite));
		this.damage = damage;
		this.range = range;
		SuccessRate = 0;
		rank = 0;
	}
	
	public float Damage {
		get {
			return damage;
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

	public void Upgrade(){
		rank++;
		if (rank == 1 || rank == 2)
			damage += 5;
		if (rank == 3 || rank == 4)
			damage += 10;
		if (rank == 5 || rank == 6)
			damage += 15;
		if (rank == 7 || rank == 8)
			damage += 20;
		if (rank == 9 || rank == 10)
			damage += 30;
	}

	public bool CheckUpgradeReq(string grade){
		bool ret = false;
		if (rank == 0 || rank == 1)
			if ( grade.Contains("common"))
						ret = true;
		if (rank == 2 || rank == 3)
			if ( grade.Contains("uncommon"))
				ret = true;
		if (rank == 4 || rank == 5)
			if ( grade.Contains("rare"))
				ret = true;
		if (rank == 6 || rank == 7)
			if ( grade.Contains("mythical"))
				ret = true;
		if (rank == 8 || rank == 9)
			if ( grade.Contains("legendary"))
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
}

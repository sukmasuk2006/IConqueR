using UnityEngine;
using System.Collections;

public class Weapon : Item {

	private float damage;
	private int slot;
	//private ArrayList<Item> equippedGem;

	public Weapon(string name):
		base(name){
//		this.damage = damage;/
//		this.slot = slot;
	}
	
	public float Damage {
		get {
			return damage;
		}
	}

	public int Slot {
		get {
			return slot;
		}
	}
}

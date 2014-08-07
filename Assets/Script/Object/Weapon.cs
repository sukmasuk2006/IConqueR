using UnityEngine;
using System.Collections;

public class Weapon : Item {

	private float damage;
	private int slot;
	//private ArrayList<Item> equippedGem;

	public Weapon(string name,int damage, int slot, float str,float agi,float intel,float dex,float vit):
		base(name,str,agi,intel,dex,vit){
		this.damage = damage;
		this.slot = slot;
	}
	
}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Gem : Item {

	private int gemId;
	private int grade;
	//private ArrayList<int> requiremets;

	public Gem(string name,int damage, int slot, float str,float agi,float intel,float dex,float vit):
		base(name,str,agi,intel,dex,vit){

	}
}

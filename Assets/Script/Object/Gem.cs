using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Gem : Item {

	private int grade;
	//private ArrayList<int> requiremets;

	public Gem(string name):
		base(name){
		InitializeGem ();
	}

	private void InitializeGem(){
		string[] linesFromFile = null;
		TextAsset txt = (TextAsset)Resources.Load ("Data/Item/"+ name.Trim(), typeof(TextAsset));
		string content = txt.text;
		linesFromFile = content.Split ("\n" [0]);
		desc = linesFromFile [0];
		Price = int.Parse(linesFromFile[1]);
		stats = new UnitStatus ();
		stats.Str =  int.Parse( linesFromFile [2]);
		stats.Agi = int.Parse (linesFromFile [3]);
		stats.Vit = int.Parse (linesFromFile [4]);
	}
	public int Grade {
		get {
			return grade;
		}
	}


}

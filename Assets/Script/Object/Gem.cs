using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Gem : Item {

	private string grade;
	protected UnitStatus stats;
	//private ArrayList<int> requiremets;

	public Gem(string name):
		base(name){
		InitializeGem ();
	}

	private void InitializeGem(){
		string[] linesFromFile = null;
		TextAsset txt = (TextAsset)Resources.Load ("Data/Gem/"+ name.Trim(), typeof(TextAsset));
		Debug.Log ("added " + name);
		string content = txt.text;
		sprites = (Sprite)Resources.Load ("Sprite/Gems/" + name.Trim (), typeof(Sprite));
		linesFromFile = content.Split ("\n" [0]);
		grade = linesFromFile [0];
		Price = int.Parse(linesFromFile[1]);
		stats = new UnitStatus ();
		stats.Str =  int.Parse( linesFromFile [2]);
		stats.Agi = int.Parse (linesFromFile [3]);
		stats.Vit = int.Parse (linesFromFile [4]);
		SuccessRate = float.Parse(linesFromFile[5]);
	}
	public string Grade {
		get {
			return grade;
		}
	}

	public UnitStatus Stats {
		get {
			return stats;
		}
		set {
			stats = value;
		}
	}


}

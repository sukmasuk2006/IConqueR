using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class Gem : Item {

	private string grade;
	protected UnitStatus stats;
	//private ArrayList<int> requiremets;

	public Gem(int id):
		base(id,""){
		InitializeGem ();
	}

	private void InitializeGem(){
		string[] linesFromFile = null;
		TextAsset txt = (TextAsset)Resources.Load ("Data/Gem/"+ id, typeof(TextAsset));
		string content = txt.text;
		linesFromFile = content.Split ("\n" [0]);
		name = linesFromFile[0].Trim();
		grade = linesFromFile [1].Trim();
		Price = int.Parse(linesFromFile[2]);
		PriceType = int.Parse (linesFromFile [3]);
		stats = new UnitStatus ();
		stats.Str =  int.Parse( linesFromFile [4]);
		stats.Agi = int.Parse (linesFromFile [5]);
		stats.Vit = int.Parse (linesFromFile [6]);
		SuccessRate = float.Parse(linesFromFile[7]);
//		Debug.Log ("added " + name + " rate " + SuccessRate);
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

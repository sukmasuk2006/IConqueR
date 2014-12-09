using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class Gem : Item {

	private string grade;
	protected UnitStatus stats;
	//private ArrayList<int> requiremets;

	public Gem(int id, string name):
		base(id,name){
		InitializeGem ();
	}

	private void InitializeGem(){
		string[] linesFromFile = null;
		TextAsset txt = (TextAsset)Resources.Load ("Data/Gem/"+ name.Trim(), typeof(TextAsset));
		string content = txt.text;
		linesFromFile = content.Split ("\n" [0]);
		grade = linesFromFile [0].Trim();
		Price = int.Parse(linesFromFile[1]);
		PriceType = int.Parse (linesFromFile [2]);
		stats = new UnitStatus ();
		stats.Str =  int.Parse( linesFromFile [3]);
		stats.Agi = int.Parse (linesFromFile [4]);
		stats.Vit = int.Parse (linesFromFile [5]);
		SuccessRate = float.Parse(linesFromFile[6]);
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

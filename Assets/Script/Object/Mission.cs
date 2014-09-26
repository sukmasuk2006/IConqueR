//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
using UnityEngine;
using System.Collections.Generic;
public class Mission
{
	private string name;
	private bool isOpen;
	private bool isComplete;
	private int expReward;
	private int goldReward;
	private int diamondReward;
	private int maxReward;
	private List<Unit> enemyList;

	public Mission (string name)
	{
		this.name = name;
		InitializeMission ();
	}

	private void InitializeMission(){
		enemyList = new List<Unit> ();
	//	Debug.Log ("nama " + name);
		TextAsset txt = (TextAsset)Resources.Load ("Data/Mission/"+name.Trim(), typeof(TextAsset));
		string content = txt.text;
		string[] linesFromFile = content.Split ("\n" [0]);
		expReward = int.Parse(linesFromFile [0]);
		goldReward = int.Parse(linesFromFile [1]);
		diamondReward = int.Parse(linesFromFile [2]);
		maxReward = int.Parse (linesFromFile [3]);
//		Debug.Log ("misi " + name);
		for (int i = 4; i < linesFromFile.Length; i++) {
			enemyList.Add(new Unit(i,linesFromFile[i].Trim()));		
			//Debug.Log ("musuh " + linesFromFile[i].Trim());
		}
	}

	public int GetReward{
		get{
			int rew = Random.Range(0,maxReward);
			return rew;
			}
	}

	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}

	public bool IsOpen {
		get {
			return isOpen;
		}
		set {
			isOpen = value;
		}
	}

	public bool IsComplete {
		get {
			return isComplete;
		}
		set {
			isComplete = value;
		}
	}

	public int ExpReward {
		get {
			return expReward;
		}
		set {
			expReward = value;
		}
	}

	public int GoldReward {
		get {
			return goldReward;
		}
		set {
			goldReward = value;
		}
	}

	public int DiamondReward {
		get {
			return diamondReward;
		}
		set {
			diamondReward = value;
		}
	}

	public List<Unit> EnemyList {
		get {
			return enemyList;
		}
		set {
			enemyList = value;
		}
	}

	public int MaxReward {
		get {
			return maxReward;
		}
		set {
			maxReward = value;
		}
	}
}

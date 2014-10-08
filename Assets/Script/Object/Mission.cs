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
[System.Serializable]
public class Mission
{
	private int id;

	public int Id {
		get {
			return id;
		}
	}

	private string name;
	private bool isOpen;
	private bool isComplete;
	private int expReward;
	private int goldReward;
	private int diamondReward;
	private int maxReward;
	private List<Unit> enemyList;
	private string enemyListName;

	public string EnemyListName {
		get {
			return enemyListName;
		}
	}

	public Mission (int id)
	{
		this.id = id;
		InitializeMission ();
	}

	private void InitializeMission(){
		enemyList = new List<Unit> ();
		Debug.Log ("nama " + id);
		enemyListName = "";
		TextAsset txt = (TextAsset)Resources.Load ("Data/Mission/"+id, typeof(TextAsset));
		string content = txt.text;
		string[] linesFromFile = content.Split ("\n" [0]);
		name = linesFromFile [0];
		expReward = int.Parse(linesFromFile [1]);
		goldReward = int.Parse(linesFromFile [2]);
		diamondReward = int.Parse(linesFromFile [3]);
		maxReward = int.Parse (linesFromFile [4]);
		for (int i = 5; i < linesFromFile.Length; i++) {
			enemyList.Add(new Unit(i,linesFromFile[i].Trim()));		
			enemyListName += linesFromFile[i].Trim() + " ";
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


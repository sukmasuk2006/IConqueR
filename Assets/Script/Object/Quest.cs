//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
[System.Serializable]
public class Quest
{
	private int id;
	private string name;
	private string target;
	private int currentQuantity;
	private int quantityNeeded;
	private bool isCompleted;
	private bool isRewardTaken;
	private int rewardMoney;
	private int rewardDiamond;

	public Quest (int id,string trg)
	{
		this.id = id;
		target = trg;
		currentQuantity = 0;
		isCompleted = false;
		InitializeQuest ();
	}

	private void InitializeQuest(){
		TextAsset txt = (TextAsset)Resources.Load ("Data/Quest/" + target.Trim(), typeof(TextAsset));
		string content = txt.text;
		string[] linesFromFile = content.Split ("\n" [0]);


		target = linesFromFile [0];
		quantityNeeded = int.Parse (linesFromFile [1]);
		rewardMoney = int.Parse (linesFromFile [2]);
		rewardDiamond = int.Parse (linesFromFile [3]);
	}

	public int Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}

	public string Target {
		get {
			return target;
		}
		set {
			target = value;
		}
	}

	public int CurrentQuantity {
		get {
			return currentQuantity;
		}
		set {
			currentQuantity = value;
		}
	}

	public int QuantityNeeded {
		get {
			return quantityNeeded;
		}
		set {
			quantityNeeded = value;
		}
	}

	public bool IsCompleted {
		get {
			return isCompleted;
		}
		set {
			isCompleted = value;
		}
	}


	public int RewardMoney {
		get {
			return rewardMoney;
		}
		set {
			rewardMoney = value;
		}
	}

	public int RewardDiamond {
		get {
			return rewardDiamond;
		}
		set {
			rewardDiamond = value;
		}
	}

	public bool IsRewardTaken {
		get {
			return isRewardTaken;
		}
		set {
			isRewardTaken = value;
		}
	}
}



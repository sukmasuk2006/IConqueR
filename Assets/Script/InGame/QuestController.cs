using UnityEngine;
using System.Collections;

public class QuestController : MonoBehaviour {

	public TextMesh descOne;
	public TextMesh descTwo;
	public TextMesh rewardOne;
	public TextMesh rewardTwo;
	public GameObject buttonOne;
	public GameObject buttonTwo;
	public ScreenData data;
	public TextMesh completedOne;
	public TextMesh completedTwo;
	public TextMesh quantyOne;
	public TextMesh quantyTwo;

	Quest one, two;

	// Use this for initialization
	void Start () {
	//	rendererOne.sprite =
		GameData.UpdateGoldQuest ();
		SetQuest ();
	}

	public void SetQuest(){
		buttonOne.SetActive (false);
		buttonTwo.SetActive (false);
		completedOne.text = completedTwo.text = "";

		one = GameData.profile.questList [(data.corridorState*2)+0];
		two = GameData.profile.questList [(data.corridorState*2)+1];
		
		descOne.text = SetDesc (one.QuantityNeeded,one.Target.Trim());
		descTwo.text = SetDesc (two.QuantityNeeded,two.Target.Trim());
		
		rewardOne.text = one.RewardMoney.ToString() + "\n\n" + one.RewardDiamond.ToString();
		rewardTwo.text = two.RewardMoney.ToString() + "\n\n" + two.RewardDiamond;

		quantyOne.text = one.CurrentQuantity + " / " + one.QuantityNeeded;
		quantyTwo.text = two.CurrentQuantity + " / " + two.QuantityNeeded;

//		Debug.Log ("quest ke  1 id" + one.Id);
		if (one.IsCompleted)
				if (!one.IsRewardTaken)
						buttonOne.SetActive (true);
				else
						completedOne.text = "Completed!";
		if (two.IsCompleted )
			if ( !two.IsRewardTaken)
						buttonTwo.SetActive (true);
		else {
			completedTwo.text = "Completed!";
				}
	}

	private string SetDesc(int num, string name){
		string ret;
		switch (name) {
		case "defeat" :  ret = "Defeat\n"+num + "\nenemy armies!";
			break;
		case "fortress" : ret = "Destroy\n"+num + "\nenemy fortress!";
			break;
		case "castle" :  ret = "Conquer\n"+num + "\nenemy Castle";
			break;
		case "gold" :  ret = "Have total\n"+num + "\nGold!";
			break;
		default :ret = "";
			break;
		}
		return ret;
	}

	// Update is called once per frame
	void Update () {
	
	}
}

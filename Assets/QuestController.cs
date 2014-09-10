using UnityEngine;
using System.Collections;

public class QuestController : MonoBehaviour {

	public SpriteRenderer rendererOne;
	public SpriteRenderer rendererTwo;
	public TextMesh descOne;
	public TextMesh descTwo;
	public TextMesh rewardOne;
	public TextMesh rewardTwo;
	public ScreenData data;
	Quest one, two;

	// Use this for initialization
	void Start () {
	//	rendererOne.sprite =
		SetQuest ();
	}

	public void SetQuest(){
		one = GameData.questList [(data.corridorState*2)+0];
		two = GameData.questList [(data.corridorState*2)+1];
		
		descOne.text = SetDesc (one.QuantityNeeded,one.Target.Trim());
		descTwo.text = SetDesc (two.QuantityNeeded,two.Target.Trim());
		
		rewardOne.text = one.RewardMoney.ToString();
		rewardTwo.text = two.RewardMoney.ToString();
	}

	private string SetDesc(int num, string name){
		string ret;
		switch (name) {
		case "defeat" :  ret = "Defeat "+num + " enemy armies!";
			break;
		case "fortress" : ret = "Destroy "+num + " enemy fortress!";
			break;
		case "kingdom" :  ret = "Conquer "+num + " enemy Kingdoms!";
			break;
		case "unlock" :  ret = "Unlock "+num + " of your troopers!!";
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

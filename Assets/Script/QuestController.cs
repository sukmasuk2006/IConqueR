using UnityEngine;
using System.Collections;

public class QuestController : MonoBehaviour {

	public SpriteRenderer rendererOne;
	public SpriteRenderer rendererTwo;
	public TextMesh descOne;
	public TextMesh descTwo;
	public TextMesh rewardOne;
	public TextMesh rewardTwo;
	public GameObject buttonOne;
	public GameObject buttonTwo;
	public ScreenData data;
	Quest one, two;

	// Use this for initialization
	void Start () {
	//	rendererOne.sprite =
		SetQuest ();
	}

	public void SetQuest(){
		buttonOne.SetActive (false);
		buttonTwo.SetActive (false);

		one = GameData.questList [(data.corridorState*2)+0];
		two = GameData.questList [(data.corridorState*2)+1];
		
		descOne.text = SetDesc (one.QuantityNeeded,one.Target.Trim());
		descTwo.text = SetDesc (two.QuantityNeeded,two.Target.Trim());
		
		rewardOne.text = one.RewardMoney.ToString();
		rewardTwo.text = two.RewardMoney.ToString();

		Debug.Log ("quest ke  1" + one.IsCompleted);
		if (one.IsCompleted && !one.IsRewardTaken)
						buttonOne.SetActive (true);
		if (two.IsCompleted && !two.IsRewardTaken)
						buttonTwo.SetActive (true);
	}

	private string SetDesc(int num, string name){
		string ret;
		switch (name) {
		case "defeat" :  ret = "Defeat "+num + " enemy armies!";
			break;
		case "fortress" : ret = "Destroy "+num + " enemy fortress!";
			break;
		case "castle" :  ret = "Demolish "+num + " enemy Kingdoms!";
			break;
		case "gold" :  ret = "Have total "+num + " Gold!";
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

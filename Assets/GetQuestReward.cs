using UnityEngine;
using System.Collections;

public class GetQuestReward : MonoBehaviour {

	public int slot;
	public ScreenData data;
	public ProfileController profile;
	Quest q;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnMouseDown(){
		q = GameData.questList [(data.corridorState*2)+slot];
		GameData.profile.Gold += q.RewardMoney;
		GameData.profile.Diamond += q.RewardDiamond;
		q.IsRewardTaken = true;
		profile.UpdateGoldAndDiamond ();
		gameObject.SetActive (false);
	}
}

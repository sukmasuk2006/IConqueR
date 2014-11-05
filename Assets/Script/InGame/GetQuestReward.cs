using UnityEngine;
using System.Collections;

public class GetQuestReward : MonoBehaviour {

	public int slot;
	public ScreenData data;
	public ProfileController profile;
	Quest q;
	public TextMesh teks;
	public AudioClip sound;
	private QuestController questController;
	// Use this for initialization
	void Start () {
		questController = GameObject.Find("QuestScreen").GetComponent<QuestController>();
	}
	
	void OnMouseDown(){
		q = GameData.profile.questList [(data.corridorState*2)+slot];
		q.IsRewardTaken = true;
		profile.UpdateGoldAndDiamond (0,-q.RewardMoney);
		profile.UpdateGoldAndDiamond (1,-q.RewardDiamond);
		profile.CheckIsCompletedAchievement();
		gameObject.SetActive (false);
		teks.text = "Completed!";
		questController.SetQuest();
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);

	}
}

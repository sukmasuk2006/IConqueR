using UnityEngine;
using System.Collections;

public class MissonSetter : MonoBehaviour {

	public TextMesh name;

	public int curr;
	public string missionType; // fortress/castle
	public TextMesh confirmText1;
	public TextMesh confirmText2;
	public EnemySetter setter;
	public int enemyLeaderTitle;
	private SpriteRenderer render;

	// Use this for initialization
	void Start () {
		if (curr > GameData.profile.NextMission) {
			this.gameObject.SetActive(false);
		}
		render = gameObject.GetComponent<SpriteRenderer> ();
//		Debug.Log("CURR " + curr);
		name.text = GameData.missionList [curr].Name;
		if (curr == GameData.profile.NextMission) {
			if (missionType == "Camp" )
				this.render.sprite = (Sprite)Resources.Load ("Sprite/Button/icon_attack_yellow", typeof(Sprite));
			else if (missionType == "Fortress")
				this.render.sprite = (Sprite)Resources.Load ("Sprite/Button/icon_fortress_yellow", typeof(Sprite));
			else if ( missionType == "Castle")
				this.render.sprite = (Sprite)Resources.Load ("Sprite/Button/icon_crown_yellow", typeof(Sprite));
		}
	}

	void OnMouseUp(){
		SetInformation ();
		GameData.missionList [curr].Title = enemyLeaderTitle;
		GameData.missionType = missionType;
		GameData.currentMission = curr;
	}

	void SetInformation(){
		confirmText1.text = "Attack "+ GameData.missionList[curr].Name +" " + missionType +" ?";
		//confirmText2.text = "Enemy list :" + GameData.missionList[curr].EnemyListName;
//		Debug.Log ("Update slot 0");

		setter.UpdateSlot (GameData.missionList [curr]);
	}
}

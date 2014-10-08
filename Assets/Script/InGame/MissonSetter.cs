using UnityEngine;
using System.Collections;

public class MissonSetter : MonoBehaviour {

	public TextMesh name;

	public int curr;
	public string missionType; // fortress/castle
	public TextMesh confirmText1;
	public TextMesh confirmText2;
	public EnemySetter setter;
	// Use this for initialization
	void Start () {
		name.text = GameData.missionList [curr].Name;
		//if (curr > GameData.profile.NextMission) {
	//		this.gameObject.SetActive(false);
		//}
	}

	// pake int, soalnya udah urut

	void OnMouseDown(){
		SetInformation ();
		GameData.missionType = missionType;
		GameData.currentMission = curr;
	}

	void SetInformation(){
		confirmText1.text = "Attack "+ GameData.missionList[curr].Name +" " + missionType +" ?";
		//confirmText2.text = "Enemy list :" + GameData.missionList[curr].EnemyListName;
		Debug.Log ("Update slot 0");

		setter.UpdateSlot (GameData.missionList [curr]);
	}
}

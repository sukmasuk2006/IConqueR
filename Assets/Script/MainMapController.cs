using UnityEngine;
using System.Collections;

public class MainMapController : MonoBehaviour {

	// panjang timer 160 px.

	public GameObject RaidNotification;
	public GameObject MainMapScreen;
	//public GameObject raidTimer;
	private float raidTimerYPos;
	private float timerMovespeed;

	// Use this for initialization
	void Start () {
		GameData.gameState = GameConstant.MAP_STATE;
		//raidTimer.transform.position = GameData.raidTimePos;
		//raidTimerYPos = raidTimer.transform.position.y;

		AutoSaveData ();
	//	iTween.MoveTo (zombie_timer, iTween.Hash ("position", new Vector3 (-2,zombie_timer.transform.position.y,-1), "time",GameData.raidTime,"EaseType","linear"));
	}

	void AutoSaveData(){
		PlayerPrefs.SetInt ("gold", GameData.gold);
		PlayerPrefs.SetInt ("diamond", GameData.diamond);
	}
	// Update is called once per frame
	void Update () {
		UpdateRaidTime ();
		CheckRaid ();
	}
	private void UpdateRaidTime(){
		if (GameData.gameState == GameConstant.MAP_STATE) {
			GameData.UpdateRaidTime();
			//raidTimer.transform.position = new Vector2(raidTimer.transform.position.x-timerMovespeed,raidTimerYPos);	
			//GameData.raidTimePos = raidTimer.transform.position;
		} 
	}

	private void CheckRaid(){
		if (GameData.raidTime <= 0) {
			iTween.MoveTo (RaidNotification, iTween.Hash ("position", new Vector3 (0, 0, -1), "time", 1.0f));
			iTween.MoveTo (MainMapScreen, iTween.Hash ("position", new Vector3 (0, 12f, -1), "time", 1.0f));
			GameData.gameState = GameConstant.RAID_NOTIFICATION;
		}
	}
}


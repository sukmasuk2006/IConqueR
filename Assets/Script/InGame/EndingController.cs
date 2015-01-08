using UnityEngine;
using System.Collections;

public class EndingController : MonoBehaviour {

	public Camera camera;
	public AudioClip sound;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnMouseDown(){
		if (GameData.readyToTween) {
			MusicManager.getMusicEmitter ().audio.PlayOneShot (sound);
			if ( GameData.profile.NextMission == 50 ){
				GameData.gameState = "EndingScene";
				camera.GetComponent<ScreenFader> ().FadeOut ("EndingScene");
			}
			else{
				GameData.gameState = "HomeScene";
				camera.GetComponent<ScreenFader> ().FadeOut ("HomeScene");

			}
			GameData.SaveData();
		}
	}
}

using UnityEngine;
using System.Collections;

public class SimpleButton : MonoBehaviour {

	public string targetScene;
	public Camera camera;
	public bool notCoveredByAnyScreen = true;
	public AudioClip sound;
	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown(){
		if (GameData.readyToTween) {
						MusicManager.getMusicEmitter ().audio.PlayOneShot (sound);
						camera.GetComponent<ScreenFader> ().FadeOut (targetScene);
						GameData.gameState = targetScene;
				}
	}
}

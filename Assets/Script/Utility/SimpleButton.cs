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
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);
		camera.GetComponent<ScreenFader> ().FadeOut (targetScene);
		GameData.gameState = targetScene;
	}
}

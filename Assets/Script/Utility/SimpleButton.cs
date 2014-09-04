using UnityEngine;
using System.Collections;

public class SimpleButton : MonoBehaviour {

	public string targetScene;
	public Camera camera;
	public bool notCoveredByAnyScreen = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		camera.GetComponent<ScreenFader> ().FadeOut (targetScene);
		GameData.gameState = targetScene;
	}
}

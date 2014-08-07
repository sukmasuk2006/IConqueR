using UnityEngine;
using System.Collections;

public class StoryController : MonoBehaviour {

	public Texture skipButtonTex;
	// Use this for initialization
	void Start () {
		
	}

	void OnGUI(){
		if (GUI.Button (new Rect (500, 0, skipButtonTex.width, skipButtonTex.height), skipButtonTex, "label")) {
					 Camera.main.GetComponent<ScreenFader>().FadeOut("MainMapScene");
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

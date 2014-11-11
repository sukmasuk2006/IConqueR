using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	private ScreenFader fader;

	void Start () {
		fader = Camera.main.GetComponent<ScreenFader>();
	}
	
	void OnMouseDown(){
		if ( GameData.profile.StoryCompleted )
			fader.FadeOut("HomeScene");
		else
			fader.FadeOut("StoryScene");
	}


}

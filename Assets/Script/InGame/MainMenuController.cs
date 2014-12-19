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
		else{
			GameData.profile.inventoryList.Add(GameData.shopList[3]); // nambah gem ketika ulang
			fader.FadeOut("StoryScene");
		}
		GameData.SaveData();
	}


}

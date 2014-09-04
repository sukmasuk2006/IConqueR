using UnityEngine;
using System.Collections;

public class HeroProfileSetter : MonoBehaviour {

	public int id;
	public string name;
	public Camera camera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (GameData.unitList [id].IsUnlocked) {
						GameData.selectedToViewProfileId = id;
						GameData.selectedToViewProfileName = name;
						camera.GetComponent<ScreenFader> ().FadeOut ("HeroProfileScene");
						GameData.gameState = "HeroProfileScene";
				}
	}
}

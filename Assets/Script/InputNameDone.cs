using UnityEngine;
using System.Collections;

public class InputNameDone : MonoBehaviour {

	public GameObject tweenedObject;
	public GameObject removedObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
		if (!InputNameHandler.isDone) {
			InputNameHandler.isDone = true;
			PlayerPrefs.SetInt("level",GameData.currentLevel);
			PlayerPrefs.SetFloat("exp",GameData.currentExp);
			PlayerPrefs.SetInt("gold",GameData.gold);
			PlayerPrefs.SetInt("diamond",GameData.diamond);

			iTween.MoveTo (tweenedObject, iTween.Hash ("position", removedObject.transform.position, "time", 0.5f));
			iTween.MoveTo (removedObject, tweenedObject.transform.position,0.5f);

		}
	}
}

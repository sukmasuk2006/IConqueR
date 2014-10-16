using UnityEngine;
using System.Collections;

public class Unpause : MonoBehaviour {

	public GameObject obj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && GameData.readyToTween && GameData.gameState == "Paused") {
			UnPause();
		}
	}

	void OnMouseDown(){
		UnPause ();	
	}

	void UnPause(){
		iTween.MoveTo ( obj,iTween.Hash("position",new Vector3(0,-12,-3),"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
		//sound.audio.PlayOneShot (sound.audio.clip);
		GameData.readyToTween = false;	
	}

	void ReadyTween(){
		GameData.gameState = "";	
		GameData.readyToTween = true;
	}
}

using UnityEngine;
using System.Collections;

public class LogoSceneController : MonoBehaviour {

	private const int DELAY = 2;
	public GameObject tweenedObject;
	public GameObject removedObject;
	public float time;
	// Use this for initialization
	void Start () {
	//	Debug.Log ("di logo " + GameData.isFirstPlay);
	//	if (GameData.isFirstPlay) {
		//	TweenInputName();
		//}
		//else
			TweenMainMenu ();
		GameMusic.PlayMusic ("Music/royal");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*private void TweenInputName(){
		iTween.MoveTo (inputNameScreen, iTween.Hash ("delay", 1f, "position", removedObject.transform.position, "time", time));
		iTween.MoveTo (removedObject, iTween.Hash ("delay", 1f, "position", inputNameScreen.transform.position, "time", time));
	}*/

	public void TweenMainMenu(){

	//	Debug.Log ("GO");
		
		iTween.MoveTo (tweenedObject, iTween.Hash ("delay",1f,"position", removedObject.transform.position, "time",time));
		iTween.MoveTo (removedObject, iTween.Hash ("delay",1f,"position", tweenedObject.transform.position, "time", time,"EaseType","linear"));
	}

	void MoveTarget(){
	
	}


}

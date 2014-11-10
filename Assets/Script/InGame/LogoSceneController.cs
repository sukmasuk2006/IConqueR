using UnityEngine;
using System.Collections;

public class LogoSceneController : MonoBehaviour {

	private const int DELAY = 2;
	public GameObject tweenedObject;
	public GameObject removedObject;
	public Vector3 LogoTarget;
	public Vector3 buttonTarget;
	public GameObject button;
	public float time;
	public AudioClip dor;
	// Use this for initialization
	void Start () {
	
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
		if (GameData.readyToTween) {
						GameData.readyToTween = false;
						//	Debug.Log ("GO");
						iTween.ColorTo (removedObject, iTween.Hash ("delay", 0f, "a", 0f, "time", time, "EaseType", "linear"));
						iTween.MoveTo (removedObject, iTween.Hash ("delay", 3f, "position", new Vector3 (0, 0, 5), "time", time, "EaseType", "linear"));

						iTween.MoveTo (tweenedObject, iTween.Hash ("delay", 2f, "position", LogoTarget, "time", time, "EaseType", "linear"
						));
						iTween.MoveTo (button, iTween.Hash ("delay", 2f, "position", buttonTarget, "time", time, "EaseType", "linear"
		                                           , "onComplete", "ReadyTween", "onCompleteTarget", gameObject));
				}
	}

	void ReadyTween(){
		MusicManager.getMusicPlayer().audio.PlayOneShot(dor);
		GameData.readyToTween = true;
	}


}

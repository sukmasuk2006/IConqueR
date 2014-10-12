using UnityEngine;
using System.Collections;

public class LogoSceneController : MonoBehaviour {

	private const int DELAY = 2;
	public GameObject tweenedObject;
	public GameObject removedObject;
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
		GameData.readyToTween = false;
	//	Debug.Log ("GO");
		iTween.ColorTo (removedObject, iTween.Hash ("delay",1f,"a",0f ,"time", time,"EaseType","linear"));
		iTween.MoveTo (removedObject, iTween.Hash ("delay",4f,"position", new Vector3(0,0,3), "time", time,"EaseType","linear"));

		iTween.MoveTo (tweenedObject, iTween.Hash ("delay",3f,"position", removedObject.transform.position, "time",0.1f,"EaseType","linear"
		                                           ,"onComplete", "ReadyTween", "onCompleteTarget", gameObject));
	}

	void ReadyTween(){
		MusicManager.getMusicPlayer().audio.PlayOneShot(dor);
		GameData.readyToTween = true;
	}


}

using UnityEngine;
using System.Collections;

public class FirstPlayChecker : MonoBehaviour {

	public GameObject tweenedObject;
	public GameObject removedObject;
	public Camera camera;
	// Use this for initialization
	public string targetScene;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
		Debug.Log ("pressed");
			if (GameData.isFirstPlay) {
				Debug.Log ("first");
				TweenScale.Begin(removedObject,0.1f,Vector3.zero).onFinished += OnFinishedAlpha;
//				iTween.MoveTo (tweenedObject, iTween.Hash ("position", removedObject.transform.position, "time", 2f));
//				iTween.MoveTo (removedObject, iTween.Hash ("position", tweenedObject.transform.position, "time", 1f,"EaseType","linear"));
				}
				else {
					camera.GetComponent<ScreenFader> ().FadeOut (targetScene);
					Debug.Log ("pressed 2");

		}
	}

	void OnFinishedAlpha (UITweener tween)
	{
		TweenPosition.Begin (tweenedObject, 0.1f, removedObject.transform.position);

	}
}

using UnityEngine;
using System.Collections;

public class LogoSceneController : MonoBehaviour {

	private const int DELAY = 2;
	public GameObject tweenedObject;
	public GameObject removedObject;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public void Go(){
		//TweenPosition.Begin (removedObject, 1, tweenedObject.transform.position);
		
		TweenAlpha.Begin (removedObject,0.5f, 0).onFinished += OnFinishAlpha;
//		TweenAlpha.OnFinished +=  

		Debug.Log ("GO");
//		iTween.MoveTo (tweenedObject, iTween.Hash ("position", removedObject.transform.position, "time", 1f));/
	//	iTween.MoveTo (removedObject, iTween.Hash ("position", tweenedObject.transform.position, "time", 1f,"EaseType","linear"));
	}

	void OnFinishAlpha (UITweener tween)
	{
		TweenPosition.Begin (tweenedObject, 0.1f, removedObject.transform.position);
	}
}

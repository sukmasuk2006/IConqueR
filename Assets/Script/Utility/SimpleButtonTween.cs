using UnityEngine;
using System.Collections;

public class SimpleButtonTween : MonoBehaviour {

	public GameObject tweenedObject;
	public GameObject targetObject;
	public string gameStateRequired;
	public string gameStateTarget;

	public bool isRemoved = true;

	// Use this for initialization
	void Start () {
		Debug.Log ("state " + GameData.gameState);
	}


	// Update is called once per frame
	void Update () {

	}

	void OnClick(){
		Debug.Log ("gamestate " + GameData.gameState);
		if (GameData.gameState.Contains(gameStateRequired)) {
			TweenPosition.Begin (tweenedObject, 0.1f, targetObject.transform.localPosition).method = UITweener.Method.BounceIn;
			GameData.gameState = gameStateTarget;		
		}
	}
}

using UnityEngine;
using System.Collections;

public class SimpleButtonTween : MonoBehaviour {

	public GameObject tweenedObject;
	public GameObject targetObject;
	public string gameStateRequired;
	public string gameStateTarget;
	private Vector3 tempPosition;

	public bool isRemoved = true;

	// Use this for initialization
	void Start () {
	}


	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		//HOTween.To(tweenedObject,0.5f,"position",targetObject.transform.position);
		tempPosition = targetObject.transform.position;
		Debug.Log ("Tween 1");
		if (GameData.readyToTween ) {
			GameData.readyToTween = false;
			iTween.MoveTo ( targetObject,iTween.Hash("position",tweenedObject.transform.position,"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
			Debug.Log ("Tween 2");
			GameData.gameState = gameStateTarget;		
			//HOTween.To( tweenedObject, 8, new TweenParms().Prop( "position", new PlugVector3Path( path ).ClosePath().OrientToPath() ).Loops( -1 ).Ease( EaseType.Linear ).OnStepComplete( PathCycleComplete ) );
			//HOTween.To(tweenedObject.transform,1,
			//           new TweenParms().Prop("position",targetObject.transform.position));                                                          
			//HOTween.To(targetObject.transform,1,
			//           new TweenParms().Prop("position",tweenedObject.transform.position));                                                          
		}
	}

	void OnMouseUp(){
	
	}
	
	void ReadyTween(){
		iTween.MoveTo ( tweenedObject,iTween.Hash("position",tempPosition,"time", 0.1f,"onComplete","ReadyTween2","onCompleteTarget",gameObject));
		Debug.Log ("Tween 3");
	}

	void ReadyTween2(){
		GameData.readyToTween = true;
		Debug.Log ("Oncomplete");
	}
}

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

	void OnMouseDown(){
		Debug.Log ("gamestate " + GameData.gameState);
		//HOTween.To(tweenedObject,0.5f,"position",targetObject.transform.position);
		if (GameData.gameState.Contains(gameStateRequired) && GameData.readyToTween ) {
			GameData.readyToTween = false;
			iTween.MoveTo ( tweenedObject,iTween.Hash("position",targetObject.transform.position,"time", 0.5f,"oncomplete","ReadyTween","oncompletetarget",gameObject));
			iTween.MoveTo (targetObject, tweenedObject.transform.position,0.5f);		
			GameData.gameState = gameStateTarget;		
			//HOTween.To( tweenedObject, 8, new TweenParms().Prop( "position", new PlugVector3Path( path ).ClosePath().OrientToPath() ).Loops( -1 ).Ease( EaseType.Linear ).OnStepComplete( PathCycleComplete ) );
			//HOTween.To(tweenedObject.transform,1,
			//           new TweenParms().Prop("position",targetObject.transform.position));                                                          
			//HOTween.To(targetObject.transform,1,
			//           new TweenParms().Prop("position",tweenedObject.transform.position));                                                          
		}
	}

	void ReadyTween(){
		GameData.readyToTween = true;
	}
}

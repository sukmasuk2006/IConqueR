using UnityEngine;
using System.Collections;

public class ScaleEffect : MonoBehaviour {

	public float newScaleX;
	public float newScaleY;
	public float moveXby;
	public float moveYby;
	private Vector2 moveTarget,scaleTarget,oldPos,oldScale;
	public bool trueMoveFalseScale;
	// Use this for initialization
	void Start () {
		oldPos = transform.position;
		oldScale = transform.localScale;
		moveTarget = new Vector2(newScaleX+transform.position.x,newScaleY+transform.position.y);
		scaleTarget = new Vector2(scaleTarget.x,scaleTarget.y);
		if ( trueMoveFalseScale ) iTween.MoveTo ( gameObject,iTween.Hash("position",moveTarget,"time", 1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
		else iTween.ScaleTo ( gameObject,iTween.Hash("scale",scaleTarget,"time", 1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
	}
	
	// Update is called once per frame
	void ReadyTween() {
		if ( trueMoveFalseScale ) iTween.MoveTo ( gameObject,iTween.Hash("position",oldPos,"time", 1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
		else iTween.ScaleTo ( gameObject,iTween.Hash("scale",oldScale,"time", 1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
	}
}

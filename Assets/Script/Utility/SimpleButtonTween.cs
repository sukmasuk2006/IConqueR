using UnityEngine;
using System.Collections;

public class SimpleButtonTween : MonoBehaviour {

	public GameObject tweenedObject;
	public GameObject targetObject;
	public string gameStateRequired;
	public string gameStateTarget;
	private Vector3 tempPosition;
	public AudioClip sound;

	public bool isRemoved = true;

	// Use this for initialization
	void Start () {
	}


	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){

	}

	void OnMouseUp(){
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);
		//HOTween.To(tweenedObject,0.5f,"position",targetObject.transform.position);
		tempPosition = targetObject.transform.position;
		
		if (GameData.readyToTween ) {
			GameData.readyToTween = false;
			iTween.MoveTo ( targetObject,iTween.Hash("position",tweenedObject.transform.position,"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
			//sound.audio.PlayOneShot (sound.audio.clip);
			GameData.gameState = gameStateTarget;	
			                                         
		}
	}
	
	void ReadyTween(){
		iTween.MoveTo ( tweenedObject,iTween.Hash("position",tempPosition,"time", 0.1f,"onComplete","ReadyTween2","onCompleteTarget",gameObject));
	 
	}

	void ReadyTween2(){
		GameData.readyToTween = true;
	}
}

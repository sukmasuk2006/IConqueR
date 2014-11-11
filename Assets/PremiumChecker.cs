using UnityEngine;
using System.Collections;

public class PremiumChecker : MonoBehaviour {
	
	public GameObject tweenedObject;
	public GameObject targetObject;
	public string gameStateRequired;
	public string gameStateTarget;
	private Vector3 tempPosition;
	public AudioClip sound;
	private ProfileController prof;
	public bool isRemoved = true;
	
	// Use this for initialization
	void Start () {
		try{
			prof = GameObject.Find("Profile").GetComponent<ProfileController>();
		}
		catch{
		}
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
		try {
			prof.CheckIsCompletedAchievement();
		}
		catch{
		}
		Debug.Log (" Klik  " + GameData.readyToTween);
		if (GameData.readyToTween  ) {
			GameData.readyToTween = false;
			if ( GameData.gameState == "ConfirmExit"){
				iTween.MoveTo ( tweenedObject,iTween.Hash("position",new Vector3(0,-12f,0f),"time", 0.1f));
				GameData.gameState = GameData.prevGameState;
				GameData.readyToTween = true;
				Debug.Log("gk jadi exit");
			}
			else {
				iTween.MoveTo ( targetObject,iTween.Hash("position",tweenedObject.transform.position,"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
				//sound.audio.PlayOneShot (sound.audio.clip);
				Debug.Log("what");
				GameData.gameState = gameStateTarget;	
			}                                   
		}
	}
	
	void ReadyTween(){
		iTween.MoveTo ( tweenedObject,iTween.Hash("position",tempPosition,"time", 0.1f,"onComplete","ReadyTween2","onCompleteTarget",gameObject));
		
	}
	
	void ReadyTween2(){
		GameData.readyToTween = true;
	}
}

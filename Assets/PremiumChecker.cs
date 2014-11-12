using UnityEngine;
using System.Collections;

public class PremiumChecker : MonoBehaviour {

	public GameObject nonPremiumObject;
	public GameObject PremiumObject;
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
		Debug.Log (" Klik  " + GameData.readyToTween);
		if (GameData.readyToTween  ) {
			GameData.readyToTween = false;
			if ( GameData.isPremium){
				iTween.MoveTo ( targetObject,iTween.Hash("position",PremiumObject.transform.position,"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
				Debug.Log("gk jadi exit");
			}
			else {
				iTween.MoveTo ( targetObject,iTween.Hash("position",nonPremiumObject.transform.position,"time", 0.1f,"onComplete","ReadyTween2","onCompleteTarget",gameObject));
			}                                   
		}
	}
	
	void ReadyTween(){
		iTween.MoveTo ( PremiumObject,iTween.Hash("position",tempPosition,"time", 0.1f));
		GameData.readyToTween = true;
	}
	
	void ReadyTween2(){
		iTween.MoveTo ( nonPremiumObject,iTween.Hash("position",tempPosition,"time", 0.1f));
		GameData.readyToTween = true;
	}
}

using UnityEngine;
using System.Collections;

public class InventoryCloser : MonoBehaviour {

	public GameObject inventoryScreen;
	public GameObject upgradeScreen;
	public GameObject HomeScreen;
	private Vector3 tempPosition;
	public AudioClip sound;
	private GameObject tweenedObject;
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
		tempPosition = inventoryScreen.transform.position;
		tweenedObject = GameData.gameState == "Sell" ? HomeScreen : 
			upgradeScreen;
	

		if (GameData.readyToTween  ) {
			GameData.readyToTween = false;
			iTween.MoveTo ( inventoryScreen,iTween.Hash("position",tweenedObject.transform.position,"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
			//sound.audio.PlayOneShot (sound.audio.clip);
		//	GameData.gameState = gameStateTarget;	
			
		}
	}
	
	void ReadyTween(){
		iTween.MoveTo ( tweenedObject,iTween.Hash("position",tempPosition,"time", 0.1f,"onComplete","ReadyTween2","onCompleteTarget",gameObject));
		GameData.readyToTween = true;	
	}
	
	void ReadyTween2(){
		GameData.readyToTween = true;
	}
}

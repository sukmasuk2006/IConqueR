using UnityEngine;
using System.Collections;

public class StartUpgrade : MonoBehaviour {

	public GameObject screen;
	public bool button;
	public UpgradeWeaponController controller;
	public AudioClip sound;
	// Use this for initialization
	void Start () {
		
	}

	void OnMouseDown(){
		if (button) {// yes
			controller.StartCrafting();	
	
		} //no
		else {
		}
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);

		iTween.MoveTo ( screen,iTween.Hash("position",new Vector3(0,-12f,-5f),"time", 0.1f,"oncomplete","ReadyTween","oncompletetarget",gameObject));

	}

	void ReadyTween(){
		GameData.readyToTween = true;
		GameData.gameState = "";
	}
}

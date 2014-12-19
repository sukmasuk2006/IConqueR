using UnityEngine;
using System.Collections.Generic;

public class Unpause : MonoBehaviour {

	public BattleController controller;
	public List<HeroController> unitList;
	public GameObject obj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		UnPause ();	
	}

	public void UnPause(){
		Debug.Log("Unpause");
		GameData.readyToTween = false;	
		controller.DeactivateShade(0f,0.1f);
		//sound.audio.PlayOneShot (sound.audio.clip);
		GameData.gameState = "";
		for (int i =0; i < unitList.Count; i++) {
			if ( unitList[i].gameObject.activeInHierarchy )
				unitList[i].CheckState();		
		}

		iTween.MoveTo ( obj,iTween.Hash("position",new Vector3(3.74f,22f,-2f),"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));

	}

	void ReadyTween(){
		GameData.gameState = "";	
		GameData.readyToTween = true;
	}
}

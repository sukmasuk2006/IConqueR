using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeController : MonoBehaviour {

	// Use this for initialization
	
	public TextMesh goldText;
	public TextMesh diamondText;

	void Start () {
		GameData.gameState = "HomeScene";
		diamondText.text = GameData.diamond.ToString ();

	}
	
	// Update is called once per frame
	void Update () {
		goldText.text = GameData.gold.ToString ();

	}

	void SetActiveHeroes(){
		 /*
		Debug.Log ("hero set");
		int index = 0,cleanSlotIndex = 0;
		for (int i = 0; i < 8 ; i++ ) {
			if ( cleanSlotIndex < 5 ) 
				basecampActiveHeroesList[cleanSlotIndex++].sprite = null;

			if ( GameData.selectedHeroes[i]){
				basecampActiveHeroesList[index++].sprite = 
					(Sprite)Resources.Load("Sprite/Character/Hero/"
					                       +GameData.heroesList[i],typeof(Sprite));
				Debug.Log(GameData.heroesList[i]);
				//GameData.basecampActiveHeroesList[index++] = GameData.heroesList[i];
			}
		}*/
	}
}

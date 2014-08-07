using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseCampController : MonoBehaviour {

	public List<SpriteRenderer> basecampActiveHeroesList;
	// Use this for initialization
	void Start () {
		GameData.gameState = "BasecampScene";
		SetActiveHeroes ();
	}
	
	// Update is called once per frame
	void Update () {
	
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

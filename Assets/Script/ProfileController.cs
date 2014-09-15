using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProfileController : MonoBehaviour {

	// Use this for initialization
	
	public TextMesh goldText;
	public TextMesh diamondText;
	public Transform expBar;
	private float scaleAwal = 1.1f;
	private float scale = 0f;
	private float expTujuan;


	void Start () {
		//Debug.Log ("profile contr current gold " + GameData.gold);
		UpdateGoldAndDiamond ();
		expBar.localScale = new Vector3 (scaleAwal * GameData.currentExp / GameData.expList[GameData.currentLevel]
		                                 , expBar.localScale.y,
		                                expBar.localScale.z);
	}

	public void UpdateGoldAndDiamond(){
		diamondText.text = GameData.diamond.ToString ();
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

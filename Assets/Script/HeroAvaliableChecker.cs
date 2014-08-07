using UnityEngine;
using System.Collections;

public class HeroAvaliableChecker : MonoBehaviour {

	public GameObject heroIcon;
	public GameObject checkerIcon;

	public int avaliableAt;
	public float goldRequired;
	public int slot;

	private bool isAvailable = true;
	private bool isSelected;

	// Use this for initialization
	void Start () {
	/*
		if (GameData.unlockedHeroes [slot]) {
			}
		else {
			heroIcon.GetComponent<SpriteRenderer>().enabled = false;			
		}
		if (GameData.selectedHeroes [slot]) {
			isSelected = true;
		}
		else {
			checkerIcon.GetComponent<SpriteRenderer>().enabled = false;	
			isSelected = false;
		}
		*/
		// if gamedata.heroavailabe[count] = true;. heroicon = aktif
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetToAvailable(){
		if (!isAvailable) {
			if ( GameData.gold > goldRequired && !isAvailable ){
			//	heroIcon.SetActive = true;
				isAvailable = true;
			}
		}
	}

	void OnMouseDown(){
		if (isSelected ) {
			if ( GameData.availableHeroes > 1 ){
				isSelected = false;
				checkerIcon.GetComponent<SpriteRenderer>().enabled = false;	
				GameData.availableHeroes--;
				//GameData.selectedHeroes[slot] = false;
			}
		}
		else {
			if ( GameData.availableHeroes < 5 ){
				GameData.availableHeroes++;
				isSelected = true;
				checkerIcon.GetComponent<SpriteRenderer>().enabled = true;	
				//GameData.selectedHeroes[slot] = true;
			}
		}
	}
}

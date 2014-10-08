using UnityEngine;
using System.Collections.Generic;

public class EnemySetter : MonoBehaviour {

	public List<SpriteRenderer> renders;
	// Use this for initialization
	public void UpdateSlot(Mission m){
		Debug.Log ("Update slot");
		for ( int i = 0 ; i < 5 ; i++ ){
			try {
				Debug.Log ("Update slot "+ i +" " +m.EnemyList[i].Job );
				renders[i].sprite = SwitchSprite(m.EnemyList[i].Job);
			}
			catch{
//				Debug.Log ("catch "+ i +" " +m.EnemyList[i].Job );
				renders[i].sprite = null;
			}
		}
	}

	Sprite SwitchSprite(string s){
		Sprite sprite = null;
		switch (s) {
		case "knight" : sprite = GameData.unitIconList[0];break;
		case "warrior" : sprite = GameData.unitIconList[1];break;
		case "archer" : sprite = GameData.unitIconList[2];break;
		case "tribe" : sprite = GameData.unitIconList[3];break;
		case "thief" : sprite = GameData.unitIconList[4];break;
		case "monk" : sprite = GameData.unitIconList[5];break;
		case "assasin" : sprite = GameData.unitIconList[6];break;
		case "hunter" : sprite = GameData.unitIconList[7];break;
		case "ninja" : sprite = GameData.unitIconList[8];break;
		case "ksatria" : sprite = GameData.unitIconList[9];break;

		}
		return sprite;
	}
}

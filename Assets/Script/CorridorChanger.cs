using UnityEngine;
using System.Collections;

public class CorridorChanger : MonoBehaviour {

	public int dir;
	public GameObject controller;
	public ScreenData data;// Use this for initialization

	void Start () {
	
	}

	void OnMouseUp(){
		//dir 1 ++ dir -1 --
		Debug.Log (data.corridorState + " " + data.maxCorridorState);
		if (dir > 0 && data.corridorState < data.maxCorridorState)
			data.corridorState++;
		// geser kiri
		else if ( dir < 0 && data.corridorState > 0 )
			data.corridorState--;
		if (GameData.gameState.Contains ("Shop")) {
				controller.GetComponent<ShopController> ().UpdateShop ();
			Debug.Log("setshop");
		} else if (GameData.gameState.Contains ("Quest")) {
			Debug.Log("setquest");
			controller.GetComponent<QuestController>().SetQuest();					
		}

	}
}

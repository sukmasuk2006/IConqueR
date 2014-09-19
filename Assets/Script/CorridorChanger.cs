using UnityEngine;
using System.Collections.Generic;

public class CorridorChanger : MonoBehaviour {

	public int dir;
	public List<GameObject> controller;
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
			for ( int i = 0 ; i < controller.Count ; i++ )
				controller[i].GetComponent<ShopSlotSetter>().UpdateSlot ();
			Debug.Log("setshop");
		}
		else if (GameData.gameState.Contains ("upgrade")) {
			for ( int i = 0 ; i < controller.Count ; i++ )
				controller[i].GetComponent<InventorySetter>().UpdateSlot ();
			Debug.Log("setshop");
		} else if (GameData.gameState.Contains ("Quest")) {
			Debug.Log("setquest");
			controller[0].GetComponent<QuestController>().SetQuest();					
		}

	}
}

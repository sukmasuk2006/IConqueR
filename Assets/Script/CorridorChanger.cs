using UnityEngine;
using System.Collections.Generic;

public class CorridorChanger : MonoBehaviour {

	public int dir;
	public List<GameObject> controller;
	public ScreenData data;
	public TextMesh corridorState;
	// Use this for initialization

	void Start () {
		corridorState.text = "Page " + (data.corridorState+1).ToString();

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
			corridorState.text = "Page " + (data.corridorState+1).ToString();
			Debug.Log("setshop");
		}
		else if (GameData.gameState.Contains ("Upgrade")) {
			for ( int i = 0 ; i < controller.Count ; i++ ){
				controller[i].GetComponent<InventorySetter>().UpdateSlot ();
				controller[i].GetComponent<InventorySetter>().CheckButton ();
			corridorState.text = "Page " + (data.corridorState+1).ToString();
			Debug.Log("setupgrade");
			}
		} else if (GameData.gameState.Contains ("Quest")) {
			Debug.Log("setquest");
			controller[0].GetComponent<QuestController>().SetQuest();					
			corridorState.text = "Page " + (data.corridorState+1).ToString();
		}

	}
}

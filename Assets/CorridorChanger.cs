using UnityEngine;
using System.Collections;

public class CorridorChanger : MonoBehaviour {

	public int dir;
	public ShopController controller;
	public ScreenData data;// Use this for initialization

	void Start () {
	
	}

	void OnMouseUp(){
		Debug.Log (data.corridorState + " " + data.maxCorridorState);
		if (dir < 0 && data.corridorState < data.maxCorridorState)
			data.corridorState++;
		// geser kiri
		else if ( dir > 0 && data.corridorState > 0 )
			data.corridorState--;
		controller.UpdateShop ();
	}
}

using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	public float barDisplay; //current progress
	public Vector2 pos = new Vector2(20,100);
	public Vector2 size = new Vector2(700,200);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI() {
		//draw the background:
//GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
	//	GUI.Box(new Rect(0,0, size.x, size.y), fullTex);
		
	//	//draw the filled-in part:
	//	GUI.BeginGroup(new Rect(0,0, size.x * barDisplay, size.y));
		GUI.Box (new Rect (pos.x, pos.y, barDisplay, size.y),fullTex);//, fullTex);

		//GUI.EndGroup();
		//GUI.EndGroup();
	}
	
	void Update() {
		//for this example, the bar display is linked to the current time,
		//however you would set this value based on your desired display
		//eg, the loading progress, the player's health, or whatever.
		Debug.Log ("size " + Screen.width + " " + Screen.height);
		barDisplay++;
		//      barDisplay = MyControlScript.staticHealth;
	}
}

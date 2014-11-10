using UnityEngine;
using System.Collections;

public class StoryChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnMouseDown(){
	  Camera.main.GetComponent<StoryFader> ().FadeOut ();
	}
}

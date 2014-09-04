using UnityEngine;
using System.Collections;

public class AutoFadeLogo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		AutoFade.LoadLevel ("MenuScene", 3.5f, 1.5f, Color.white);
	}
}

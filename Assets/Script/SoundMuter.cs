using UnityEngine;
using System.Collections;
using PluginUnityWP;

public class SoundMuter : MonoBehaviour {

	private bool isMute = false;
	Renderer soundOn;

	// Use this for initialization
	void Start () {
		soundOn = GameObject.Find ("soundOn").renderer;
		if (isMute)
			soundOn.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnMouseDown(){
						if (!isMute) {
								Debug.Log ("mute");
								AudioListener.volume = 0;
								isMute = true;		
								soundOn.enabled = false;
						} else {
								Debug.Log ("unmute");
								AudioListener.volume = 100;
								isMute = false;
								soundOn.enabled = true;
					}
	}

}

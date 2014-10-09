using UnityEngine;
using System.Collections;
using PluginUnityWP;

public class SoundMuter : MonoBehaviour {

	private bool isMute = false;
	public SpriteRenderer soundOn;
	public Sprite on;
	public Sprite off;

	// Use this for initialization
	void Start () {
		if (isMute)
			soundOn.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnMouseDown(){
						if (!isMute) {
								Debug.Log ("mute");
								MusicManager.setVolume(0,0);
								isMute = true;		
								soundOn.sprite = off;
						} else {
								Debug.Log ("unmute");
								MusicManager.setVolume(1,0);
								isMute = false;
								soundOn.sprite = on;
					}
	}

}

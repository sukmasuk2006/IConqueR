using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour {

	public static bool spawned = false;

	void Awake() {
		if(spawned == false)
		{
			spawned = true;
			
			DontDestroyOnLoad(MusicManager.getMusicEmitter());
			}
		else
		{
			DestroyImmediate(MusicManager.getMusicEmitter()); //This deletes the new object/s that you
		}	
	}

	public static void PlayMusic(string name){
		MusicManager.play (name);
//		Debug.Log ("PLAY " + MusicManager.getMusicEmitter ().audio.clip.name);

	}

}

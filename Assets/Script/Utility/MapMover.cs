using UnityEngine;
using System.Collections;

public class MapMover : MonoBehaviour {

	public ScreenData data;
	public GameObject tweenedObject;
	public int dir;
	private float x,y;
	public AudioClip sound;
	// Use this for initialization
	void Start () {
		data.corridorState = GameData.profile.MapPosition;
		SetCoord ();
		tweenedObject.transform.position = new Vector3 (x, y, -0.6f);
	}
	
	void OnMouseUp(){
		if (GameData.readyToTween) {
			x = 0;
			y =0;
			if (dir < 0 && data.corridorState > 0) {
				data.corridorState--;
			} 
			else if (dir > 0 && data.corridorState < data.maxCorridorState) {
				data.corridorState++;
			}
			
			SetCoord ();
			GameData.readyToTween = false;	
			iTween.MoveTo (tweenedObject, iTween.Hash ("position", new Vector3 (x, y, -0.6f), "time", 1f, "onComplete", "ReadyTween", "onCompleteTarget", gameObject));
			GameData.profile.MapPosition = data.corridorState;
		}
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);

	}

	void ReadyTween(){
		GameData.readyToTween = true;
		SaveLoad.Save ();
	}
		
	void SetCoord(){
			switch (data.corridorState) {
		case 0 : x =0f;y=0f; break;
		case 1 : x =-20f;y=0f; break;
		case 2 : x =0f;y=10f; break;
		case 3 : x =-20f;y=10f; break;
			}
	}


}

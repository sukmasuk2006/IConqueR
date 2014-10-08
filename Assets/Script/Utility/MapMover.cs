using UnityEngine;
using System.Collections;

public class MapMover : MonoBehaviour {

	public ScreenData data;
	public GameObject tweenedObject;
	public GameObject mapBg;
	public int dir;
	private float x,y;
	private float xMap,yMap;
	public AudioClip sound;
	// Use this for initialization
	void Start () {
		data.corridorState = GameData.profile.MapPosition;
		SetCoord ();
		SetMapCoord ();
		tweenedObject.transform.position = new Vector3 (x, y, -0.6f);
		mapBg.transform.position = new Vector3 (xMap, yMap, -0.2f);
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
			SetMapCoord();
			GameData.readyToTween = false;	
			iTween.MoveTo (tweenedObject, iTween.Hash ("position", new Vector3 (x, y, -0.6f), "time", 1f, "onComplete", "ReadyTween", "onCompleteTarget", gameObject));
			iTween.MoveTo (mapBg, iTween.Hash ("position", new Vector3 (xMap, yMap, -0.2f), "time", 1f, "onComplete", "ReadyTween", "onCompleteTarget", gameObject));
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
		case 0 : x =0;y=0f; break;
		case 1 : x =-20.6f;y=1.2f; break;
		case 2 : x =6.5f;y=12.5f; break;
		case 3 : x =-6.8f;y=13.2f; break;
		case 4 : x =-21f;y=14.3f; break;
		}
	}

	void SetMapCoord(){
		switch (data.corridorState) {
		case 0 : xMap =-19f;yMap=8f; break;
		case 1 : xMap =-39.6f;yMap=9.2f; break;
		case 2 : xMap =-12.2f;yMap=19.67f; break;
		case 3 : xMap =-25.7f;yMap=21.3f; break;
		case 4 : xMap =-37.8f;yMap=24f; break;
		}
	}


}

﻿using UnityEngine;
using System.Collections;

public class ObjectTweener : MonoBehaviour {

	// Use this for initialization
	public GameObject obj;
	public float time;
	public int dir;
	private const float corridorSize = 18;
	public ScreenData data;
	public TextMesh corridorState;

	void Start () {
		corridorState.text = "Page " + (data.corridorState+1).ToString();
	}

	void OnMouseUp(){
		// geser kanan
//		Debug.Log (data.corridorState + " " + data.maxCorridorState);
		if (dir > 0 && data.corridorState < data.maxCorridorState)
			data.corridorState++;
		// geser kiri
		else if ( dir < 0 && data.corridorState > 0 )
			data.corridorState--;
		iTween.MoveTo ( obj,iTween.Hash("position",new Vector3(corridorSize*-data.corridorState,
		                                                       -1.3f,-3f),"time",time));
		corridorState.text = "Page " + (data.corridorState+1).ToString();
	}
}
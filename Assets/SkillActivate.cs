using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillActivate : MonoBehaviour {

	public GameplayController controller;
	private List<GameObject> heroList;
	private List<GameObject> enemyList;
	// Use this for initialization
	void Start () {
		heroList = controller.heroList;
		enemyList = controller.enemyList;
	}

	void OnMouseDown(){


	}
	// Update is called once per frame
	void Update () {
	
	}
}

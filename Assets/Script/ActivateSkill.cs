using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateSkill : MonoBehaviour {

	public BattleController controller;
	private List<GameObject> affectedUnitList;
	public int slot;

	// Use this for initialization
	void Start () {

		Debug.Log ("jum " + GameData.formationList.Count);
		for (int i = 0 ; i < GameData.formationList.Count ; i++ ){
			if ( GameData.formationList[i].IsUnlocked )
				controller.activeSkill[slot].DoEffect(GameData.formationList[i].Unit);
		}
	}

	// Update is called once per frame
	void Update () {
	}
}

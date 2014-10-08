using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateSkill : MonoBehaviour {

	public BattleController controller;
	private List<GameObject> affectedUnitList;
	public int slot;

	// Use this for initialization
	void Start () {
		controller.activeSkill[slot].DoEffect(GameData.profile.unitList[controller.activeSkill[slot].Id]);
	}

	// Update is called once per frame
	void Update () {
	}
}

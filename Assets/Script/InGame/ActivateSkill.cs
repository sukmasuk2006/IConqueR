using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateSkill : MonoBehaviour {

	public BattleController controller;
	public SpriteRenderer render;
	private List<GameObject> affectedUnitList;
	public int slot;

	// Use this for initialization
	void Start () {
		render.sprite = GameData.skillSpriteList[controller.activeSkill[slot].Id];
		Debug.Log ("slot " + slot + " id " + controller.activeSkill [slot].Id);
		controller.activeSkill[slot].DoEffect(GameData.profile.unitList[controller.activeSkill[slot].Id]);
	}

	// Update is called once per frame
	void Update () {
	}
}

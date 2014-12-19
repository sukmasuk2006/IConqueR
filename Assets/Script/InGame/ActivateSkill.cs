using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateSkill : MonoBehaviour {

	public BattleController controller;
	public SpriteRenderer render;
	public HeroController hero;
	private List<GameObject> affectedUnitList;
	public int slot;

	// Use this for initialization
	void Start () {
		if ( this.gameObject.activeInHierarchy && GameData.profile.formationList[slot].Unit.HeroId != 99  ){
			render.sprite = GameData.skillSpriteList[controller.activeSkill[slot].Id];
			//			Debug.Log ("slot " + slot + " id " + controller.activeSkill [slot].Id);
			Debug.Log ("sebelum " + GameData.profile.unitList[controller.activeSkill[slot].Id].AttackPoint);
			controller.activeSkill[slot].DoPassiveEffect(GameData.profile.unitList[controller.activeSkill[slot].Id]);
			Debug.Log ("after activare " + GameData.profile.unitList[controller.activeSkill[slot].Id].AttackPoint);
		}
	}

	void OnMouseDown(){
		if ( controller.BatlleState == 0 && GameData.profile.formationList[slot].Unit.HeroId != 99 )
			hero.DoSpecial();
	}

	// Update is called once per frame
	void Update () {
	}
}

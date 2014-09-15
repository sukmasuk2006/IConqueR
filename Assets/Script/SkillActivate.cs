using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillActivate : MonoBehaviour {

	public BattleController controller;
	public GameObject skillCD;
	private List<GameObject> heroList;
	private List<GameObject> enemyList;
	private float scaleYAwal;
	private float scaleY;
	private float cooldownAwal;
	private float cooldown;
	public int slot;
	private bool isCooldown;

	// Use this for initialization
	void Start () {
		isCooldown = false;
		heroList = controller.heroList;
		enemyList = controller.enemyList;
		scaleYAwal = 0.54f;
		scaleY = 0f;
		cooldown = 0;
		cooldownAwal =  GameData.skillList [slot].Cooldown;
//		Debug.Log ("slot " + slot + " cd " + GameData.skillList [slot].Cooldown);
	}

	void OnMouseDown(){
		if (!isCooldown) {
			cooldown = cooldownAwal;
			UpdatePicture();
			isCooldown = true;
			foreach ( GameObject g in enemyList ){
				if ( g.activeInHierarchy ){
					HeroController h = g.GetComponent<HeroController>();
					float dmg = GameData.skillList[slot].Effect.Amount;
					h.stats.HealthPoint -= dmg;
						controller.ReceiveDamage("enemy",dmg);
					Debug.Log("darah " + g.GetComponent<HeroController>().stats.HealthPoint);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isCooldown) {
				UpdatePicture();
				cooldown -= Time.deltaTime;
				if ( cooldown <= 0 ){
					isCooldown = false;
				}
		}
		//GameData.skillList[0].
	}

	void UpdatePicture(){
		skillCD.transform.localScale = new Vector3 (skillCD.transform.localScale.x,
		                                            (cooldown * scaleYAwal / cooldownAwal),
		                                            skillCD.transform.localScale.z);
	}
}

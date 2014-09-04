using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayController : MonoBehaviour {

	/*
	 * Gameplay
	 */
	public List<GameObject> heroList;
	public List<GameObject> enemyList;
	public GameObject globalHeroHealthBar;
	public GameObject globalEnemyHealthBar;
	public GameObject reportScreen;
	public Vector3 reportTargetPosition;	
	public TextMesh goldTextMesh;

	private bool isWin = false;
	/*
     * Report
	 */
	private Vector3 ConstantHeroHealthLocalScale;
	private Vector3 ConstantEnemyHealthLocalScale;

	private float heroTotalHealth;
	private float enemyTotalHealth;
	private float ConstantHeroHealth; // untuk menghitung
	private float ConstantEnemyHealth; // untuk menghitung

	// Use this for initialization
	void Start () {
		/*awal2 semua hero aktif, kemudian dicek ada brp yang aktif*/
		GameData.gameState = GameConstant.GAMEPLAY_SCENE;
		int i = 0;
		foreach ( GameObject g in heroList ){
			if ( GameData.unitList[i].IsUnlocked )
				heroTotalHealth += g.GetComponent<HeroController>().stats.HealthPoint; 
			i++;
		}
		foreach ( GameObject g in enemyList ){
			enemyTotalHealth += g.GetComponent<HeroController>().stats.HealthPoint; 
		}
	
		ConstantHeroHealthLocalScale = globalHeroHealthBar.transform.localScale;
		ConstantEnemyHealthLocalScale = globalEnemyHealthBar.transform.localScale;

		//Debug.Log ("health awal" + heroTotalHealth);
		ConstantHeroHealth = heroTotalHealth;
		ConstantEnemyHealth = enemyTotalHealth;
	}

	public float HeroTotalHealth {
		get {
			return heroTotalHealth;
		}
		set {
			heroTotalHealth = value;
		}
	}
	
	public float EnemyTotalHealth {
		get {
			return enemyTotalHealth;
		}
		set {
			enemyTotalHealth = value;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void ReceiveDamage(string target,float damage){
		if ( target.Contains("hero")){
			UpdateHeroHealthBar(damage);
		}
		else{
			UpdateEnemyHealthBar(damage);
		}
	}

	private void UpdateHeroHealthBar(float damage)
	{
		HeroTotalHealth -= damage;
		Vector3 temp = new Vector3((ConstantHeroHealthLocalScale * heroTotalHealth / 
		                            ConstantHeroHealth).x,
		                           ConstantHeroHealthLocalScale.y,
		                           ConstantHeroHealthLocalScale.z);
		globalHeroHealthBar.transform.localScale = temp; 
	}

	private void UpdateEnemyHealthBar(float damage){
		EnemyTotalHealth -= damage;
		Vector3 temp2 = new Vector3((ConstantHeroHealthLocalScale * heroTotalHealth / 
		                             ConstantHeroHealth).x,
		                            ConstantHeroHealthLocalScale.y,
		                            ConstantHeroHealthLocalScale.z);
		globalEnemyHealthBar.transform.localScale = temp2; 
	}

	void CheckWin(){
		if (isWin) {
			GameData.gold += 100;
			goldTextMesh.text = GameData.gold.ToString();
			iTween.MoveTo (reportScreen, iTween.Hash ("position", reportTargetPosition, "time", 1.0f));
			isWin = false;
			if ( GameData.raidTime <= 0 ){
				GameData.raidTime = 60f; 
			}
		}
	}
}

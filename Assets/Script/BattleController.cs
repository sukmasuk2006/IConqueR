using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour {

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
	public List<Unit> activeEnemyList;
	// 0 => battle
	// 1 => player win
	// 3=> lose , kenapa 3? biar bisa dibuat pembagi sekalian
	private int battleState = 0;
	/*
     * Report
	 */
	private Vector3 ConstantHeroHealthLocalScale;
	private Vector3 ConstantEnemyHealthLocalScale;

	private float heroTotalHealth;
	private float enemyTotalHealth;
	private float ConstantHeroHealth; // untuk menghitung
	private float ConstantEnemyHealth; // untuk menghitung

	private Mission mission;
	// Use this for initialization
	void Start () {

		mission = GameData.missionList [GameData.currentMission-1];
		//activeHeroList = new List<Unit> ();
		activeEnemyList = mission.EnemyList;
		/*awal2 semua hero aktif, kemudian dicek ada brp yang aktif*/
		GameData.gameState = GameConstant.GAMEPLAY_SCENE;
		int i = 0;
		foreach (FormationUnit u in GameData.formationList) {
				if ( u.IsUnlocked ){
					GameData.formationList[i].Unit.Refresh();
					heroList[i].SetActive(true);
					Sprite sprite = (Sprite)Resources.Load ("Sprite/Character/Hero/"+
			                                        u.Unit.Name, typeof(Sprite));
					heroList[i].GetComponent<SpriteRenderer>().sprite = sprite;	
					heroTotalHealth += u.Unit.HealthPoint;
					i++;
				}
			Debug.Log("unlocked hero " + i + " " + GameData.formationList[i].Unit.HealthPoint);

		}
		i = 0;
		foreach (Unit u in activeEnemyList) {
			activeEnemyList[i].Refresh();
			enemyList[i].SetActive(true);		
			enemyTotalHealth += u.HealthPoint;
			i++;
		}

	
		ConstantHeroHealthLocalScale = globalHeroHealthBar.transform.localScale;
		ConstantEnemyHealthLocalScale = globalEnemyHealthBar.transform.localScale;

		//Debug.Log ("health awal" + heroTotalHealth);
		ConstantHeroHealth = heroTotalHealth;
		ConstantEnemyHealth = enemyTotalHealth;
		Debug.Log ("start mission");
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
		if (battleState == 0) {
				if (target.Contains ("enemy")) {
						UpdateEnemyHealthBar (damage);
				} else {
						UpdateHeroHealthBar (damage);
				}
		}
	}

	private void UpdateHeroHealthBar(float damage)
	{
		HeroTotalHealth -= damage;
		if (heroTotalHealth <= 0)
						heroTotalHealth = 0;
		Vector3 temp = new Vector3((ConstantHeroHealthLocalScale * heroTotalHealth / 
		                            ConstantHeroHealth).x,
		                           ConstantHeroHealthLocalScale.y,
		                           ConstantHeroHealthLocalScale.z);
		globalHeroHealthBar.transform.localScale = temp; 
		if (heroTotalHealth <= 0) {
			battleState = 5;		
			Lose();
		}
	}

	private void UpdateEnemyHealthBar(float damage){
		EnemyTotalHealth -= damage;
		if (enemyTotalHealth <= 0)
						enemyTotalHealth = 0;
		Vector3 temp2 = new Vector3((ConstantEnemyHealthLocalScale * enemyTotalHealth / 
		                             ConstantEnemyHealth).x,
		                            ConstantEnemyHealthLocalScale.y,
		                            ConstantEnemyHealthLocalScale.z);
		globalEnemyHealthBar.transform.localScale = temp2; 
		if (enemyTotalHealth == 0) {
			battleState = 1;	
			Win();
		}
	}

	// WIINNNN!!
	void Win(){
		GetReward ();
		ShowOnReport ();
		Debug.Log ("win");
	}

	void Lose(){
		GetReward ();
		ShowOnReport ();
		
		Debug.Log ("lose");
	}

	void ShowOnReport(){
		goldTextMesh.text = GameData.gold.ToString ();
		iTween.MoveTo (reportScreen, iTween.Hash ("position", reportTargetPosition, "time", 1.0f));
		if (GameData.raidTime <= 0) {
			GameData.raidTime = 60f; 
		}
		
		mission = null;
	}

	void GetReward(){
		
		GameData.gold += mission.GoldReward / battleState;
		GameData.currentExp += (mission.ExpReward/3)  / battleState;
		GameData.diamond += mission.DiamondReward  / battleState;
		int nextExp = GameData.expList [GameData.currentLevel - 1];
		if (GameData.currentExp >= nextExp) {
			GameData.currentExp -= nextExp;
			GameData.currentLevel++;
		}
		foreach (Unit u in GameData.unitList) {
			u.CurrentExp += mission.ExpReward  / battleState;		
		}
	}



	public int BatlleState {
		get {
			return battleState;
		}
		set {
			battleState = value;
		}
	}
}

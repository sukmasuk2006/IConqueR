using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour {

	/*
	 * Gameplay
	 */
	public List<GameObject> heroList;
	public List<GameObject> enemyList;
	public List<GameObject> skillList;

	public List<Skill> activeSkill;
	public List<Unit> activeEnemyList;

	public GameObject globalHeroHealthBar;
	public GameObject globalEnemyHealthBar;
	public GameObject reportScreen;
	public Vector3 reportTargetPosition;	
	public TextMesh goldTextMesh;
	public TextMesh diamondText;
	public TextMesh expEarnedText;
	public TextMesh winloseText;
	public GameObject expBar;

	// 0 => battle
	// 1 => player win
	// 3=> lose , kenapa 3? biar bisa dibuat pembagi sekalian
	private int battleState = 0;
	private int nextExp;
	private float scaleX;
	/*
     * Report
	 */
	private Vector3 ConstantHeroHealthLocalScale;
	private Vector3 ConstantEnemyHealthLocalScale;

	private float heroTotalHealth;
	private float enemyTotalHealth;
	private float ConstantHeroHealth; // untuk menghitung
	private float ConstantEnemyHealth; // untuk menghitung
	private int goldEarn;
	private float expEarn;
	private int diamondEarn;
	private Mission mission;

	// Use this for initialization
	void Start () {
		ScaleExpBar ();
		Debug.Log ("battle activate");

		mission = GameData.missionList [GameData.currentMission-1];
		activeEnemyList = mission.EnemyList;
		activeSkill = new List<Skill> ();
		int i = 0,j = 0;

		for ( i =0; i < GameData.skillList.Count; i++) {
			if ( GameData.skillList[i].IsSelected ){
				activeSkill.Add(GameData.skillList[i]);
				skillList[j].SetActive(true);
				//Debug.Log("selected skill " + i);
				j++;
			}
		}

		/*awal2 semua hero mati, kemudian dicek ada brp yang aktif*/
		i = 0;
		GameData.gameState = GameConstant.GAMEPLAY_SCENE;
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
		winloseText.text = "Conquered!";
		GetReward ();
		ShowOnReport ();
		Debug.Log ("win");
	}

	void Lose(){
		
		winloseText.text = "You Lose!";
		GetReward ();
		ShowOnReport ();
		
		Debug.Log ("lose");
	}

	void GetReward(){
		goldEarn = mission.GoldReward / battleState;
		GameData.gold += goldEarn;
		expEarn = (mission.ExpReward/3)  / battleState;
		GameData.currentExp += expEarn;
		diamondEarn = mission.DiamondReward  / battleState;
		GameData.diamond += diamondEarn;
	}

	void ShowOnReport(){
		goldTextMesh.text ="+"+goldEarn.ToString ();
		diamondText.text ="+"+diamondEarn.ToString ();
		expEarnedText.text ="+"+expEarn.ToString ();
		iTween.MoveTo (reportScreen, iTween.Hash ("position", reportTargetPosition, "time", 1.0f,"delay",3.0f));
		ScaleExpBar ();
		GetExpReward ();
	}

	void ScaleExpBar(){
		int nextExp = GameData.expList [GameData.currentLevel - 1];
		scaleX = GameData.currentExp * 1f / nextExp;
		if (GameData.currentExp >= nextExp) {
			scaleX = 1;
			GameData.currentExp -= nextExp;
			GameData.currentLevel++;
		}
		Vector3 newScale = new Vector3 (scaleX,expBar.transform.localScale.y, expBar.transform.localScale.z);
		iTween.ScaleTo (expBar, iTween.Hash("scale", newScale,
		                                    "time", 1.5f,
		                                    "delay",4.0f));

	}

	void GetExpReward(){

		foreach (Unit u in GameData.unitList) {
			u.CurrentExp += mission.ExpReward  / battleState;		
		}
		mission = null;
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

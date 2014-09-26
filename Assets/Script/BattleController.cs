using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour {

	/*
	 * Gameplay
	 */
	public GameObject reportScreen;
	public GameObject levelUpScreen;
	public GameObject itemGained;
	public SpriteRenderer itemGainedSprite;
	public TextMesh itemName;
	private float[] positionList;
	private bool[] positionAvailableList;

	public GameObject globalHeroHealthBar;
	public GameObject globalEnemyHealthBar;
	public List<GameObject> heroList;
	public List<GameObject> enemyList;
	public List<GameObject> skillList;

	public List<Skill> activeSkill;
	public List<Unit> activeEnemyList;
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
	private int isGetReward;

	// Use this for initialization
	void Start () {
		//		Debug.Log ("battle activate");
		positionList = new float[12]{1.5f,3f,4.5f,6f,7.5f,9f,1.5f,3f,4.5f,6f,7.5f,9f};
		positionAvailableList = new bool[12]{true,true,true,true,true,true,true,true,true,true,true,true};
		isGetReward = 0;
		battleState = 0;
		mission = GameData.missionList [GameData.profile.CurrentMission];
		//Debug.Log ("Misi ke-" + (GameData.profile.CurrentMission - 1).ToString());
		activeEnemyList = mission.EnemyList;
		//Debug.Log ("Jumlah musuh " + activeEnemyList.Count);
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
				if ( u.IsUnlocked && u.Unit.HeroId != 99){
					GameData.formationList[i].Unit.Refresh();
					heroList[i].SetActive(true);
					heroList[i].GetComponent<SpriteRenderer>().sprite = u.Unit.Sprites;	
					heroTotalHealth += u.Unit.HealthPoint;
					i++;
				}
//			Debug.Log("unlocked hero " + i + " " + GameData.formationList[i].Unit.HealthPoint);

		}
		i = 0;
		foreach (Unit u in activeEnemyList) {
			u.Refresh();
			enemyList[i].SetActive(true);	
			enemyList[i].GetComponent<SpriteRenderer>().sprite = u.Sprites;
			enemyTotalHealth += u.HealthPoint;
			i++;
		}
		//Debug.Log ("Jumlah hp " + enemyTotalHealth);

	
		ConstantHeroHealthLocalScale = globalHeroHealthBar.transform.localScale;
		ConstantEnemyHealthLocalScale = globalEnemyHealthBar.transform.localScale;

		//Debug.Log ("health awal" + heroTotalHealth);
		ConstantHeroHealth = heroTotalHealth;
		ConstantEnemyHealth = enemyTotalHealth;
//		Debug.Log ("start mission");
		ScaleExpBar ();

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
			foreach(GameObject g in heroList){
				if ( g.activeInHierarchy )
					g.GetComponent<HeroController>().MoveToGraveyard();
			}
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
			foreach(GameObject g in enemyList){
				if ( g.activeInHierarchy )
					g.GetComponent<HeroController>().MoveToGraveyard();
			}
			Win();
		}
	}

	// WIINNNN!!
	void Win(){
		winloseText.text = "Conquered!";
		// win, kadang dapat kadang kaga
		isGetReward = Random.Range (0, 99);
		GetReward ();
		ShowOnReport ();
		Debug.Log ("win " + isGetReward);
	}

	void Lose(){
		
		winloseText.text = "You Lose!";
		GetReward ();
		ShowOnReport ();
		
		Debug.Log ("lose");
	}

	void GetReward(){
		goldEarn = mission.GoldReward / battleState;
		GameData.profile.Gold += goldEarn;
		expEarn = (mission.ExpReward)  / battleState;
		GameData.profile.CurrentExp += (int)expEarn;
		diamondEarn = mission.DiamondReward  / battleState;
		GameData.profile.Diamond += diamondEarn;
		if (isGetReward % 2 == 1) {
			int get = mission.GetReward;
			itemGained.SetActive(true);
			itemGainedSprite.sprite = GameData.shopList[get].Sprites;
			itemName.text = GameData.shopList[get].Name;
			GameData.inventoryList.Add(GameData.shopList[get]);
			Debug.Log("get reward");
		}
		
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
		int nextExp = mission.ExpReward;
		scaleX = GameData.profile.CurrentExp * 1f / nextExp;
		if (GameData.profile.CurrentExp >= nextExp) {
			//LEVELUP
			scaleX = 1;
			GameData.profile.CurrentExp -= nextExp;
			GameData.profile.Level++;
			iTween.MoveTo (levelUpScreen, iTween.Hash ("position", new Vector3(levelUpScreen.transform.position.x,reportTargetPosition.y,
			                                                                   levelUpScreen.transform.position.z), "time", 1.0f,"delay",5.0f));
			}
		Vector3 newScale = new Vector3 (scaleX,expBar.transform.localScale.y, expBar.transform.localScale.z);
		iTween.ScaleTo (expBar, iTween.Hash("scale", newScale,
		                                    "time", 1.5f,
		                                    "delay",4.0f,"oncomplete","ReadyTween","oncompletetarget",gameObject));

	}

	void ReadyTween(){
		GameData.readyToTween = true;
	}

	void GetExpReward(){
		// untuk unit
		foreach (Unit u in GameData.unitList) {
			if ( u.IsActive )
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

	public float[] PositionList {
		get {
			return positionList;
		}
		set {
			positionList = value;
		}
	}

	public bool[] PositionAvailableList {
		get {
			return positionAvailableList;
		}
		set {
			positionAvailableList = value;
		}
	}
}

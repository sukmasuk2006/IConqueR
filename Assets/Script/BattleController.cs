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
	public List<GameObject> heroLevelUpSpriteList;
	public SpriteRenderer bg;
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
	private const int itemChance = 40;

	// Use this for initialization
	void Start () {
		battleState = 0;
		MusicManager.play ("Music/royal2", 0.1f, 0.1f);
		Sprite back = null;
		int cur = (GameData.currentMission/5)+1;
		back = (Sprite)Resources.Load ("Sprite/Background/"+cur,typeof(Sprite));
		bg.sprite = back;
		//		Debug.Log ("battle activate");
		positionList = new float[12]{1.75f,3f,4.25f,5.5f,6.25f,7.5f,1.75f,3f,4.25f,5.5f,6.25f,7.5f};
		positionAvailableList = new bool[12]{true,true,true,true,true,true,true,true,true,true,true,true};
		isGetReward = 0;
		battleState = 0;
		mission = GameData.profile.missionList [GameData.currentMission];
		//Debug.Log ("Misi ke-" + (GameData.profile.CurrentMission - 1).ToString());
		activeEnemyList = mission.EnemyList;
		//Debug.Log ("Jumlah musuh " + activeEnemyList.Count);
		activeSkill = new List<Skill> ();
		int i = 0,j = 0;

		for ( i =0; i < GameData.profile.skillList.Count; i++) {
			if ( GameData.profile.skillList[i].IsSelected ){
				activeSkill.Add(GameData.profile.skillList[i]);
				skillList[j].SetActive(true);
				//Debug.Log("selected skill " + i);
				j++;
			}
		}

		/*awal2 semua hero mati, kemudian dicek ada brp yang aktif*/
		i = 0;
		GameData.gameState = GameConstant.GAMEPLAY_SCENE;
		foreach (FormationUnit u in GameData.profile.formationList) {
				if ( u.IsUnlocked && u.Unit.HeroId != 99){
					GameData.profile.formationList[i].Unit.Refresh();
					heroList[i].SetActive(true);
//					heroList[i].GetComponent<SpriteRenderer>().sprite = u.Unit.Sprites;	
					heroTotalHealth += u.Unit.HealthPoint;
					i++;
				}
//			Debug.Log("unlocked hero " + i + " " + GameData.formationList[i].Unit.HealthPoint);

		}
		i = 0;
		foreach (Unit u in activeEnemyList) {
			u.Refresh();
			enemyList[i].SetActive(true);	
//			enemyList[i].GetComponent<SpriteRenderer>().sprite = u.Sprites;
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
		enemyTotalHealth = Mathf.Floor(enemyTotalHealth- damage);
		if (enemyTotalHealth <= 0)
				enemyTotalHealth = 0;

		Vector3 temp2 = new Vector3((ConstantEnemyHealthLocalScale * enemyTotalHealth / 
		                             ConstantEnemyHealth).x,
		                            ConstantEnemyHealthLocalScale.y,
		                            ConstantEnemyHealthLocalScale.z);
		globalEnemyHealthBar.transform.localScale = temp2; 
		if (enemyTotalHealth <= 0) {
			battleState = 1;	
			Debug.Log ("WIN2");
			foreach(GameObject g in enemyList){
				if ( g.activeInHierarchy )
					g.GetComponent<HeroController>().MoveToGraveyard();
			}
			Win();

		}
	}

	// WIINNNN!!
	void Win(){
		Debug.Log ("WIN3");
		winloseText.text = "Conquered!";
		// win, kadang dapat kadang kaga
		isGetReward = Random.Range (0, 99);
		GetReward ();
		if (GameData.currentMission == GameData.profile.NextMission) {
			if (GameData.missionType.Contains ("Fortress"))
				GameData.profile.FortressDestroyed++;
			else if (GameData.missionType.Contains ("Castle"))
				GameData.profile.CastleDestroyed++;
			GameData.profile.NextMission++;
		}
		GameData.profile.CheckQuestAchievement ();
		ShowOnReport ();
//		Debug.Log ("win " + GameData.profile.DefeatedArmy);
		SaveLoad.Save ();
	}

	void Lose(){
		
		winloseText.text = "You Lose!";
		GetReward ();
		ShowOnReport ();
		GameData.profile.CheckQuestAchievement ();

		SaveLoad.Save ();
		Debug.Log ("lose");
	}

	void GetReward(){
		goldEarn = mission.GoldReward / battleState;
		GameData.profile.Gold += goldEarn;
		diamondEarn = mission.DiamondReward  / battleState;
		GameData.profile.Diamond += diamondEarn;
		// semakin tinggi misi, semakin susah dpt reward
		if (isGetReward < itemChance - GameData.currentMission && battleState == 1) {
			int get = mission.GetReward;
			itemGained.SetActive(true);
			itemGainedSprite.sprite = GameData.gemSpriteList[get];
			itemName.text = GameData.shopList[get].Name;
			GameData.profile.inventoryList.Add(GameData.shopList[get]);
//			Debug.Log("get reward");
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
		int gotExp = mission.ExpReward; // buat ngitung
		expEarn = gotExp;
		scaleX = GameData.profile.CurrentExp * 1f / GameData.profile.NextExp; // cek awal2

		Debug.Log ("DI BATLE cur " + GameData.profile.CurrentExp + " got " + gotExp + " nesx " + GameData.profile.NextExp);
		if ( battleState == 1 || battleState == 5 ){
			if (GameData.profile.IsLevelUp(gotExp) ) {
			//LEVELUP
				GameData.profile.CurrentExp += gotExp;
				scaleX = 0.99f;
				Debug.Log ("LEVEL UP");
				iTween.MoveTo (levelUpScreen, iTween.Hash ("position", new Vector3(levelUpScreen.transform.position.x,reportTargetPosition.y,
				                                                                   levelUpScreen.transform.position.z), "time", 1.0f,"delay",5.0f));
			}
			else{
				// rescale
				GameData.profile.CurrentExp += gotExp;
				scaleX = GameData.profile.CurrentExp * 1f / GameData.profile.NextExp; // cek awal2
			}
			Debug.Log("get exp");

		}
		Debug.Log ("scale x" + scaleX);
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
		int reward = mission.ExpReward / battleState;
		foreach (Unit u in GameData.profile.unitList) {
			if ( u.IsActive )
				u.CurrentExp += reward;		
		}
		List<FormationUnit> fl = GameData.profile.formationList;
		for (int i = 0 ; i < 5 ; i++) {
			if ( fl[i].IsUnlocked && fl[i].Unit.HeroId != 99){
				if ( fl[i].Unit.IsLevelUp(reward)){
					GameObject spr = heroLevelUpSpriteList[i];
					iTween.MoveTo(spr,iTween.Hash("position",new Vector3(spr.transform.position.x,-4.3f,-3.3f),
					                              "time",0.5f,"delay",4.5f));
				}
				fl[i].Unit.CurrentExp += reward;	
			}
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

using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class BattleController : MonoBehaviour {

	/*
	 * Gameplay
	 */
	public GameObject reportScreen;
	public GameObject levelUpScreen;
	public GameObject itemGained;
	public SpriteRenderer itemGainedSprite;
	public SpriteRenderer heroTitle;
	public SpriteRenderer enemyTitle;
	public TextMesh itemName;
	private float[] positionList;
	private bool[] positionAvailableList;

	public GameObject pauseScreen;
	public GameObject globalHeroHealthBar;
	public GameObject globalEnemyHealthBar;
	public List<GameObject> heroList;
	public List<GameObject> enemyList;
	public List<GameObject> skillList;
	public List<GameObject> heroLevelUpSpriteList;
	public SpriteRenderer bg;
	public List<Skill> activeSkill;
	public List<Unit> activeEnemyList;
	public List<Unit> tempStats;
	public Vector3 reportTargetPosition;	
	public TextMesh goldTextMesh;
	public TextMesh diamondText;
	public TextMesh expEarnedText;
	public TextMesh winloseText;
	public GameObject expBar;

	// 0 => battle
	// 1 => player win
	// 3=> lose , kenapa 3? biar bisa dibuat pembagi sekalian
	private int totalHero;
	private int totalEnemy;
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
		Debug.Log ("batltecontrol START");

		heroTitle.sprite = GameData.titleSpriteList [GameData.profile.Title];
		mission = GameData.missionList [GameData.currentMission];
		enemyTitle.sprite = GameData.titleSpriteList [mission.Title];
		int cur = (GameData.currentMission/10)+1;

		battleState = 0;
		MusicManager.play ("Music/royal2", 0.1f, 0.1f);
		Sprite back = null;
		back = (Sprite)Resources.Load ("Sprite/Background/"+cur,typeof(Sprite));
		bg.sprite = back;
		//		Debug.Log ("battle activate");
		positionList = new float[12]{1.75f,3f,4.25f,5.5f,6.25f,7.5f,1.75f,3f,4.25f,5.5f,6.25f,7.5f};
		positionAvailableList = new bool[12]{true,true,true,true,true,true,true,true,true,true,true,true};
		isGetReward = 0;
		battleState = 0;
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
					float level = 0; 
					level = 1 + (GameData.profile.Level * 0.05f);
					level += (GameData.profile.Title * 0.25f);
					u.Unit.Agi *= level;
					u.Unit.Str *= level;
					u.Unit.Vit *= level;
					u.Unit.SetStats();
					heroTotalHealth += u.Unit.HealthPoint;
					i++;
				}
		}
		totalHero = i;
		i = 0;
		tempStats = new List<Unit> ();
		for ( i = 0 ; i < activeEnemyList.Count ; i++) {
			Unit s = activeEnemyList[i];
			s.Refresh();
			enemyList[i].SetActive(true);	
//			enemyList[i].GetComponent<SpriteRenderer>().sprite = u.Sprites;
			tempStats.Add(new Unit(s.HeroId,s.Job.Trim()));
//			Debug.Log("added temp " + tempStats.Count
			// tiap 3 stage, naik level musuhnya
			float level = 0; 
			if ( GameData.missionType == "Camp") //boss
				level = 1 + (GameData.currentMission/2)*0.25f;
			else if ( GameData.missionType == "Fortress") //boss
				level = 1 + (GameData.currentMission/2)*0.5f;
			else if ( GameData.missionType == "Castle") //boss
				level = 1 + (GameData.currentMission/2)* 0.75f;

			level += (mission.Title * 0.5f);
			s.Agi *= level;
			s.Str *= level;
			s.Vit *= level;
			s.Weapon.WeaponStats.Str = s.Weapon.WeaponStats.Agi = s.Weapon.WeaponStats.Vit = 0;
			s.Weapon.Damage = 1 * level;
			s.SetStats();
			enemyTotalHealth += s.HealthPoint;

		}
	//	Debug.Log ("report " + activeEnemyList.Count + " " + tempStats.Count);

	
		ConstantHeroHealthLocalScale = globalHeroHealthBar.transform.localScale;
		ConstantEnemyHealthLocalScale = globalEnemyHealthBar.transform.localScale;
		totalEnemy = mission.EnemyList.Count;
	
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
		if (Input.GetKeyDown (KeyCode.Escape) && GameData.gameState != "Paused") {
			iTween.MoveTo ( pauseScreen,iTween.Hash("position",new Vector3(3.7f,-0.3f,-3f),"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
			//sound.audio.PlayOneShot (sound.audio.clip);
			GameData.gameState = "Paused";	
			GameData.readyToTween = false;	
			Debug.Log("pause");
		}
		else if (Input.GetKeyDown (KeyCode.Escape) && GameData.readyToTween && GameData.gameState == "Paused") {
			pauseScreen.GetComponentInChildren<Unpause>().UnPause();
		}
	}

	public void ReceiveDamage(string target,float damage){
		//if (battleState == 0) {
				if (target.Contains ("enemy")) {
						UpdateEnemyHealthBar (damage);
				} else {
						UpdateHeroHealthBar (damage);
				}
		//}
	}

	public int TotalHero {
		get {
			return totalHero;
		}
		set {
			totalHero = value;
			Debug.Log("hero die " + totalHero);
			if ( totalHero == 0 ){
				battleState = 5;
				Lose();
				Debug.Log("LOSEEE");
			}
		}
	}

	public int TotalEnemy {
		get {
			return totalEnemy;
		}
		set {
			totalEnemy = value;
			Debug.Log("enemy die " + totalEnemy);
			if ( TotalEnemy == 0 ){ 
				battleState = 1;
				Win();
				Debug.Log("WINN");
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
//		int total = heroList.Where (x => x.activeInHierarchy).ToList ().Count;
//		Debug.Log ("total hero " + total);
/*		if (heroTotalHealth <= 0 || total == 0) {
			battleState = 5;		
			foreach(GameObject g in heroList){
				if ( g.activeInHierarchy )
					g.GetComponent<HeroController>().MoveToGraveyard();
			}
		}*/
	}

	private void UpdateEnemyHealthBar(float damage){
		enemyTotalHealth = Mathf.Floor(enemyTotalHealth- damage);
		if (enemyTotalHealth <= 0) {
			enemyTotalHealth = 0;
		}

		Vector3 temp2 = new Vector3((ConstantEnemyHealthLocalScale * enemyTotalHealth / 
		                             ConstantEnemyHealth).x,
		                            ConstantEnemyHealthLocalScale.y,
		                            ConstantEnemyHealthLocalScale.z);
		globalEnemyHealthBar.transform.localScale = temp2; 
		/*
		int total = enemyList.Where (x => x.activeInHierarchy).ToList ().Count;
//		Debug.Log ("total enemy " + total);
		if (enemyTotalHealth <= 0 || total == 0) {
			battleState = 1;	
			foreach (GameObject g in enemyList) {
				HeroController h = g.GetComponent<HeroController>();
				if ( g.activeInHierarchy  ){
					h.MoveToGraveyard();
				}
			}
		}*/
	}

	// WIINNNN!!
	void Win(){
		Debug.Log ("WIN3");
		winloseText.text = "Conquered!";
		GetReward ();
		// diamond kalau win doang + caslte + 1x doang dptnya
		if (GameData.currentMission == GameData.profile.NextMission) {
			diamondEarn = mission.DiamondReward;

			if (GameData.missionType.Contains ("Fortress")){
				var m = GameData.profile.questList.Where(x => x.Target.Contains("fortress")).ToList();
				foreach( Quest q in m )
					q.CurrentQuantity++;
			}
			else if (GameData.missionType.Contains ("Castle")){
				var m = GameData.profile.questList.Where(x => x.Target.Contains("Castle")).ToList();
				foreach( Quest q in m )
					q.CurrentQuantity++;
				GameData.profile.Title++;
				GameData.profile.Diamond += diamondEarn;
			}
			GameData.profile.NextMission++;
		}

		ShowOnReport ();
//		Debug.Log ("win " + GameData.profile.DefeatedArmy);

	}

	void Lose(){
		
		winloseText.text = "You Lose!";
		GetReward ();
		ShowOnReport ();
		Debug.Log ("lose");
	}

	void GetReward(){
		// gold pasti
		// win, kadang dapat item kadang ngga
		isGetReward = Random.Range (0, 99);
		goldEarn = mission.GoldReward / battleState;
		GameData.profile.Gold += goldEarn;
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
		ScaleExpBar ();
		GetExpReward ();
		goldTextMesh.text ="+"+goldEarn.ToString ();
		diamondText.text ="+"+diamondEarn.ToString ();
		expEarnedText.text ="+"+expEarn.ToString ();
		Debug.Log ("report " + activeEnemyList.Count + " " + tempStats.Count);
		for (int i = 0; i < activeEnemyList.Count; i++) {
			activeEnemyList[i].CopyStats(tempStats[i].HeroId,tempStats[i]);
		}
		iTween.MoveTo (reportScreen, iTween.Hash ("position", reportTargetPosition, "time", 1.0f,"delay",3.0f));
		GameData.SaveData ();
	}

	void ScaleExpBar(){
		int gotExp = mission.ExpReward; // buat ngitung
		scaleX = GameData.profile.CurrentExp * 1f / GameData.profile.NextExp; // cek awal2

		//Debug.Log ("DI BATLE cur " + GameData.profile.CurrentExp + " got " + gotExp + " nesx " + GameData.profile.NextExp);
		if ( battleState == 1 || battleState == 5 ){
			gotExp /= battleState;
			expEarn = gotExp;
			if (GameData.profile.IsLevelUp(gotExp) ) {
			//LEVELUP
				GameData.profile.CurrentExp += gotExp;
				scaleX = 0.99f;
			//	Debug.Log ("LEVEL UP");
				iTween.MoveTo (levelUpScreen, iTween.Hash ("position", new Vector3(levelUpScreen.transform.position.x,reportTargetPosition.y,
				                                                                   levelUpScreen.transform.position.z), "time", 1.0f,"delay",5.0f));
			}
			else{
				// rescale
				GameData.profile.CurrentExp += gotExp;
				scaleX = GameData.profile.CurrentExp * 1f / GameData.profile.NextExp; // cek awal2
			}
			//Debug.Log("get exp");

		}
//		Debug.Log ("scale x" + scaleX);
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
		int i = 0;
		foreach (Unit u in GameData.profile.unitList) {
			if ( u.IsActive ){
				if ( u.IsLevelUp(reward)){
					GameObject spr = heroLevelUpSpriteList[i];
					iTween.MoveTo(spr,iTween.Hash("position",new Vector3(spr.transform.position.x,-4.3f,-3.3f),
					                              "time",0.5f,"delay",4.5f));
				}
				u.CurrentExp += reward;
				i++;
			}
		}
		GameData.profile.RefreshFormation ();
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

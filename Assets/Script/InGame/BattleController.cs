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
	public List<Transform> listPrefab;
	public GameObject skillShade;
	public GameObject reportShade;

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

	const float MaxExpBarScaleX = 0.85f;

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
	private const int itemChance = 60;

	// Use this for initialization
	void Start () {
		DeactivateShade(0f,0f);
		DeactivateSkillShade(0f,0f);
		Debug.Log ("batltecontrol START");
		heroTitle.sprite = GameData.titleSpriteList [GameData.profile.Title];
		mission = GameData.missionList [GameData.currentMission];
		enemyTitle.sprite = GameData.titleSpriteList [mission.Title];
		battleState = 0;
		if ( GameData.profile.TutorialState < 3 ){
			SetPrefab();
		}

		 MusicManager.play ("Music/royal2", 0.1f, 0.1f);
		Sprite back = null;
		back = (Sprite)Resources.Load ("Sprite/Background/"+mission.Place,typeof(Sprite));
		bg.sprite = back;
		//		Debug.Log ("battle activate");
		positionList = new float[12]{1.75f,3f,4.25f,5.5f,6.25f,7.5f,1.75f,3f,4.25f,5.5f,6.25f,7.5f};
		positionAvailableList = new bool[12]{true,true,true,true,true,true,true,true,true,true,true,true};
		isGetReward = 0;
		activeEnemyList = mission.EnemyList;
		Debug.Log ("Jumlah musuh " + activeEnemyList.Count);
		activeSkill = new List<Skill> ();
		int i = 0,j = 0;
		totalHero = 0;
		/*awal2 semua hero mati, kemudian dicek ada brp yang aktif*/
		i = 0;
	//	if ( GameData.profile.TutorialState == 1 )
		//	GameData.gameState = GameConstant.GAMEPLAY_SCENE;
		GameData.profile.RefreshFormation(); // refresh stats

		for ( i = 0 ; i < 5 ; i ++ ) {
			FormationUnit u = GameData.profile.formationList[i];// ambil unit di formation ke-i
			activeSkill.Add(GameData.profile.skillList[0]); // tambahkan skill default
				if ( u.IsUnlocked ){ // jika formation ke-i udah keunlock
					
					if (  u.UnitHeroId != 99 ){ // jika bukan unit template
						if ( GameData.profile.skillList[u.Unit.HeroId].IsUnlocked ){ // jika skill milik unit di form
							// udah keunlock
							activeSkill[i] = GameData.profile.skillList[u.Unit.HeroId];
							skillList[i].SetActive(true);
						}
						u.Unit.Str += GameData.profile.Level;
						u.Unit.Agi += GameData.profile.Level;
						u.Unit.Vit += GameData.profile.Level;

						if ( GameData.profile.TutorialState == 2 ){
							u.Unit.Str += 5;
							u.Unit.Agi += 5;
							u.Unit.Vit += 5;
						}
						heroList[i].SetActive(true);
						totalHero++;
						Debug.Log("stas "+ u.Unit.Str + " " + u.Unit.Agi + " " + u.Unit.Vit);
						u.Unit.SetStats();
						Debug.Log("Hero ke " + i + " weapon lev " + u.Unit.Weapon.Rank);
						heroTotalHealth += u.Unit.HealthPoint;
					}
				}
		}
		i = 0;
		tempStats = new List<Unit> ();
		for ( i = 0 ; i < activeEnemyList.Count ; i++) {
			Unit s = activeEnemyList[i];
			Debug.Log("enemy ke " + i + " " + s.Job);
			s.Refresh();
			enemyList[i].SetActive(true);	
			tempStats.Add(new Unit(s.HeroId,s.Job.Trim()));
			int level = 0; 
			level = GameData.currentMission * 3 / 4;

			if ( GameData.missionType == "Camp") //boss
				level +=1;
			else if ( GameData.missionType == "Fortress") //boss
				level +=2;
			else if ( GameData.missionType == "Castle") //boss
				level += 3;

			if ( GameData.profile.TutorialState == 3 ){
				level += 2;
			}
			level += mission.Title;
			for ( int l = 0 ; l < level ; l++ ){
				s.LevelUp();
		//		Debug.Log("enemy job " + s.JobList[0] + " stas en"+ s.Str + " " + s.Agi + " " + s.Vit);
			}
			//Debug.Log("levl " + s.Level + " stas en"+ s.Str + " " + s.Agi + " " + s.Vit);
			//for ( int r = 1 ; r < s.Weapon.Rank ; r++ ){
			//	s.Weapon.Upgrade(new UnitStatus());
			//}

			enemyTotalHealth += s.HealthPoint;

		}
		ConstantHeroHealthLocalScale = new Vector3(1f,globalHeroHealthBar.transform.localScale.y,globalHeroHealthBar.transform.localScale.z);
		ConstantEnemyHealthLocalScale = new Vector3(1f,globalEnemyHealthBar.transform.localScale.y,globalEnemyHealthBar.transform.localScale.z);
		totalEnemy = mission.EnemyList.Count;
		ConstantHeroHealth = heroTotalHealth;
		ConstantEnemyHealth = enemyTotalHealth;
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

		//Debug.Log("range " + Mathf.Abs(heroList[0].transform.position.x - enemyList[0].transform.position.x));

		if (Input.GetKeyDown (KeyCode.Escape) && GameData.gameState != "Paused") {
			iTween.MoveTo ( pauseScreen,iTween.Hash("position",new Vector3(3.7f,-0.3f,-3f),"time", 0.1f,"onComplete","ReadyTween","onCompleteTarget",gameObject));
			//sound.audio.PlayOneShot (sound.audio.clip);
			GameData.gameState = "Paused";	
			GameData.readyToTween = false;	
			Debug.Log("pause");
			ActivateShade(0f,0.1f);
		}
		else if (Input.GetKeyDown (KeyCode.Escape) && GameData.readyToTween && GameData.gameState == "Paused") {
			pauseScreen.GetComponentInChildren<Unpause>().UnPause();

		}
	}

	public void ReceiveDamage(string target,float damage){
				if (target.Contains ("enemy")) {
						UpdateEnemyHealthBar (damage);
				} else {
						UpdateHeroHealthBar (damage);
				}
	}

	public int TotalHero {
		get {
			return totalHero;
		}
		set {
			totalHero = value;
			if ( totalHero == 0 ){
				battleState = 5;
				Lose();
			}
		}
	}

	public int TotalEnemy {
		get {
			return totalEnemy;
		}
		set {
			totalEnemy = value;
			if ( TotalEnemy == 0 ){ 
				battleState = 1;
				Win();
			}
		}
	}

	private void UpdateHeroHealthBar(float damage)
	{
		HeroTotalHealth -= damage;
		if (heroTotalHealth <= 0)
						heroTotalHealth = 0;
		Vector3 temp = new Vector3(1.02f-(ConstantHeroHealthLocalScale * heroTotalHealth / 
		                            ConstantHeroHealth).x,
		                           ConstantHeroHealthLocalScale.y,
		                           ConstantHeroHealthLocalScale.z);
		globalHeroHealthBar.transform.localScale = temp; 
	}

	private void UpdateEnemyHealthBar(float damage){
		enemyTotalHealth = Mathf.Floor(enemyTotalHealth- damage);
		if (enemyTotalHealth <= 0) {
			enemyTotalHealth = 0;
		}

		Vector3 temp2 = new Vector3((ConstantEnemyHealthLocalScale * enemyTotalHealth / 
		                             ConstantEnemyHealth).x - 1.02f,
		                            ConstantEnemyHealthLocalScale.y,
		                            ConstantEnemyHealthLocalScale.z);
		globalEnemyHealthBar.transform.localScale = temp2; 
	}

	// WIINNNN!!
	void Win(){
		winloseText.text = "Conquered!";
		GetNonObjectReward();
		GetObjectReward ();
		// diamond kalau win doang + caslte + 1x doang dptnya
		if (GameData.currentMission == GameData.profile.NextMission) {
			diamondEarn = mission.DiamondReward;
			GameData.profile.Diamond += diamondEarn;

			if (GameData.missionType.Contains ("Fortress")){
				var m = GameData.profile.questList.Where(x => x.Target.Contains("fortress")).ToList();
				foreach( Quest q in m )
					q.CurrentQuantity++;
			}
			else if (GameData.missionType.Contains ("Castle")){
				var m = GameData.profile.questList.Where(x => x.Target.Contains("castle")).ToList();
				foreach( Quest q in m )
					q.CurrentQuantity++;
				GameData.profile.Title++;
			}
		if ( GameData.profile.NextMission < 50 )
			GameData.profile.NextMission++;
		}

		ShowOnReport ();
//		Debug.Log ("win " + GameData.profile.DefeatedArmy);

	}

	void Lose(){
		
		winloseText.text = "You Lose!";
		GetNonObjectReward ();
		ShowOnReport ();
	//	Debug.Log ("lose");
	}

	void GetNonObjectReward(){
		goldEarn = mission.GoldReward / battleState;
		GameData.profile.Gold += goldEarn;
	
	}

	void GetObjectReward(){
		isGetReward = Random.Range (0, 99);
		CheckTutorialState();
		float getChance = itemChance - (GameData.currentMission/6);
		// tiap battle chance 70. dikurangi tiap misi naik/6. misal 70 - misi 49/6 => 22
		Debug.Log("Reward chance " + isGetReward + " mision chance " + getChance);
		if (isGetReward < getChance && battleState == 1) {
			int get = mission.GetReward;
			itemGained.SetActive(true);
			itemGainedSprite.sprite = GameData.gemSpriteList[get];
			itemName.text = GameData.shopList[get].Name;
			GameData.profile.inventoryList.Add(GameData.shopList[get]);
			//			Debug.Log("get reward");
		}
	}

	public void ActivateShade(float delay,float time){
		iTween.ColorTo (reportShade, iTween.Hash ("delay", delay, "a", 1f, "time", time, "EaseType", "linear"));
	}
	
	public void DeactivateShade(float delay,float time){
		iTween.ColorTo (reportShade, iTween.Hash ("delay", delay, "a", 0f, "time", time, "EaseType", "linear"));
	}

	public void ActivateSkillShade(float delay,float time){
		iTween.ColorTo (skillShade, iTween.Hash ("delay", delay, "a", 1f, "time", time, "EaseType", "linear"));
	}
	
	public void DeactivateSkillShade(float delay,float time){
		iTween.ColorTo (skillShade, iTween.Hash ("delay", delay, "a", 0f, "time", time, "EaseType", "linear"));
	}

	void ShowOnReport(){
		ScaleExpBar ();
		GetExpReward ();
		goldTextMesh.text ="+"+goldEarn.ToString ();
		diamondText.text ="+"+diamondEarn.ToString ();
		expEarnedText.text ="+"+expEarn.ToString ();
	//	Debug.Log ("report " + activeEnemyList.Count + " " + tempStats.Count);
		for (int i = 0; i < activeEnemyList.Count; i++) {
			activeEnemyList[i].CopyStats(tempStats[i]);
		}
		ActivateShade(2f,1f);
		iTween.MoveTo (reportScreen, iTween.Hash ("position", new Vector3(0,0,reportScreen.transform.position.z), "time", 1.0f,"delay",3.0f));

		GameData.SaveData ();
	}

	void ScaleExpBar(){
		int gotExp = mission.ExpReward; // buat ngitung
		scaleX = GameData.profile.CurrentExp * MaxExpBarScaleX / GameData.profile.NextExp; // cek awal2

		//Debug.Log ("DI BATLE cur " + GameData.profile.CurrentExp + " got " + gotExp + " nesx " + GameData.profile.NextExp);
		if ( battleState == 1 || battleState == 5 ){
			gotExp /= battleState;
			expEarn = gotExp;
			if (GameData.profile.IsLevelUp(gotExp) ) {
			//LEVELUP
				GameData.profile.CurrentExp += gotExp;
				scaleX = 0.85f;
			//	Debug.Log ("LEVEL UP");
				iTween.MoveTo (levelUpScreen, iTween.Hash ("position", new Vector3(levelUpScreen.transform.position.x,1,
				                                                                   levelUpScreen.transform.position.z), "time", 1.0f,"delay",5.0f));
			}
			else{
				// rescale
				GameData.profile.CurrentExp += gotExp;
				scaleX = GameData.profile.CurrentExp * MaxExpBarScaleX / GameData.profile.NextExp; // cek awal2
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
				u.CurrentExp += reward;
				i++;
			}
		}
		for (int j = 0 ; j <  GameData.profile.formationList.Count ; j++) {
			FormationUnit u = GameData.profile.formationList[j];
			if ( u.UnitHeroId != 99 ){
				if ( u.Unit.IsLevelUp(reward)){
					GameObject spr = heroLevelUpSpriteList[j];
					
					iTween.MoveTo(spr,iTween.Hash("position",new Vector3(spr.transform.position.x,-5f,-3.3f),
					                              "time",0.5f,"delay",4.5f));
				}
			}
		}
		//GameData.profile.RefreshFormation ();
		mission = null;
	}

	
	void CheckTutorialState(){
		if ( GameData.profile.TutorialState < 3 )
			isGetReward = 100;
	}
	
	public void SetPrefab(){
		Debug.Log("set tutorial state " + GameData.profile.TutorialState);
		iTween.MoveTo ( this.gameObject,iTween.Hash("time",3.5f,"onComplete","ShowPrefab","onCompleteTarget",gameObject));	
	}

	void ShowPrefab(){
		if ( GameData.profile.TutorialState < GameConstant.TOTAL_TUTORIAL ){
			//battleState = 6;// tutorialstate
			Transform t = listPrefab [GameData.profile.TutorialState-2];
			Instantiate (t, new Vector3(t.position.x,t.position.y,t.position.z), Quaternion.identity);
		}
	}

	public void DestoryPrefab(){
		GameObject temp = GameObject.FindGameObjectWithTag ("Tutorial");
		Debug.Log ("at tutor destroy " + temp);
		Destroy (temp);
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

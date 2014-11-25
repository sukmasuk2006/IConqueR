using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/*
Bug list

 */

public class HeroController : MonoBehaviour {

	public List<GameObject> enemyList;
	public SpriteRenderer icon;
	public BattleController controller;
	public GameObject healthBar;
	public Transform manaBar;
	public SpriteRenderer lockSprite;
	public Unit stats;
	public GameObject projectile;
	//private Animator animator;

	public string target;
	private Vector3 healthScaleConstant;
	private float movementSpeed = 0f;

   // kecepatan gerak
	public float antiBug = 0f;
	private int states = 0;
	private bool isHero = false;
		// menentukan animasi
	public int direction = 1;
	private int attackType = 0; // 0  melee, 1 range
	private float attackSpeed = 0f;
	private float healthConstant;
	private bool isAttack = false;
	private bool isChangeState = true;
	public int slot;
	public SkeletonAnimation animator;
	private bool isDeath;
	private HeroController targetUnit;
	private const float MAXMANABAR = 0.37f;
	private bool isDoSpecial = false;
	private Skill skill;

	private AudioClip weaponSound;
	private AudioClip skillSound;


	// Use this for initialization
	void Start () {
		Debug.Log ("HERO START");
		isDeath = false;
		attackSpeed = 0;
		if (gameObject.activeInHierarchy) {
			healthBar.SetActive (true);
			lockSprite.enabled = false;
		}
		if (gameObject.name.Contains ("hero")) {
			stats = GameData.profile.formationList[slot].Unit;
			icon.sprite = GameData.unitIconList[stats.HeroId];
			isHero = true;
			skill = GameData.profile.skillList[stats.HeroId];
//			Debug.Log("di hero cont " + stats.HeroId);
			InitializeWeapon();
		} else if (gameObject.name.Contains ("enemy")) {
			stats =  controller.activeEnemyList[slot];
			isHero = false;
			InitializeWeapon();
			manaBar = healthBar.transform;
			InitializePosition(1);
		}
		manaBar.localScale =  new Vector3(0f,manaBar.localScale.y,manaBar.localScale.z);
		weaponSound = (AudioClip)Resources.Load("Music/"+stats.Weapon.SoundEffectName,typeof(AudioClip));
		skillSound = (AudioClip)Resources.Load("Music/Skill/"+stats.Job+"Skill",typeof(AudioClip));
		Debug.Log(" heroid " + stats.HeroId + " nama  " + name + " job " + stats.Job );
		animator.skeletonDataAsset = GameData.skeleteonDataAssetList[stats.HeroId];
		animator.calculateNormals = true;
		animator.Awake ();
		animator.state.AddAnimation (0, "run", true,0);
		animator.skeleton.SetSkin(stats.Job);
	//	Debug.Log(" jum material " + animator.skeletonDataAsset.atlasAsset.materials.Length);
		//animator.state.AddAnimation (1, "attack", false,0);
		healthConstant = stats.HealthPoint;
		healthScaleConstant = healthBar.transform.localScale;
		movementSpeed = stats.Movement;
	}
	
	// Update is called once per frame
	void Update () {
		CheckState ();
	}

	void InitializeWeapon(){
		if (stats.Weapon.Range == 5) {
			attackType = 1;		
			projectile.SetActive(true);
			//	Debug.Log("name " + name + " isarcher " );
		}
		
	}
	
	void InitializePosition(int pos){
		
		int dest = 0;
		dest = RandomPos (dest);
		while (!controller.PositionAvailableList[dest]) {
			dest = RandomPos (dest);
		}
		controller.PositionAvailableList [dest] = false;
		transform.position = new Vector2 (controller.PositionList[dest] * pos, gameObject.transform.position.y);
	}
	
	int RandomPos(int dest){
		if ( gameObject.name.Contains("hero") ) //hero range
			return Random.Range (0,6);
		else
			return Random.Range (6,12); // enemy
	}


	public void CheckState(){
		if (this.stats.HealthPoint <= 0 && !isDeath) {
			isDeath = true;
			if ( name.Contains("hero")) controller.TotalHero--;
			else if ( name.Contains("enemy")) controller.TotalEnemy--;
			Debug.Log("name " + name + " die");
			MoveToGraveyard();
		}
		// jika masih battle
		if (controller.BatlleState == 0 && GameData.gameState != "Paused" && !isDeath ) {
			attackSpeed -= Time.deltaTime;
			if ( manaBar.localScale.x < MAXMANABAR )
			{
				Debug.Log("update mana bar");
				float scaleX = manaBar.localScale.x + 0.5f * Time.deltaTime;
				manaBar.localScale = new Vector3(scaleX,manaBar.localScale.y,manaBar.localScale.z);
			}
//			animator.state.ClearTracks ();
			// check attack time
			if (attackType == 0) {		
				// MELEE
//				Debug.Log("melee " + attackSpeed + " isat " + isAttack);
				if (attackSpeed <= 0 && !isAttack) {
					// jika waktunya serang, SERANG!
					animator.state.ClearTracks ();
					isAttack = true;
					movementSpeed = stats.Movement;
					animator.state.AddAnimation (0, "run", true,0);
					rigidbody2D.velocity = Vector2.zero;
					PushForward (1f);
				} else {
					DeceleratePullBack ();
				}
			} else {
				if (attackSpeed <= 0 && !isAttack) {
					// jika waktunya serang, SERANG!
						isAttack = true;
					if (animator.state.GetCurrent (1) == null && attackType == 1) {
					//	Debug.Log("name " + name + "dodmage 0");
						animator.state.SetAnimation (0, "attack", false);
						animator.state.GetCurrent (0).Complete += HandleComplete;
					}

					//DoDamageToTarget
				}
			}//	Push();
			
		} else {
			// battle end

			rigidbody2D.velocity = Vector2.zero;
			isAttack = false;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (GameData.gameState != "Paused") {
						if (coll.gameObject.name.Contains ("wall")) {
								// push dikit
								rigidbody2D.velocity = Vector2.zero;
								PushForward (0.1f);
						} else if (coll.gameObject.name.Contains (target)) {
								rigidbody2D.velocity = Vector2.zero;
								targetUnit = coll.gameObject.GetComponent<HeroController> ();
								if (isAttack && attackType == 0) {
										
										if (animator.state.GetCurrent (1) == null) {
					//							Debug.Log("name " + name + "dodmage 0");
												animator.state.AddAnimation (1, "attack", false,0f);
												animator.state.GetCurrent (1).Complete += HandleComplete;
										}
								}
						} 
				}
	}


	void OnTriggerStay2D(Collider2D coll) {
		if (GameData.gameState != "Paused") {
						if (coll.gameObject.name.Contains ("wall")) {
								// push dikit
								rigidbody2D.velocity = Vector2.zero;
								PushForward (0.1f);
						} else if (coll.gameObject.name.Contains (target) && !CheckIsCornered ()) {
								targetUnit = coll.gameObject.GetComponent<HeroController> ();
								PushForward (-0.1f);
						} 
				}	
	}

	public bool CheckIsCornered(){
		bool ret = false;
		if (isHero) {
			if ( transform.position.x < -8 )
				ret = true;
		} else {
			if ( transform.position.x > 8 ) 
				ret = true;
		}
		return ret;
	}

	void HandleComplete (Spine.AnimationState state, int trackIndex, int loopCount)
	{
		if ( attackType == 0 ){
			float force = (stats.Str/100)+0.5f;
			DoDamageToTarget (targetUnit,-force);
			
		}
		else
			projectile.GetComponent<ProjectileController> ().Launch ();

		MusicManager.getMusicPlayer().audio.PlayOneShot(weaponSound);
		GetReadyForNextAttack ();
	}

	void HandleComplete1 (Spine.AnimationState state, int trackIndex, int loopCount)
	{
		transform.position = new Vector2 (-14f * direction, transform.position.y);
		Debug.Log("COMPLETED1");
		
		projectile.SetActive(false);
		
	}


	// jika F- => mundurin kita/musuh
	// F + => majuin kita/musuh
	public void PushForward(float f){
//		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce (new Vector2 (stats.Movement* direction *f, 0f));
		//		Debug.Log ("vel X " + rigidbody2D.angularVelocity);
		
	}

	public void DoDamageToTarget(HeroController h,float force){
		// h : target, force : force untuk dorong kebelakang
		// ATTACK!! kalau masih hidup
		if (stats.HealthPoint > 0) {
				// damage critical atau tidak, dimasukkan ke unit untuk dihitung evasion
				float damage = h.stats.ReceiveDamage(stats.Damage,stats.IsCritical,stats.StatsType);
				h.UpdateHealthBar ();
//				Debug.Log(stats.Job +" nggepuk " + h.stats.Job + " damage asli " + stats.Damage + " hasil " + damage);
				if ( !h.CheckIsCornered() ) // jika gk kepepet nusuhnya, pukul mundur
					h.PushForward(force);
				stats.IsCritical = false; // set critical ke semula, tapi chance tetep
				controller.ReceiveDamage (target, damage);
	//			GetReadyForNextAttack();
		}
	}

	void OnMouseDown(){

	}

	public void  DoSpecial(){
		if ( manaBar.localScale.x >= MAXMANABAR && skill.IsUnlocked ){
			controller.ActivateSkillShade(0f,0.1f);
			animator.state.ClearTracks();
			manaBar.localScale = new Vector3(0f,manaBar.localScale.y,manaBar.localScale.z);
			isDoSpecial = true;
			controller.BatlleState = 99;
			animator.state.AddAnimation(1,"special",false,0f);
			animator.state.GetCurrent (1).Complete += HandleComplete2;
			Debug.Log(stats.Job+" special");
		}
	}

	void HandleComplete2 (Spine.AnimationState state, int trackIndex, int loopCount)
	{
		MusicManager.getMusicPlayer().audio.PlayOneShot(skillSound);
		animator.state.ClearTracks();
		List<GameObject> unitList = skill.ActiveSkillEffect.Tipe == 1 ? 
									controller.enemyList : controller.heroList;
		Debug.Log ( "ENEMT LIUT " + unitList.Count);
		foreach( GameObject t in unitList ){
			if ( skill.IsInRange(transform.position.x,t.transform.position.x) && t.activeInHierarchy ){
				HeroController u = t.GetComponent<HeroController>();
				float dmg = skill.DoActiveEffect(stats.Damage,u.stats);
				u.stats.ReceiveDamage(dmg,false,stats.StatsType);
				u.UpdateHealthBar ();
				controller.ReceiveDamage (target, dmg);
			}
		}
		controller.BatlleState = 0;
		isDoSpecial = false;
		animator.state.SetAnimation(1,"idle",true);
		animator.state.ClearTracks();
		animator.state.SetAnimation(0,"idle",true);
		controller.DeactivateSkillShade(0f,0.1f);
	}



	public void GetReadyForNextAttack(){
		isAttack = false;
		attackSpeed = stats.AttackSpeed;
		movementSpeed = stats.Movement;
		//animator.state.ClearTracks ();
		if (animator.state.GetCurrent (1) == null) {
		//	Debug.Log("name " + name + "nul 1");
		}
		else
	//		Debug.Log("name " + name + "gak nul 1");
		
		animator.state.ClearTracks ();
		//animator.state.AddAnimation (0, "idle", false,1.5f);
		animator.state.AddAnimation (0, "idle", true,0.5f);
	}

	public void DeceleratePullBack(){
		// semakin gede agi, semakin gede pushForce supaya gak jauh2 kebelakangnya.
		if ( rigidbody2D.velocity.x < 0 && direction == 1 || rigidbody2D.velocity.x > 0 && direction == -1)
			rigidbody2D.AddForce (new Vector2 (stats.PushForce * direction, 0f));
	}

	public void UpdateHealthBar(){

		float scaleX = (stats.HealthPoint * healthScaleConstant / healthConstant).x;
		//if (this.gameObject.name.Contains ("hero"))
	//		Debug.Log ("health " + stats.HealthPoint  +" sacle " + scaleX);
		healthBar.transform.localScale = new Vector3(scaleX,
		                                             healthScaleConstant.y,
		                                             healthScaleConstant.z);

	}

	public void MoveToGraveyard(){
		if (this.gameObject.name.Contains ("enemy")) {
			var m = GameData.profile.questList.Where(x => x.Target.Contains("defeat")).ToList();
			foreach( Quest q in m )
				q.CurrentQuantity++;
		}
		isAttack = false;
		rigidbody2D.velocity = Vector2.zero;
		healthBar.SetActive(false);
		animator.state.ClearTracks();
		animator.state.AddAnimation(0,"die",false,0);
		animator.state.GetCurrent (0).Complete += HandleComplete1;

		//UpdateHealthBar ();
		//this.gameObject.SetActive (false);	
	}



	public bool IsAttack {
		get {
			return isAttack;
		}
		set {
			isAttack = value;
		}
	}
	public float AntiBug {
		get {
			return antiBug;
		}
	}
	public float MovementSpeed {
		get {
			return movementSpeed;
		}
	}
	
	public bool IsDeath {
		get {
			return isDeath;
		}
	}

}

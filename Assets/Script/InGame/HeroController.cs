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
	public SpriteRenderer lockSprite;
	public Unit stats;
	public GameObject projectile;
	//private Animator animator;

	public string target;
	private Vector3 healthScaleConstant;
	private float movementSpeed = 0f;
	public float MovementSpeed {
		get {
			return movementSpeed;
		}
	}

   // kecepatan gerak
	public float antiBug = 0f;
	private int states = 0;
	private bool isHero = false;
	public float AntiBug {
		get {
			return antiBug;
		}
	}

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

	public bool IsDeath {
		get {
			return isDeath;
		}
	}


	// Use this for initialization
	void Start () {
	//	Debug.Log ("HERO START");
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
//			Debug.Log("di hero cont " + stats.HeroId);
			InitializeWeapon();
			InitializePosition(-1);
		} else if (gameObject.name.Contains ("enemy")) {
			stats =  controller.activeEnemyList[slot];
			isHero = false;
			InitializeWeapon();
			InitializePosition(1);
		}
		animator.skeletonDataAsset = GameData.skeleteonDataAssetList[stats.HeroId];
		animator.calculateNormals = true;
		animator.loop = true;
		animator.Awake ();
		animator.state.SetAnimation (0, "run", true);
		healthConstant = stats.HealthPoint;
		healthScaleConstant = healthBar.transform.localScale;
		movementSpeed = stats.Movement;
	}
	
	// Update is called once per frame
	void Update () {
				if (this.stats.HealthPoint <= 0) {
					isDeath = true;
					MoveToGraveyard();
				}
				// jika masih battle
				if (controller.BatlleState == 0 && GameData.gameState != "Paused") {
						attackSpeed -= Time.deltaTime;
						// check attack time
						if (attackType == 0) {		
								// MELEE
								if (attackSpeed <= 0 && !isAttack) {
										// jika waktunya serang, SERANG!
										isAttack = true;
										movementSpeed = stats.Movement;
										PushForward (1f);
								} else {
										DeceleratePullBack ();
								}
						} else {
								if (attackSpeed <= 0 && !isAttack) {
										// jika waktunya serang, SERANG!
										isAttack = true;
										projectile.GetComponent<ProjectileController> ().Launch ();
										//DoDamageToTarget
								}
						}//	Push();

				} else {
				// battle end
			rigidbody2D.velocity = Vector2.zero;
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
												animator.state.ClearTrack (0);
												animator.state.SetAnimation (1, "attack", false);
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
		DoDamageToTarget (targetUnit,-0.5f);
		animator.state.ClearTracks ();
		animator.state.AddAnimation (0, "run", true, -0.33f);

	}

	// jika F- => mundurin kita/musuh
	// F + => majuin kita/musuh
	public void PushForward(float f){
//		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce (new Vector2 (stats.Movement* direction *f, 0f));
		//		Debug.Log ("vel X " + rigidbody2D.angularVelocity);
		
	}

	public void DoDamageToTarget(HeroController h,float force){
		// ATTACK!! kalau masih hidup
		if (stats.HealthPoint > 0) {
				// damage critical atau tidak, dimasukkan ke unit untuk dihitung evasion
				//Debug.Log("damaged unit " + gameObject.name +" health " + stats.HealthPoint);
				float damage = h.stats.ReceiveDamage(stats.Damage,stats.IsCritical);
				//Debug.Log(" damage " + damage);
				if ( !h.CheckIsCornered() ) // jika gk kepepet nusuhnya, pukul mundur
					h.PushForward(force);
				stats.IsCritical = false; // set critical ke semula, tapi chance tetep
				controller.ReceiveDamage (target, damage);
				h.UpdateHealthBar ();
				GetReadyForNextAttack ();
		}
	}

	public void GetReadyForNextAttack(){
		isAttack = false;
		attackSpeed = stats.AttackSpeed;
		movementSpeed = stats.Movement;
		if (animator.state.GetCurrent (0) == null) {
			Debug.Log("1 null");		
		}
		else
			Debug.Log("1 gak null");	
		animator.state.SetAnimation (0, "run", true);
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
		transform.position = new Vector2 (-11f, transform.position.y);
		projectile.SetActive(false);
		UpdateHealthBar ();
		this.gameObject.SetActive (false);	
//		Debug.Log("DIIEEEE");
	}

	void InitializeWeapon(){
		if (stats.Weapon.Range == 5) {
			attackType = 1;		
			projectile.SetActive(true);
			Debug.Log("name " + name + " isarcher " );
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

	public bool IsAttack {
		get {
			return isAttack;
		}
		set {
			isAttack = value;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
Bug list

 */

public class HeroController : MonoBehaviour {

	public List<GameObject> enemyList;
	public BattleController controller;
	public GameObject healthBar;
	public SpriteRenderer lockSprite;
	public Unit stats;
	public GameObject projectile;
	//private Animator animator;

	public string target;
	private Vector3 healthScaleConstant;
	private float movementSpeed = 0f;   // kecepatan gerak
	private int states = 0; 			// menentukan animasi
	public int direction = 1;
	private int attackType = 0; // 0  melee, 1 range
	private float attackSpeed = 0f;
	private float healthConstant;
	private bool isAttack = false;
	private bool isChangeState = true;
	public int slot;
	public SkeletonAnimation animator;

	// Use this for initialization
	void Start () {

		attackSpeed = 0;
		if (gameObject.activeInHierarchy) {
			healthBar.SetActive (true);
			lockSprite.enabled = false;
			}
		if (gameObject.name.Contains ("hero")) {
			stats = GameData.formationList[slot].Unit;
//			Debug.Log("di hero cont " + slot + " " + stats.HealthPoint);
			InitializeWeapon();
			InitializePosition(-1);
		} else if (gameObject.name.Contains ("enemy")) {
				stats = controller.activeEnemyList[slot];
			InitializeWeapon();
			InitializePosition(1);
		}
		stats.SetSpine ();
		animator.skeletonDataAsset = stats.PlayerData;
		animator.calculateNormals = true;
		animator.loop = true;
		animator.Awake ();
		animator.state.SetAnimation (0, "jalan2", true);
		//animator.AnimationName = "jalan2";

		// set aktif gak nya
		//if (!GameData.unitList[slot].IsUnlocked && this.gameObject.name.Contains("hero")) {
		//	gameObject.SetActive(false);
		//	healthBar.SetActive (false);
		//}
		
	//	Debug.Log ("str " + stats.Str + " agi " + stats.Agi +"vit " + stats.Vit);
	//	Debug.Log ("hp " + stats.HealthPoint + "aspd " + stats.AttackSpeed + "crit " + stats.Critical);
	//	Debug.Log ("atk " + stats.AttackPoint +" def " + stats.DefensePoint + " ms " + stats.Movement);
		healthConstant = stats.HealthPoint;
		healthScaleConstant = healthBar.transform.localScale;
		movementSpeed = stats.Movement;
	//	rigidbody2D.AddForce (new Vector2 (movementSpeed  * direction, 0f));

	}
	
	// Update is called once per frame
	void Update () {
				if (this.stats.HealthPoint <= 0) {
					MoveToGraveyard();
				}
				// jika masih battle
		if (controller.BatlleState == 0) {
						attackSpeed -= Time.deltaTime;
						// check attack time
						if (attackType == 0) {		
								// MELEE
								if (attackSpeed <= 0 && !isAttack) {
										// jika waktunya serang, SERANG!
										isAttack = true;
										movementSpeed = stats.Movement;
										PushForward ();
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

	void InitializeWeapon(){
		if (stats.Weapon.Range == 5) {
			attackType = 1;		
			projectile.SetActive(true);
		}

	}

	public void PushForward(){
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce (new Vector2 (stats.Movement* direction, 0f));

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name.Contains (target) && isAttack && attackType == 0 ) {
			HeroController h = coll.gameObject.GetComponent<HeroController>();
			DoDamageToTarget(h);
		} else if (coll.gameObject.name.Contains ("wall")) {
			// push dikit
			rigidbody2D.velocity = Vector2.zero;
		}
		
	}
	public void DoDamageToTarget(HeroController h){
		// ATTACK!!
		if (stats.HealthPoint > 0) {
				// damage critical atau tidak, dimasukkan ke unit untuk dihitung evasion
				float damage = h.stats.ReceiveDamage(stats.Damage,stats.IsCritical);
				controller.ReceiveDamage (target, damage);
				h.UpdateHealthBar ();
				GetReadyForNextAttack ();
		}
	}

	void GetReadyForNextAttack(){
		isAttack = false;
		attackSpeed = stats.AttackSpeed;
		movementSpeed = stats.Movement;
		// jika tidak, mundur
		if (attackType == 0) {
						rigidbody2D.velocity = Vector2.zero;
						rigidbody2D.AddForce (new Vector2 (stats.Movement * direction * -1, 0f));
				}
		states = 2;
		isChangeState = true;
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
		transform.position = new Vector2 (-11f, transform.position.y);
		projectile.SetActive(false);
		UpdateHealthBar ();
		this.gameObject.SetActive (false);	
		Debug.Log("DIIEEEE");
	}

	void InitializePosition(int pos){

		int dest = 0;
		dest = RandomPos (dest);
		while (!controller.PositionAvailableList[dest]) {
			dest = RandomPos (dest);
		}
		controller.PositionAvailableList [dest] = false;
		transform.position = new Vector2 (controller.PositionList[dest] * pos, transform.position.y);
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

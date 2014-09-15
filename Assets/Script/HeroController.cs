using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
Bug list

 */

public class HeroController : MonoBehaviour {

	public List<GameObject> enemyList;
	public BattleController gameplayController;
	public GameObject healthBar;
	public SpriteRenderer lockSprite;
	public Unit stats;
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

	// Use this for initialization
	void Start () {
		attackSpeed = 0;
	//	animator = this.GetComponent<Animator>();
	//	InitializePosition (Random.Range (0, 4));
		if (gameObject.activeInHierarchy) {
			healthBar.SetActive (true);
			lockSprite.enabled = false;
//			Debug.Log("AKTIF " + name + " " + slot);
		}
		if (gameObject.name.Contains ("hero")) {
		//	if ( gameplayController.activeHeroList[slot] != null ){
		//		gameplayController.activeHeroList[slot].Refresh();
			stats = GameData.formationList[slot].Unit;
			Debug.Log("di hero cont " + slot + " " + stats.HealthPoint);
		//	}
		} else if (gameObject.name.Contains ("enemy")) {
		//	if ( gameplayController.activeEnemyList[slot] != null ){
		//		gameplayController.activeEnemyList[slot].Refresh();
				stats = gameplayController.activeEnemyList[slot];
		//	}
		}
		if (stats.Job == "archer" || stats.Job == "mage") {
			attackType = 1;		
		}

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

	}
	
	// Update is called once per frame
	void Update () {
						if (this.stats.HealthPoint <= 0) {
								//	Debug.Log("die");
								UpdateHealthBar ();
								MoveToGraveyard ();
								this.gameObject.SetActive (false);		
						}
		// jika masih battle
		if (gameplayController.BatlleState == 0) {
			attackSpeed -= Time.deltaTime;

						//change animation
						if (isChangeState) {
								//		animator.SetInteger("states",states);
								isChangeState = false;		
						}

						// check attack time
						if (attackSpeed <= 0 && !isAttack) {
								isAttack = true;
								movementSpeed = stats.Movement;
						}

						// jika attack dorong ke depan
						if (isAttack) {
								Attack ();
						} else {
								PullBack ();
						}
				}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.name.Contains (target) && isAttack ) {
			HeroController h = coll.collider.gameObject.GetComponent<HeroController>();
			DoDamageToTarget(h);
		} else if (coll.collider.name.Contains ("wall")) {
			// push dikit
			rigidbody2D.AddForce (new Vector2 (movementSpeed * rigidbody2D.mass * direction, 0f));
		}
		
	}
	void DoDamageToTarget(HeroController h){
		float damage = h.stats.AttackPoint;
		h.stats.HealthPoint -= damage;
		gameplayController.ReceiveDamage(target,damage);
		h.UpdateHealthBar ();
		GetReadyForNextAttack();
	}

	void GetReadyForNextAttack(){
		isAttack = false;
		attackSpeed = stats.AttackSpeed;
		movementSpeed = stats.Movement;
		states = 2;
		isChangeState = true;
	}

	void Attack(){
		rigidbody2D.AddForce (new Vector2 (movementSpeed * rigidbody2D.mass * direction, 0f));
		states = 1;
		isChangeState = true;
		movementSpeed *= 1.03f;
		
		if (movementSpeed >= 2f)
			movementSpeed = 2f;
	}

	void PullBack(){
		// jika tidak, mundur
		this.rigidbody2D.AddForce (new Vector2 (movementSpeed  * direction * -1, 0f));
		states = 0;
		isChangeState = true;
		// perlambatan gerakan
		movementSpeed *= 0.9f;
	}

	public void UpdateHealthBar(){
		float scaleX = (stats.HealthPoint * healthScaleConstant / healthConstant).x;
		//if (this.gameObject.name.Contains ("hero"))
	//		Debug.Log ("health " + stats.HealthPoint  +" sacle " + scaleX);
		healthBar.transform.localScale = new Vector3(scaleX,
		                                             healthScaleConstant.y,
		                                             healthScaleConstant.z);

	}

	void MoveToGraveyard(){
		transform.position = new Vector2 (-12f, transform.position.y);
	}

	void InitializePosition(int pos){
		transform.position = new Vector2 (pos * 0.5f - 4.5f, transform.position.y);
	}
}

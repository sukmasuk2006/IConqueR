using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
Bug list

 */

public class HeroController : MonoBehaviour {

	public List<GameObject> enemyList;
	public GameplayController gameplayController;
	public GameObject healthBar;

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
		if ( gameObject.name.Contains("hero"))
			stats = GameData.unitList [slot];
		else if ( gameObject.name.Contains("enemy"))
			stats = GameData.enemyList [slot];

		if (stats.Job == "archer" || stats.Job == "mage") {
			attackType = 1;		
		}
		if (!GameData.unitList[slot].IsUnlocked) {
			healthBar.SetActive (false);
			gameObject.SetActive(false);
		}
		
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
			MoveToGraveyard();
			this.gameObject.SetActive(false);		
		}
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
			Attack();
		}
		else {
			PullBack();
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.name.Contains (target) && isAttack ) {
			Unit h = coll.collider.gameObject.GetComponent<HeroController>().stats;
			DoDamageToTarget(h);
		} else if (coll.collider.name.Contains ("wall")) {
			movementSpeed = 0f;
		}
		
	}
	void DoDamageToTarget(Unit h){
		float damage = stats.AttackPoint;
		h.HealthPoint -= damage;
		gameplayController.ReceiveDamage(target,damage);
		GetReadyForNextAttack();
		UpdateHealthBar();
	}

	void GetReadyForNextAttack(){
		isAttack = false;
		attackSpeed = stats.AttackSpeed;
		movementSpeed = stats.Movement * 3;
		states = 2;
		isChangeState = true;
	}

	void Attack(){
		rigidbody2D.AddForce (new Vector2 (movementSpeed * rigidbody2D.mass * direction, 0f));
		states = 1;
		isChangeState = true;
		if ( movementSpeed <= 3f )
			movementSpeed *= 1.03f;
	}

	void PullBack(){
		// jika tidak, mundur
		this.rigidbody2D.AddForce (new Vector2 (movementSpeed * rigidbody2D.mass * direction * -1, 0f));
		states = 0;
		isChangeState = true;
		// perlambatan gerakan
		movementSpeed *= 0.9f;
	}

	void UpdateHealthBar(){
		healthBar.transform.localScale = new Vector3((stats.HealthPoint * healthScaleConstant 
		                                              / healthConstant).x,healthScaleConstant.y,
		                                             healthScaleConstant.z);
	}

	void MoveToGraveyard(){
		transform.position = new Vector2 (-12f, transform.position.y);
		this.gameObject.SetActive(false);		
	}

	void InitializePosition(int pos){
		transform.position = new Vector2 (pos * 0.5f - 4.5f, transform.position.y);
	}
}

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
	public SpriteRenderer spriteRenderer;
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
		if (gameObject.activeInHierarchy) {
			healthBar.SetActive (true);
			lockSprite.enabled = false;
			spriteRenderer.sprite = GameData.formationList[slot].Unit.Icon;
		}
		if (gameObject.name.Contains ("hero")) {
			stats = GameData.formationList[slot].Unit;
//			Debug.Log("di hero cont " + slot + " " + stats.HealthPoint);
			InitializePosition(-1);
		} else if (gameObject.name.Contains ("enemy")) {
				stats = controller.activeEnemyList[slot];
			InitializePosition(1);
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
	//	rigidbody2D.AddForce (new Vector2 (movementSpeed  * direction, 0f));

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
				if (controller.BatlleState == 0) {
					attackSpeed -= Time.deltaTime;

								//change animation
								if (isChangeState) {
										//		animator.SetInteger("states",states);
										isChangeState = false;		
								}

								// check attack time
								if (attackSpeed <= 0 && !isAttack) {
		// jika waktunya serang, SERANG!
										isAttack = true;
										movementSpeed = stats.Movement;
									rigidbody2D.velocity = Vector2.zero;
									rigidbody2D.AddForce (new Vector2 (stats.Movement* direction, 0f));
								}
								else if (!isAttack)
								{
									DeceleratePullBack();
								}
							//	Push();
						}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name.Contains (target) && isAttack ) {
			HeroController h = coll.gameObject.GetComponent<HeroController>();
			DoDamageToTarget(h);
		} else if (coll.gameObject.name.Contains ("wall")) {
			// push dikit
			rigidbody2D.velocity = Vector2.zero;
		}
		
	}
	void DoDamageToTarget(HeroController h){
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
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce (new Vector2 (stats.Movement * direction * -1, 0f));
		states = 2;
		isChangeState = true;
	}

	void DeceleratePullBack(){
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

	void MoveToGraveyard(){
		transform.position = new Vector2 (-11f, transform.position.y);
	}

	void InitializePosition(int pos){
		int dest = Random.Range (2, 9);
		transform.position = new Vector2 (dest * pos, transform.position.y);
	}
}

using UnityEngine;
using System.Collections;

public class characterMove : MonoBehaviour {
	
	public int direction = 1;
	public float moveSpeed = 0f;
	private float moveAttack = 0f;
	public float cooldown = 1f;
	private float attackTime = 0f;
	private bool isAttack = false;
	private Animator animator;
	// Use this for initialization
	void Start () {
		attackTime = cooldown;
		moveAttack = moveSpeed;
		animator = this.GetComponent<Animator>();
	}
	
	void Awake(){
		
	}
	
	// Update is called once per frame
	void Update () {
		attackTime -= Time.deltaTime;
		if (attackTime <= 0) {
			isAttack = true;
		}
		if (isAttack) {
			this.rigidbody2D.AddForce (new Vector2 (moveAttack * rigidbody2D.mass * direction, 0f));
			//		animator.SetInteger("states",0);
			moveAttack *= 1.005f;
		}
		else {
			this.rigidbody2D.AddForce (new Vector2 (moveAttack * rigidbody2D.mass * direction * -1, 0f));
			//		animator.SetInteger("states",2);
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.name.Contains ("knight") && isAttack) {
			//		direction *= -1;
			attackTime = cooldown;
			isAttack = false;
			animator.SetInteger("states",0);
			moveAttack = moveSpeed;
		} else if (coll.collider.name.Contains ("wall")) {
			attackTime = cooldown;
			isAttack = false;
			animator.SetInteger("states",1);
			moveAttack = moveSpeed;
			//direction *= -1;
		}
		
	}
}

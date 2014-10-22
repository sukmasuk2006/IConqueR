using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	public HeroController heroController;
	private bool isLaunch = false;
	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = 
			(Sprite)Resources.Load ("Sprite/Ammo/" + heroController.stats.Job, typeof(Sprite));
		SetPos ();
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.name.Contains(heroController.target) ) {
			isCollided();
			Debug.Log("DOR with " + coll.gameObject.name);
			HeroController h = coll.gameObject.GetComponent<HeroController> ();
						//h.PushForward ();
			isLaunch = false;
			heroController.DoDamageToTarget (h,0f);
					
		}
	}

	public void Launch(){
		Debug.Log("Launch");
		SetPos ();
		IsLaunch = true;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce(new Vector2(500f * heroController.direction,0f));
	}

	void SetPos(){
		Vector3 heropos = new Vector3 (heroController.gameObject.transform.position.x + (0.5f * heroController.direction),
		                              heroController.gameObject.transform.position.y + 0.5f,
		                              heroController.gameObject.transform.position.z);

		this.gameObject.transform.position = heropos;
	}

	void isCollided(){
		Vector3 heropos = new Vector3 (0,-12f,0f);
		this.gameObject.transform.position = heropos;
	}

	public bool IsLaunch {
		get {
			return isLaunch;
		}
		set {
			isLaunch = value;
		}
	}
}

using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	public HeroController heroController;
	private bool isLaunch = false;
	// Use this for initialization
	void Start () {
		SetPos ();
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.name.Contains(heroController.target) ) {
					
			Debug.Log("DOR with " + coll.gameObject.name);
			HeroController h = coll.gameObject.GetComponent<HeroController> ();
						//h.PushForward ();
			isLaunch = false;
			heroController.DoDamageToTarget (h,0f);
					
		}
	}

	public void Launch(){
		Debug.Log("Launch");
		IsLaunch = true;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce(new Vector2(500f * heroController.direction,0f));
	}


	void Update(){
	if (!isLaunch)
		SetPos ();
	}

	void SetPos(){
		Vector3 heropos = new Vector3 (heroController.gameObject.transform.position.x,
		                              heroController.gameObject.transform.position.y + 0.5f,
		                              heroController.gameObject.transform.position.z);

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

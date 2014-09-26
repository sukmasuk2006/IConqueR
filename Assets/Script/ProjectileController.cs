using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	public HeroController heroController;
	private bool isLaunch = false;
	// Use this for initialization
	void Start () {
		this.gameObject.transform.position = heroController.gameObject.transform.position;
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.name != "wall"  && isLaunch && coll.gameObject.name != "projectile" ) {
					
			Debug.Log("DOR");
			HeroController h = coll.gameObject.GetComponent<HeroController> ();
						//h.PushForward ();
						isLaunch = false;
						heroController.DoDamageToTarget (h);
					
		}
	}

	public void Launch(){
		Debug.Log("Launch");
		IsLaunch = true;
		
		rigidbody2D.AddForce(new Vector2(600f * heroController.direction,0f));
	}


	void Update(){
	if (!isLaunch )
			this.gameObject.transform.position = heroController.gameObject.transform.position;
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

using UnityEngine;
using System.Collections;

public class HeroSetter : MonoBehaviour {

	public BaseCampController baseCampController;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		baseCampController.SendMessage ("SetActiveHeroes");
	}
}

using UnityEngine;
using System.Collections;

public class SkillActivate : MonoBehaviour {

	public Animator animator;
	
	public int skillNumber;

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown(){
		animator.SetInteger("states",skillNumber);

	}
	// Update is called once per frame
	void Update () {
	
	}
}

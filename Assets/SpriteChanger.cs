using UnityEngine;
using System.Collections;

public class SpriteChanger : MonoBehaviour {

	public UISprite sprite;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnClick(){
		sprite.spriteName = "satria";		
	}
}

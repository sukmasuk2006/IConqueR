using UnityEngine;
using System.Collections.Generic;

public class ShopCategoryChanger : MonoBehaviour {

	public List<GameObject> controller;
	public ScreenData data;
	public int state;
	public TextMesh corridorState;
	public Sprite activeSprite;
	public Sprite deactiveSprite;
	private SpriteRenderer ren;
	public SpriteRenderer otherButton;
	// Use this for initialization
	void Start () {
		ren = gameObject.GetComponent<SpriteRenderer>();
		ren.sprite = state == 0 ? activeSprite : deactiveSprite;
	}
	
	void OnMouseDown(){
		data.shopState = state;
		data.corridorState = state == 0 ? 0 : 9;
		data.maxCorridorState = state == 0 ? 8 : 1;
		for ( int i = 0 ; i < controller.Count ; i++ )
			if ( data.shopState == 0 )
				controller[i].GetComponent<ShopSlotSetter>().UpdateSlotGem ();
			else
				controller[i].GetComponent<ShopSlotSetter>().UpdateSlotCatalyst ();
		
		corridorState.text = "Page 1";
		CheckButtonColor();
		Debug.Log("setshop");
	}

	void CheckButtonColor(){
		otherButton.sprite = deactiveSprite;
		ren.sprite = activeSprite;
	}
}

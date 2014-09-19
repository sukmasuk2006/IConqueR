using UnityEngine;
using System.Collections.Generic;

public class ShopController : MonoBehaviour {

	private bool isMouseDown = false;

	public SpriteRenderer item0;
	public SpriteRenderer item1;
	public SpriteRenderer item2;
	public SpriteRenderer item3;
	
	public TextMesh item0Name;
	public TextMesh item1Name;
	public TextMesh item2Name;
	public TextMesh item3Name;
	
	public TextMesh item0Price;
	public TextMesh item1Price;
	public TextMesh item2Price;
	public TextMesh item3Price;

	const int SLOT_ONE = 1;
	const int SLOT_TWO = 2;
	const int SLOT_THREE = 3;
	const int SLOT_ZERO = 0;

	public ScreenData data;
	private Sprite spriteTemp;

	void Start () {
		UpdateShop ();
	}

	private Sprite SetSprite(string name){
		spriteTemp = (Sprite)Resources.Load ("Sprite/Gems/" + name, typeof(Sprite));
		return spriteTemp;
	}

	public void UpdateShop(){
		item0.sprite = SetSprite(GameData.inventoryList[(data.corridorState*4)+0].Name);
		item1.sprite = SetSprite(GameData.inventoryList[(data.corridorState*4)+1].Name);
		item2.sprite = SetSprite(GameData.inventoryList[(data.corridorState*4)+2].Name);
		item3.sprite = SetSprite(GameData.inventoryList[(data.corridorState*4)+3].Name);
		
		item0Name.text = GameData.inventoryList[(data.corridorState*4)+0].Name;
		item1Name.text = GameData.inventoryList[(data.corridorState*4)+1].Name;
		item2Name.text = GameData.inventoryList[(data.corridorState*4)+2].Name;
		item3Name.text = GameData.inventoryList[(data.corridorState*4)+3].Name;
		
		item0Price.text = GameData.inventoryList[(data.corridorState*4)+0].Price.ToString();
		item1Price.text = GameData.inventoryList[(data.corridorState*4)+1].Price.ToString();
		item2Price.text = GameData.inventoryList[(data.corridorState*4)+2].Price.ToString();
		item3Price.text = GameData.inventoryList[(data.corridorState*4)+3].Price.ToString();
	}

	// Update is called once per frame
	void Update () {
	/*	if (isMouseDown) {
						Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

						if (Input.GetAxis ("Mouse Y") < 0 || Input.GetAxis ("Mouse Y") > 0) {
				screen.transform.position = new Vector3 (screen.transform.position.x, mousePos.y, screen.transform.position.z);
								print ("Mouse moved up");
						}
				}*/
	}

	void ShowItem(){
	
	}


	void OnMouseDown(){
		isMouseDown = true;
	}

	void OnMouseUp(){
		isMouseDown = false;
	}

}

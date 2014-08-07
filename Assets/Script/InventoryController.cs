using UnityEngine;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {

	public GameObject screen;
	private bool isMouseDown = false;

	public GameObject item0;
	public GameObject item1;
	public GameObject item2;
	public GameObject item3;
	
	public TextMesh item0Name;
	public TextMesh item1Name;
	public TextMesh item2Name;
	public TextMesh item3Name;
	
	public TextMesh item0Stock;
	public TextMesh item1Stock;
	public TextMesh item2Stock;
	public TextMesh item3Stock;
	
	public TextMesh descriptionText;
	
	const int SLOT_ONE = 1;
	const int SLOT_TWO = 2;
	const int SLOT_THREE = 3;
	const int SLOT_ZERO = 0;

	private Item item;

	void Start () {
//		selectorSprite = Instantiate (tileSelectionMarker, Vector3 (0, 0, 0), Quaternion.identity);
		Debug.Log ("GO");	GameData.inventoryList = new List<Item> ();
		GameData.inventoryList.Add (new Item ("gold_bar", 1, 1, 1, 1, 1));
		GameData.inventoryList.Add (new Item ("silver_bar", 1, 1, 1, 1, 1));
		GameData.inventoryList.Add (new Item ("gold_ore", 1, 1, 1, 1, 1));
		GameData.inventoryList.Add (new Item ("steel_bar", 1, 1, 1, 1, 1));
		GameData.inventoryList.Add (new Item ("coal", 1, 1, 1, 1, 1));
		GameData.inventoryList.Add (new Item ("gold_bar", 1, 1, 1, 1, 1));
		GameData.inventoryList.Add (new Item ("silver_bar", 1, 1, 1, 1, 1));
		GameData.inventoryList.Add (new Item ("gold_ore", 1, 1, 1, 1, 1));
		GameData.inventoryList.Add (new Item ("steel_bar", 1, 1, 1, 1, 1));
		GameData.inventoryList.Add (new Item ("coal", 1, 1, 1, 1, 1));
//		item1.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprite/Gems/Common Gems/brass",typeof(Sprite));
		descriptionText.text = "Click on the gem\nto view details";
		ShowItem ();

	}

	void Alert(){
		ShowItem ();

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
		if (GameData.inventoryList [GameData.index * SLOT_ZERO] != null) {
			item = null;
			item = GameData.inventoryList [GameData.index * SLOT_ZERO];
			item0.GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.Load ("Sprite/Gems/Common Gems/" +
                       item.Name, typeof(Sprite));
			item0Name.text = item.Name;
		}
		if (GameData.inventoryList [GameData.index * SLOT_ONE] != null) {
			item = null;
			item = GameData.inventoryList [GameData.index * SLOT_ONE];
			item1.GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.Load ("Sprite/Gems/Common Gems/" +
                       item.Name, typeof(Sprite));
			item1Name.text = item.Name;
		}
		if (GameData.inventoryList [GameData.index * SLOT_TWO] != null) {
			item = null;
			item = GameData.inventoryList [GameData.index * SLOT_TWO];
			item2.GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.Load ("Sprite/Gems/Common Gems/" +
                       item.Name, typeof(Sprite));
			item2Name.text = item.Name;
		}
		if (GameData.inventoryList [GameData.index * SLOT_THREE] != null) {
			item = null;
			item = GameData.inventoryList [GameData.index * SLOT_THREE];
			item3.GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.Load ("Sprite/Gems/Common Gems/" +
		               item.Name, typeof(Sprite));
			item3Name.text = item.Name;
		}
	}


	void OnMouseDown(){
		isMouseDown = true;
	}

	void OnMouseUp(){
		isMouseDown = false;
	}

}

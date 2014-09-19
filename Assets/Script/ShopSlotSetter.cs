using UnityEngine;
using System.Collections;
using System.Linq;

public class ShopSlotSetter : MonoBehaviour {
	
	public ScreenData data;
	public SpriteRenderer spriteRenderer;
	public int slot;
	public TextMesh teks;
	public TextMesh priceText;
	private string path = "";
	
	// Use this for initialization
	void Start () {
		UpdateSlot ();
	}
	
	public void UpdateSlot(){
		Sprite s = null;
		if (GameData.shopList.Count > (4 * data.corridorState)+slot) {
			s = GameData.shopList [(4 * data.corridorState)+slot].Sprites;
			teks.text = GameData.shopList [(4 * data.corridorState)+slot].Name; 
			priceText.text = GameData.shopList[(4 * data.corridorState)+slot].Price.ToString(); 
		}
		spriteRenderer.sprite = s;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

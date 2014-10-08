using UnityEngine;
using System.Collections;

public class ShopSlotSetter : MonoBehaviour {
	
	public ScreenData data;
	public SpriteRenderer spriteRenderer;
	public int slot;
	public TextMesh name;
	public TextMesh grade;
	public TextMesh str;
	public TextMesh agi;
	public TextMesh vit;
	public TextMesh price;
	private string path = "";
	public Sprite goldSprite;
	public Sprite diamondSprite;
	public SpriteRenderer priceTypeSprite;

	// Use this for initialization
	void Start () {
		UpdateSlot ();
	}
	
	public void UpdateSlot(){
		Sprite s = null;
		if (GameData.shopList.Count > (4 * data.corridorState)+slot) {
			if ( GameData.shopList[(4 * data.corridorState)+slot] is Gem ){
				Gem g = (Gem)GameData.shopList[(4 * data.corridorState)+slot];
				s = GameData.gemSpriteList[g.Id];
				name.text = g.Name; 
				grade.text = g.Grade;
				str.text = "Str + " + g.Stats.Str.ToString();
				agi.text = "Agi + " + g.Stats.Agi.ToString();
				vit.text = "Vit + " + g.Stats.Vit.ToString();
				price.text = g.Price.ToString(); 
				if ( g.PriceType == 0 ) // gold
					priceTypeSprite.sprite = goldSprite;
				else
					priceTypeSprite.sprite = diamondSprite;
			}
			else{
				Catalyst g = (Catalyst)GameData.shopList[(4 * data.corridorState)+slot];
				s = GameData.catalystSpriteList[g.Id];
				name.text = g.Desc; 
				grade.text = g.Name;
				str.text = "Increase upgrade";
				agi.text = "success rate";
				vit.text = "by " + g.SuccessRate.ToString();
				price.text = g.Price.ToString(); 
				priceTypeSprite.sprite = goldSprite;
			}
		}
		spriteRenderer.sprite = s;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

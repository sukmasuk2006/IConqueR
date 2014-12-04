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
	public GameObject statsInfo;

	private Color defaultColor;


	// Use this for initialization
	void Start () {
		UpdateSlotGem ();
		defaultColor = grade.color;
	}
	
	public void UpdateSlotGem(){
		statsInfo.SetActive(true);
		Sprite s = null;
		if (GameData.shopList.Count > (4 * data.corridorState)+slot) {
			Gem g = (Gem)GameData.shopList[(4 * data.corridorState)+slot];
				s = GameData.gemSpriteList[g.Id];
				name.text = g.Name; 
				grade.text = g.Grade;
				grade.color = GetColor(g.Grade);
				str.text =  g.Stats.Str.ToString();
				agi.text =  g.Stats.Agi.ToString();
				vit.text =  g.Stats.Vit.ToString();
				price.text = g.Price.ToString(); 
				if ( g.PriceType == 0 ) // gold
					priceTypeSprite.sprite = goldSprite;
				else
					priceTypeSprite.sprite = diamondSprite;
		}
		spriteRenderer.sprite = s;
	}

	public void UpdateSlotCatalyst(){
		statsInfo.SetActive(false);
		Sprite s = null;
		Catalyst g = (Catalyst)GameData.shopList[(4 * data.corridorState)+slot];
		s = GameData.catalystSpriteList[g.Id];
		name.text = g.Desc; 
		grade.text = g.Name;
		grade.color = GetColor(g.Name);
		str.text = "Upgrade";
		agi.text = "success rate";
		vit.text = "+" + g.SuccessRate.ToString();
		price.text = g.Price.ToString(); 
		priceTypeSprite.sprite = goldSprite;
		spriteRenderer.sprite = s;
	}
	
	Color GetColor(string tipe){
		Color ret = new Color();
//		Debug.Log ("UPDET COLOR " + tipe);

		switch (tipe.Trim()) {
		case "Common" : ret = Color.white;break;//white
		case "Uncommon" : ret = new Color(0,1,1,1);break; //biru muda
		case "Rare" : ret = defaultColor;break; // 
		case "Mythical" : ret = new Color(1,1,0);break;
		case "Legendary" : ret = Color.green;break;//new Color(19,255,0);break;		
		}

		return ret;
	}
}

using UnityEngine;
using System.Collections;

public class WeaponSlotInformation : MonoBehaviour {

	// Use this for initialization
	public int slot;
	public SpriteRenderer spriteRenderer;
	public TextMesh name;
	public TextMesh str;
	public TextMesh agi;
	public TextMesh vit; 

	void Start () {
		if (GameData.weaponSlotContentList.Count > slot) {
			Gem gem = GameData.weaponSlotContentList [slot];
			name.text = gem.Name;
			Sprite sprite = (Sprite)Resources.Load ("Sprites/Gem/" + name, typeof(Sprite));
			spriteRenderer.sprite = sprite;
			str.text = gem.Stats.Str.ToString();
			agi.text = gem.Stats.Agi.ToString();
			vit.text = gem.Stats.Vit.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

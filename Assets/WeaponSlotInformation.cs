using UnityEngine;
using System.Collections;

public class WeaponSlotInformation : MonoBehaviour {

	// Use this for initialization
	public int slot;
	public SpriteRenderer spriteRenderer;


	void Start () {
		if (GameData.weaponSlotContentList.Count > slot) {
			Gem gem = GameData.weaponSlotContentList [slot];
			Sprite sprite = (Sprite)Resources.Load ("Sprites/Gem/" + name, typeof(Sprite));
			spriteRenderer.sprite = sprite;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

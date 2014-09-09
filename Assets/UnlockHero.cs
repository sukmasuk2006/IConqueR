using UnityEngine;
using System.Collections;

public class UnlockHero : MonoBehaviour {

	public int slot;
	public GameObject frame;
	public Sprite sprite;
	public SpriteRenderer renderer;
	public UnlockCostController costController;

	void OnMouseDown(){
		if (GameData.gold >= GameData.unitList[slot].GoldNeeded && !GameData.unitList [slot].IsUnlocked) {
						GameData.unitList [slot].IsUnlocked = true;
						frame.SetActive (false);
						GameData.unlockedHeroes++;
						costController.SendMessage ("UpdateCost");
						this.gameObject.SetActive(false);
				} 
		}
}

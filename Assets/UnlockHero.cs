using UnityEngine;
using System.Collections;

public class UnlockHero : MonoBehaviour {

	public int slot;
	public GameObject frame;
	public Sprite sprite;
	public SpriteRenderer renderer;
	public UnlockCostController costController;

	void OnMouseDown(){
		if (GameData.gold >= GameData.unlockHeroCost && !GameData.unitList [slot].IsUnlocked) {
						GameData.unitList [slot].IsUnlocked = true;
						frame.SetActive (false);
						GameData.unlockedHeroes++;
						GameData.unlockHeroCost = GameData.unlockedHeroes * GameConstant.BASE_PRICE * 2;
						costController.SendMessage ("UpdateCost");
						renderer.enabled = false;
				} 
		}
}

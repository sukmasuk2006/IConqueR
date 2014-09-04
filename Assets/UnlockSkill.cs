using UnityEngine;
using System.Collections;

public class UnlockSkill : MonoBehaviour {

	public int slot;
	public GameObject frame;
	public Sprite sprite;
	public SpriteRenderer renderer;
	public UnlockCostController costController;

	void OnMouseDown(){
		if (GameData.gold >= GameData.unlockSkillCost && !GameData.skillList [slot].IsUnlocked) {
						GameData.skillList [slot].IsUnlocked = true;
						frame.SetActive (false);
						GameData.unlockSkillCost = GameData.unlockSkillCost * GameConstant.BASE_PRICE * 2;
						costController.SendMessage ("UpdateCost");
						renderer.enabled = false;
				} 
		}
}

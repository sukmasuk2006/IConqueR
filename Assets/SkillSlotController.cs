using UnityEngine;
using System.Collections;

public class SkillSlotController : MonoBehaviour {

	public GameObject skillLockedFrame;
	public SpriteRenderer skillButton;
	public int skillSlot;
	private bool skillState = false;
	public Sprite unlockedSprite;

	// Use this for initialization
	void Start () {
		if (GameData.skillList [skillSlot].IsUnlocked) {
			skillButton.enabled = false;
			skillState = true;
			skillLockedFrame.SetActive(false);
		}
	}
}

using UnityEngine;
using System.Collections;

public class HeroSlotController : MonoBehaviour {

	public GameObject heroLockedFrame;
	public SpriteRenderer heroButton;
	public int heroSlot;
	private bool heroState = false;
	public Sprite unlockedSprite;

	// Use this for initialization
	void Start () {
		if (GameData.unitList [heroSlot].IsUnlocked) {
			heroButton.enabled = false;
			heroState = true;
			heroLockedFrame.SetActive(false);
		}
	}
}

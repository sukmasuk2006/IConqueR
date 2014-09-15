using UnityEngine;
using System.Collections;

public class HeroSlotController : MonoBehaviour {

	public GameObject heroLockedFrame;
	public SpriteRenderer heroButton;
	public int heroSlot;
	private bool heroState = false;
	public Sprite unlockedSprite;
	public TextMesh lvlText;
	public TextMesh nameText;
	public TextMesh descText;
	public TextMesh goldText;

	// Use this for initialization
	void Start () {
		lvlText.text = GameData.unitList [heroSlot].Level.ToString();
		nameText.text = GameData.unitList [heroSlot].Name;
		descText.text = GameData.unitList [heroSlot].Description;
		goldText.text = GameData.unitList [heroSlot].GoldNeeded.ToString();

		if (GameData.unitList [heroSlot].IsUnlocked) {
			goldText.gameObject.SetActive(false);
			heroButton.enabled = false;
			heroState = true;
			heroLockedFrame.SetActive(false);
		}
	}
}

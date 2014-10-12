using UnityEngine;
using System.Collections;

public class SkillSlotController : MonoBehaviour {

	public GameObject skillLockedFrame;
	public SpriteRenderer skillButton;
	public int skillSlot;
	private bool skillState = false;
	public Sprite unlockedSprite;
	public TextMesh priceText;

	// Use this for initialization
	void Start () {
		Skill s = GameData.profile.skillList [skillSlot];
		if (s.IsUnlocked) {
						skillLockedFrame.SetActive (false);
						skillButton.sprite = unlockedSprite;
				} else if (!s.IsUnlocked) {
				priceText.text =  s.Price.ToString();

		}
	}
}

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
		priceText.text = s.Price.ToString();
		if (s.IsUnlocked) {
			skillState = true;
			skillLockedFrame.SetActive(false);

		}
	}
}

using UnityEngine;
using System.Collections;

public class HeroSlotController : MonoBehaviour {

	public GameObject heroLockedFrame;
	public SpriteRenderer heroButton;
	public int heroSlot;
	private bool heroState = false;
	public Sprite unlockedSprite;
	public TextMesh nameText;
	public TextMesh jobText;
	public TextMesh lvlText;
	public TextMesh str;
	public TextMesh agi;
	public TextMesh vit;
	public TextMesh goldText;

	// Use this for initialization
	void Start () {
		UpdateData ();
	}

	public void UpdateData(){
		nameText.text = "Name " + GameData.unitList [heroSlot].Name;
		jobText.text = GameData.unitList [heroSlot].Job;
		lvlText.text = "Level " + GameData.unitList [heroSlot].Level.ToString();
		str.text = "Str :"+GameData.unitList [heroSlot].Str.ToString();
		agi.text = "Agi :"+GameData.unitList [heroSlot].Agi.ToString();
		vit.text = "Vit :"+GameData.unitList [heroSlot].Vit.ToString();
		goldText.text = GameData.unitList [heroSlot].GoldNeeded.ToString();
		
		if (GameData.unitList [heroSlot].IsUnlocked) {
			goldText.gameObject.SetActive(false);
			heroState = true;
			heroLockedFrame.SetActive(false);
		}
	}
}

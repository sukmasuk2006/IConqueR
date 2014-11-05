using UnityEngine;
using System.Collections;

public class HeroSlotController : MonoBehaviour {

	public GameObject heroLockedFrame;
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
		nameText.text = GameData.profile.unitList [heroSlot].Name;
		jobText.text = GameData.profile.unitList [heroSlot].Job;
		lvlText.text = "Level " + GameData.profile.unitList [heroSlot].Level.ToString();
		str.text = "Str  "+GameData.profile.unitList [heroSlot].Str.ToString();
		agi.text = "Agi  "+GameData.profile.unitList [heroSlot].Agi.ToString();
		vit.text = "Vit  "+GameData.profile.unitList [heroSlot].Vit.ToString();
		goldText.text = GameData.profile.unitList [heroSlot].GoldNeeded.ToString();
		
		if (GameData.profile.unitList [heroSlot].IsUnlocked) {
			goldText.gameObject.SetActive(false);
			heroState = true;
			heroLockedFrame.SetActive(false);
		}
	}
}

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
	public GameObject tweenedObject;
	public GameObject targetObject;
	public HeroProfileController controller; // screen troops
	public GameObject assignButton;

	// Use this for initialization
	void Start () {
		UpdateData ();
	}

	public void UpdateData(){
	//	if ( GameData.gameState == "UnitShowcase" )
		//	assignButton.SetActive(false);
	//	else 
	//		assignButton.SetActive(true);
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

	void OnMouseDown(){
		if (GameData.readyToTween && GameData.gameState == "UnitShowcase" ) {
			if (GameData.profile.unitList [heroSlot].IsUnlocked){
				//				Debug.Log("masuk 2 " + GameData.profile.formationList [slot].IsUnlocked + " " + GameData.profile.formationList [slot].Unit.HeroId);
				// view profile controller set gambar dan status		
				GameData.selectedToViewProfileId = GameData.profile.unitList[heroSlot].HeroId;
				GameData.selectedToViewProfileName = GameData.profile.unitList[heroSlot].Name;
				GameData.gameState = "HeroProfileScene";
				iTween.MoveTo ( tweenedObject,iTween.Hash("position",targetObject.transform.position,"time", 0.1f,"oncomplete","MoveTarget","oncompletetarget",gameObject));
				iTween.MoveTo (targetObject, tweenedObject.transform.position,0.1f);					
				controller.SetPictureAndStatsFromFormation();
			}
		}
	}
}

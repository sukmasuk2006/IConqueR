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
	public SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		ReloadData();
	}

	void OnMouseDown(){
		Debug.Log("klik di hero unlock hero " + heroSlot + " "  + GameData.profile.unitList [heroSlot].IsUnlocked);

		if (GameData.readyToTween && GameData.gameState == "UnitShowcase" ) {
			if (GameData.profile.unitList [heroSlot].IsUnlocked){
				//				Debug.Log("masuk 2 " + GameData.profile.formationList [slot].IsUnlocked + " " + GameData.profile.formationList [slot].Unit.HeroId);
				// view profile controller set gambar dan status		
				GameData.selectedToViewProfileId = heroSlot;
				GameData.selectedToViewProfileName = GameData.profile.unitList[heroSlot].Name;
				GameData.gameState = "HeroProfileScene";
				iTween.MoveTo ( tweenedObject,iTween.Hash("position",targetObject.transform.position,"time", 0.1f,"oncomplete","MoveTarget","oncompletetarget",gameObject));
				iTween.MoveTo (targetObject, tweenedObject.transform.position,0.1f);					
				controller.SetPictureAndStatsFromFormation();
			}
		}
	}

	public void ReloadData(){
		Unit u = GameData.profile.unitList[heroSlot];
		nameText.text = u.Name;
		jobText.text = u.JobList[u.CurrentJob];
		lvlText.text = "Level " + u.Level.ToString();
		str.text = "Str  "+u.Str.ToString();
		agi.text = "Agi  "+u.Agi.ToString();
		vit.text = "Vit  "+u.Vit.ToString();
		goldText.text = u.GoldNeeded.ToString();
		
		if (u.IsUnlocked) {
			goldText.gameObject.SetActive(false);
			heroState = true;
			heroLockedFrame.SetActive(false);
		}
	}
}

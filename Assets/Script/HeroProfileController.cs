using UnityEngine;
using System.Collections;

public class HeroProfileController : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	public TextMesh healthText;
	public TextMesh strText;
	public TextMesh vitText;
	public TextMesh agiText;
	public TextMesh movText;
	public TextMesh atkText;
	public TextMesh defText;
	public TextMesh evaText;
	public TextMesh atkSpdText;
	public TextMesh critText;
	public GameObject expBar;
	public GameObject upgradeTroopButton;
	

	// Use this for initialization
	void Start () {
		SetPictureAndStats();

	}

	public void SetPictureAndStats(){
		Sprite sprite = (Sprite)Resources.Load ("Sprite/Character/Hero/"+GameData.selectedToViewProfileName, typeof(Sprite));
		spriteRenderer.sprite = sprite;	
		healthText.text = GameData.unitList [GameData.selectedToViewProfileId].HealthPoint.ToString();
		strText.text = GameData.unitList [GameData.selectedToViewProfileId].Str.ToString();
		vitText.text = GameData.unitList [GameData.selectedToViewProfileId].Vit.ToString();
		agiText.text = GameData.unitList [GameData.selectedToViewProfileId].Agi.ToString();
		movText.text = GameData.unitList [GameData.selectedToViewProfileId].Movement.ToString();
		atkText.text = GameData.unitList [GameData.selectedToViewProfileId].AttackPoint.ToString();
		defText.text = GameData.unitList [GameData.selectedToViewProfileId].DefensePoint.ToString();
		evaText.text = GameData.unitList [GameData.selectedToViewProfileId].EvasionRate.ToString();
		atkSpdText.text = GameData.unitList [GameData.selectedToViewProfileId].AttackSpeed.ToString();
		critText.text = GameData.unitList [GameData.selectedToViewProfileId].Critical.ToString();
	}

	public void SetPictureAndStatsFromFormation(){
		spriteRenderer.sprite = GameData.formationList [GameData.selectedToViewProfileId].Unit.Sprites;	
		healthText.text = GameData.formationList [GameData.selectedToViewProfileId].Unit.HealthPoint.ToString();
		strText.text = GameData.formationList [GameData.selectedToViewProfileId].Unit.Str.ToString();
		vitText.text = GameData.formationList [GameData.selectedToViewProfileId].Unit.Vit.ToString();
		agiText.text = GameData.formationList [GameData.selectedToViewProfileId].Unit.Agi.ToString();
		movText.text = GameData.formationList [GameData.selectedToViewProfileId].Unit.Movement.ToString();
		atkText.text = GameData.formationList [GameData.selectedToViewProfileId].Unit.AttackPoint.ToString();
		defText.text = GameData.formationList [GameData.selectedToViewProfileId].Unit.DefensePoint.ToString();
		evaText.text = GameData.formationList [GameData.selectedToViewProfileId].Unit.EvasionRate.ToString();
		atkSpdText.text = GameData.formationList [GameData.selectedToViewProfileId].Unit.AttackSpeed.ToString();
		critText.text = GameData.formationList [GameData.selectedToViewProfileId].Unit.Critical.ToString();
	}

	// Update is called once per frame
	void Update () {
	
	}
}

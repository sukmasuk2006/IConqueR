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
		Debug.Log ("start at heroprof");
		if (GameData.selectedToViewProfileId == 4)
			upgradeTroopButton.SetActive (false);
		else
			expBar.SetActive(false);
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

	public void SetPictureAndStats(){
		Debug.Log ("start at heroprof");
		if (GameData.selectedToViewProfileId == 4)
			upgradeTroopButton.SetActive (false);
		else
			expBar.SetActive(false);
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

	// Update is called once per frame
	void Update () {
	
	}
}

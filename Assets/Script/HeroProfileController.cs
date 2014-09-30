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

	public TextMesh name;
	public TextMesh lvl;
	public TextMesh job;
	

	// Use this for initialization
	void Start () {
	//	SetPictureAndStats();
		SetPictureAndStatsFromFormation ();
	}
	
	public void SetPictureAndStatsFromFormation(){
		Unit u = GameData.formationList [GameData.selectedToViewProfileId].Unit;
		name.text = GameData.selectedToViewProfileName;
		job.text = u.Job;
		lvl.text = u.Level.ToString();
		healthText.text = u.HealthPoint.ToString();
		spriteRenderer.sprite = u.Sprites;	
		healthText.text = u.HealthPoint.ToString();
		strText.text = u.Str.ToString() +" + "+ u.Weapon.WeaponStats.Str;
		vitText.text = u.Vit.ToString()+" + "+ u.Weapon.WeaponStats.Vit;
		agiText.text = u.Agi.ToString()+" + "+ u.Weapon.WeaponStats.Agi;
		movText.text = u.Movement.ToString();
		atkText.text = u.AttackPoint.ToString();
		defText.text = u.DefensePoint.ToString();
		evaText.text = u.EvasionRate.ToString();
		atkSpdText.text = u.AttackSpeed.ToString();
		critText.text = u.Critical.ToString();
	}

	// Update is called once per frame
	void Update () {
	
	}
}

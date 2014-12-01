using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SetHeroOnFormation : MonoBehaviour {
	
	public List<FormationSetter> listForm;
	public List<GameObject> listDismissButton;
	public TextMesh infoText;
	public GameObject tweenedObject;
	public GameObject targetObject;
	private Vector3 tempPosition;
	public int slot;
	public AudioClip sound;

	// Use this for initialization
	void Start () {
	//	infoText.text = "Select your units!";
		}
	

	void OnMouseDown(){
		tempPosition = targetObject.transform.position;
		// SET FORMATION		
				//copy status, dan id biar gampang nanti itung2an expnya setelah battle
//				Debug.Log("slot formasi yang akan di set di barackscreen " + screenData.formationSlot);
		// SELECT UNIT
		// ALGORITHM:
		// jika unit ke slot tidak aktif, dan state select unit dan sudah terunlock 
		Unit u = GameData.profile.unitList[slot];
		if ( !u.IsActive && GameData.gameState == "SelectUnit" 
		    && u.IsUnlocked) {
					
					// cek apakah di formationSlot ke unitSlotYangDiSet ada hero yang aktif
					int currentActiveUnitId = GameData.profile.formationList[GameData.unitSlotYangDiSet]
					.UnitHeroId;
					Debug.Log(" hero id " + currentActiveUnitId + " diset false " ) ;

					// kalau gak 99 brarti masih ada heronya
					if ( currentActiveUnitId != 99 ) {// inisialisasi awal pas buka slot id =99;
					GameData.profile.unitList[currentActiveUnitId].IsActive = false; // non aktifkan unit yang aktif
					GameData.profile.formationList[GameData.unitSlotYangDiSet].UnitHeroId = 99; // jadikan formationslot idnya 99, dianggap kosong dulu
					GameData.profile.activeHeroes--;
			}
			GameData.profile.formationList [GameData.unitSlotYangDiSet].
			SetUnit (slot,GameData.profile.unitList [slot]);  // isi slot dengan unit dan idnya

			u.IsActive = true; // aktifkan unit
			GameData.profile.activeHeroes++;
			listForm [GameData.unitSlotYangDiSet].ReloadSprite (u.JobList[u.CurrentJob]);
					listDismissButton[GameData.unitSlotYangDiSet].SetActive(true);
					infoText.text = "Select Unit";

				//	Debug.Log("Cek FS");
				///	foreach ( FormationUnit j in GameData.profile.formationList ){
				//		Debug.Log(" u " + j.UnitHeroId);
				//	}
			GameData.SaveData();
			if (GameData.readyToTween ) {
					GameData.readyToTween = false;
					GameData.gameState = "Home";
					iTween.MoveTo ( targetObject,iTween.Hash("position",tweenedObject.transform.position,"time", 0.1f,"oncomplete","ReadyTween","onCompleteTarget",gameObject));
				}
		}
		else{
			if ( !u.IsUnlocked )
				infoText.text = "Unlock first!";
			else{
				if ( GameData.gameState == "SelectUnit" )
					infoText.text = "This unit already assigned";
			}
		}
		StartCoroutine(alp());

		GameData.SaveData ();
	}

	public IEnumerator alp(){
		yield return new WaitForSeconds(1.5f);
		infoText.text = "Select Unit";
		
	}

	void ReadyTween(){
		GameData.readyToTween = true;
		iTween.MoveTo (tweenedObject, tempPosition,0.3f);		
//		Debug.Log ("Oncomplete");	
		MusicManager.getMusicEmitter().audio.PlayOneShot(sound);
//		this.gameObject.SetActive(false);
	}
}

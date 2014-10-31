using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class tutorialController : MonoBehaviour {

	public int batasAtasTutorial;
	public TextMesh text;
	public GameObject tutorialObject;
	public List<int> hiddenState; // state dimana dia hidden
	public List<GameObject> listTutorialObject;
	public string activeState = "Home";
	public bool textChange = false;
	// Use this for initialization
	void Start () {
		CheckTutorialState ();
	}

	void CheckTutorialState(){
		CheckVisible ();
		SetText ();
		GameData.SaveData ();
	}

	void OnMouseUp(){
		GameData.profile.TutorialState++;
		CheckTutorialState ();
	}

	void SetText(){
		try{
			Debug.Log("name " + name +" state chat " + GameData.profile.TutorialChatState); 
			var teks = GameData.tutorialTextList.Where( x => x.Id == GameData.profile.TutorialState).First();
			text.text = teks.Teks;
		}
		catch{
			
		}
	}

	void CheckVisible(){
		var tres = hiddenState.Where (x => x >= GameData.profile.TutorialState).Select ( x => x ).ToList();
		Debug.Log("name " + name +" state sekarang " + GameData.profile.TutorialState + " batas " + tres[0]); 
		if (GameData.profile.TutorialState > batasAtasTutorial || GameData.profile.TutorialState == tres[0] )
			tutorialObject.SetActive (false);
		for (int i =0 ; i < listTutorialObject.Count ; i++ ){
			try {
				int info = listTutorialObject[i].GetComponent<TutorialObjectInfo>().activeState.
						Where( x => x >= GameData.profile.TutorialState ).Select( x => x ).ToList()[0];
				Debug.Log("object ke " + i + " aktif saat " + info);
				if ( GameData.profile.TutorialState == info ){
					listTutorialObject[i].SetActive(true);
					Debug.Log("object ke " + i + " aktif !");
				}
				else if ( GameData.profile.TutorialState > info ){
					listTutorialObject[i].SetActive(false);
					Debug.Log("object ke " + i + " deaktif !");
				}
			}
			catch{
			
			}
		}
	}
}

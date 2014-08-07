using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour {

	/*
	 * Gameplay
	 */
	private bool isWin = true;

	/*
     * Report
	 */
	public GameObject reportScreen;
	public Vector3 reportTargetPosition;	
	public TextMesh goldTextMesh;

	// Use this for initialization
	void Start () {
		GameData.gameState = GameConstant.GAMEPLAY_SCENE;
	}
	
	// Update is called once per frame
	void Update () {
		CheckWin ();
	}

	void CheckWin(){
		if (isWin) {
			GameData.gold += 100;
			goldTextMesh.text = GameData.gold.ToString();
			iTween.MoveTo (reportScreen, iTween.Hash ("position", reportTargetPosition, "time", 1.0f));
			isWin = false;
			if ( GameData.raidTime <= 0 ){
				GameData.raidTime = 10f; 
				GameData.raidTimePos.x = GameData.INITIAL_RAID_TIME_POS;
			}
		}
	}
}

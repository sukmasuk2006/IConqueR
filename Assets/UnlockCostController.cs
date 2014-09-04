using UnityEngine;
using System.Collections;

public class UnlockCostController : MonoBehaviour {

	// Use this for initialization
	public TextMesh unlockCostTextSlotOne;
	public TextMesh unlockCostTextSlotTwo;
	public TextMesh unlockCostTextSlotThree;
	public TextMesh unlockCostTextSlotFour;

	void Start () {
		UpdateCost ();
	}
	

	void UpdateCost(){
		unlockCostTextSlotOne.text = unlockCostTextSlotTwo.text = 
		unlockCostTextSlotThree.text = unlockCostTextSlotFour.text = 
		GameData.unlockHeroCost.ToString ();
		Debug.Log ("cost updated");
	}
}

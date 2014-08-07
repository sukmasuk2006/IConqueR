using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour {

	private static GameMusic instance;
	public	 static GameMusic GetInstance() {
		return instance;
	}
	
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}

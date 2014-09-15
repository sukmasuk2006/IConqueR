using UnityEngine;
using System.Collections;

public class InputNameHandler : MonoBehaviour {

public TextMesh inputtedNameText;
	private TouchScreenKeyboard keyboard;
	public static bool isKeyboardOpen = false;
	public static bool isDone = false;
	// Use this for initialization
	void Start () {
		isDone = false;
		isKeyboardOpen = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (GameData.isFirstPlay) {
						if (!isKeyboardOpen) {
								keyboard = TouchScreenKeyboard.Open (inputtedNameText.text, TouchScreenKeyboardType.NamePhonePad);
								isKeyboardOpen = true;
						}

						if (isKeyboardOpen) {
								inputtedNameText.text = keyboard.text;
						}

						if (keyboard.done || keyboard.wasCanceled) {
								inputtedNameText.text = keyboard.text;

						}
						if (isDone) {
								GameData.name = inputtedNameText.text;
								PlayerPrefs.SetString ("name", GameData.name);
						}
						if (Input.GetKeyDown (KeyCode.Escape)) {

						}
				}
	}

	private void AddScore(int i){
		//GameController.AddScore(inputtedNameText.text,GameController.deathCount,i);
	}
}
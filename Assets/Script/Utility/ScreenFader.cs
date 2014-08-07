using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	public Texture2D blackScreen; // add a black texture here
	public float fadeTime; // how long you want it to fade?
	
	private bool fadeIn; // false for fade out
	private Color color = Color.black;
	private float timer;
	
	public void FadeIn()
	{
		timer = fadeTime;
		fadeIn = true;
	}
	
	public void FadeOut(string scene)
	{
		timer = fadeTime;
		fadeIn = false;
		StartCoroutine(alp(scene));
	}
	
	void Start()
	{
		FadeIn();
	}
	
	 void OnGUI()
	{
		if (fadeIn)
		{
			color.a = timer / fadeTime;
		}
		else
		{
			color.a = 1 - (timer / fadeTime);
		}
		
		GUI.color = color;
		GUI.DrawTexture (new Rect (0,0,Screen.width,Screen.height), blackScreen);
	}
	
	public IEnumerator alp(string scene){
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel (scene);
		
	}
	void Update()
	{
		timer -= Time.deltaTime;
		
		if (timer <= 0)
		{
			timer = 0;
		}
	}
}

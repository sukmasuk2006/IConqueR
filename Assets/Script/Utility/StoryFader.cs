using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryFader : MonoBehaviour {

	public Texture2D blackScreen; // add a black texture here
	public float fadeTime; // how long you want it to fade?
	public bool changeScene = true;

	private bool fadeIn; // false for fade out
	private Color color = Color.black;
	private float timer;
	private int storyState = 0;
	public List<Transform> storyList;

	
	public void FadeIn()
	{
		timer = fadeTime;
		fadeIn = true;
	}
	
	public void FadeOut()
	{
		timer = fadeTime;
		fadeIn = false;
		StartCoroutine(alp());
	}
	
	void Start()
	{
		storyState = 1;
		FadeIn();
		ChangeStory();
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
	
	public IEnumerator alp(){
		yield return new WaitForSeconds(fadeTime);
		if ( changeScene)
		{
			ChangeStory();
		}
		
	}

	void ChangeStory(){
		GameObject g = GameObject.FindGameObjectWithTag("Story");
		if( storyState < 4 ){
			Destroy(g);
			Instantiate(storyList[storyState]);
			storyState++;
		}
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

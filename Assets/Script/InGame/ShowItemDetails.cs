using UnityEngine;
using System.Collections.Generic;

public class ShowItemDetails : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	public TextMesh detailsText;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		List<string> lines = new List<string> ();
		TextAsset txt = (TextAsset)Resources.Load ("Text/Gems Description/Common Gems/"+ spriteRenderer.sprite.name, typeof(TextAsset));
		string content = txt.text;
		detailsText.text = content;
	}
}

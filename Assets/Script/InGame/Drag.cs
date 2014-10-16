using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BoxCollider2D))]

public class Drag : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector2 pos;
	private float minX,maxX,minY,maxY;

	void Start(){
		gameObject.transform.position = GameData.profile.MapPos;
		minX = 4.5f; // selama x lebih kecil dari minX
		maxX = -23.2f; // selama x lebih besar dari maxX
		minY = 16.8f; // selama y lebih kecil dari miny
		maxY = 0.3f; // selama y lebih besar dari maxy
	}
	
	void OnMouseDown() {
		if( GameData.gameState == "Map" )
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	
	void OnMouseDrag()
	{
		if (GameData.gameState == "Map" ) {
			pos = gameObject.transform.position;
			if (pos.x < minX && pos.x > maxX && pos.y > maxY && pos.y < minY) {
					Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
					Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
					transform.position = curPosition;
			}
		}
	}

	void OnMouseUp(){
		StayOnTrack ();
		GameData.profile.MapPos = gameObject.transform.position;
		GameData.SaveData ();
	}

	void StayOnTrack(){
		Debug.Log ("stay ontrack");
		float ofset = 0.1f;
		if ( pos.x >= minX )
			pos = new Vector2 (minX - ofset,pos.y);
		if ( pos.x <= maxX )
			pos = new Vector2(maxX + ofset,pos.y);
		if ( pos.y <= maxY )
			pos = new Vector2 (pos.x,maxY + ofset);
		if (  pos.y >= minY )
			pos = new Vector2( pos.x, minY - ofset);

		gameObject.transform.position = pos;
		/*if ( pos.x >= 5.3f )
			pos = new Vector2 (pos.x >= 5.3f ? 5.29f : pos.x,pos.y);
		pos = new Vector2(pos.x <= -24.5f ? -24.4f : pos.x,pos.y);
		pos = new Vector2(pos.x,pos.y <= -0.5f ? -0.51f : pos.y);
		pos = new Vector2(pos.x, pos.y >= 17.25f ? 17.24f : pos.y);*/
	}
}

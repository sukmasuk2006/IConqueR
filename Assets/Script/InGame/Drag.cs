using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BoxCollider2D))]

public class Drag : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector2 pos;
	private float minX,maxX,minY,maxY;

	private Vector2 leftFingerPos = Vector2.zero;
	private Vector2 leftLastPos = Vector2.zero;
	private Vector2 leftFingerMovedBy = Vector2.zero;

	public float slideMagnitudeX = 0.0f;
	public float slideMagnitudeY = 0.0f;
	private bool canMove = false;

	void Start(){
		Input.multiTouchEnabled = false;
		if ( GameData.profile.TutorialState > GameConstant.TOTAL_TUTORIAL )
			canMove = true;
		//if ( canMove ){
			gameObject.transform.position = GameData.profile.MapPos;
			minX = 4.5f; // selama x lebih kecil dari minX
			maxX = -23.2f; // selama x lebih besar dari maxX
			minY = 13.8f; // selama y lebih kecil dari miny
			maxY = 0.3f; // selama y lebih besar dari maxy
		//}
	}

	void OnMouseDown() {
		Debug.Log(" DRag down " + GameData.gameState + " " + GameData.isDrag );
		if( GameData.gameState == "Map" && canMove ){//&& GameData.profile.TutorialState > GameConstant.TOTAL_TUTORIAL && !GameData.isDrag){
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
			GameData.isDrag = true;
		}
	}
	
	void OnMouseDrag()
	{
	if (GameData.gameState == "Map" && canMove ){//&& GameData.profile.TutorialState > GameConstant.TOTAL_TUTORIAL && !GameData.isDrag) {
			pos = gameObject.transform.position;
			if (pos.x < minX && pos.x > maxX && pos.y > maxY && pos.y < minY) {
					Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
					Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
					transform.position = curPosition;
			}
		}
	}

	void OnMouseUp(){
		Debug.Log(" DRag up " + GameData.gameState + " " + GameData.isDrag  );
		if (GameData.gameState == "Map" && GameData.profile.TutorialState > GameConstant.TOTAL_TUTORIAL && canMove) {
			StayOnTrack ();
			GameData.profile.MapPos = gameObject.transform.position;
			GameData.isDrag = false;
		}

	}

	void Update(){
	//	dragcount.text = "Drag state " + dragstate;
	//	isdragtext.text = "isdrag " + GameData.isDrag;
		/*if( GameData.gameState == "Map"&& GameData.profile.TutorialState > GameConstant.TOTAL_TUTORIAL && canMove ){
			//		dragstate = 1;
				Touch touchZero = Input.GetTouch(0);

				if ( touchZero.phase == TouchPhase.Began ){
					offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(touchZero.position.x,touchZero.position.y, screenPoint.z));
				}
				else if ( touchZero.phase == TouchPhase.Moved  ){

					GameData.isDrag = true;
				pos = GameData.profile.MapPos;
						if (pos.x < minX && pos.x > maxX && pos.y > maxY && pos.y < minY) {
						Vector3 curScreenPoint = new Vector3 (touchZero.position.x, touchZero.position.y, screenPoint.z);
						Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
						transform.position = curPosition;
					}
				}

				else if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled)
				{
					GameData.isDrag = false;
					StayOnTrack ();
					GameData.profile.MapPos = gameObject.transform.position;
				}

		}*/
	}
	// biar gak keluar map
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

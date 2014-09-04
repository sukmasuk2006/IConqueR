using UnityEngine;
using System.Collections;

public class Item {

	protected string name;

	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}

	public Item(string name,float str,float agi,float intel,
	            float dex, float vit){
		this.name = name;
	//	status = new Status (str, agi, intel, dex, vit);
	}
}

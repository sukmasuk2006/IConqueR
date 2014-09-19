using UnityEngine;
using System.Collections;

public class Item {

	protected string name;
	protected string desc;
	protected Sprite sprites;
	private float successRate;
	private int price;

	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}

	public Item(string name){
		this.name = name.Trim();
	}



	public string Desc {
		get {
			return desc;
		}
		set {
			desc = value;
		}
	}


	public int Price {
		get {
			return price;
		}
		set {
			price = value;
		}
	}

	public Sprite Sprites {
		get {
			return sprites;
		}
		set {
			sprites = value;
		}
	}

	public float SuccessRate {
		get {
			return successRate;
		}
		set {
			successRate = value;
		}
	}
}

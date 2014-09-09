using UnityEngine;
using System.Collections;

public class Item {

	protected string name;
	protected string desc;
	private int price;
	protected UnitStatus stats;

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

	public UnitStatus Stats {
		get {
			return stats;
		}
		set {
			stats = value;
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
}

[System.Serializable]
public class Item {

	protected string name;
	protected string desc;
	private float successRate;
	private int price;
	private int priceType;
	protected int id;

	public int Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}

	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}

	public Item(int id,string name){
		this.id = id;
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



	public float SuccessRate {
		get {
			return successRate;
		}
		set {
			successRate = value;
		}
	}

	public int PriceType {
		get {
			return priceType;
		}
		set {
			priceType = value;
		}
	}
}

public class Status  {

	private float str;

	public float STR {
		get {
			return str;
		}
		set {
			str = value;
		}
	}

	private float agi;

	public float AGI {
		get {
			return agi;
		}
		set {
			agi = value;
		}
	}

	private float intelligence;

	public float INT {
		get {
			return intelligence;
		}
		set {
			intelligence = value;
		}
	}

	private float dex;

	public float DEX {
		get {
			return dex;
		}
		set {
			dex = value;
		}
	}

	private float vit;

	public float VIT {
		get {
			return vit;
		}
		set {
			vit = value;
		}
	}

	public Status(float str,float agi,float intel,float dex,float vit){
		this.str = str;
		this.agi = agi;
		this.intelligence = intel;
		this.dex = dex;
		this.vit = vit;
	}
}

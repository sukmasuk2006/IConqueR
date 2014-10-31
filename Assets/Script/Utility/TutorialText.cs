

public class TutorialText {
	// Use this for initialization
	private int id;
	private string teks;

	public TutorialText(int i,string t){
		id = i;
		teks = t;
	}

	public int Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}

	public string Teks {
		get {
			return teks;
		}
	}
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public class SkillEffect
{
	int tipe;
	float amount;

	public SkillEffect (int tipe,int amount)
	{
		this.tipe = tipe;
		this.amount = amount;
	}

	public void DoEffect(Unit u){
		
	}

	public int Tipe {
		get {
			return tipe;
		}
		set {
			tipe = value;
		}
	}

	public float Amount {
		get {
			return amount;
		}
		set {
			amount = value;
		}
	}
}


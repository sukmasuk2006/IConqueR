//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

public class FormationUnit
	{
	private Unit unit;
	private int unitSlot; // slot di unit list biar gampang bagi exp
	private bool isUnlocked = false;

				public FormationUnit (Unit u)
				{
					isUnlocked = false;
					this.unit = u;		
				}

	public void SetUnit(int slot, Unit u){
		unit.CopyStats(u);
		unitSlot = slot;
	}

	public Unit Unit {
		get {
			return unit;
		}
		set {
			unit = value;
		}
	}

	public int UnitSlot {
		get {
			return unitSlot;
		}
		set {
			unitSlot = value;
		}
	}

	public bool IsUnlocked {
		get {
			return isUnlocked;
		}
		set {
			isUnlocked = value;
		}
	}
	}

using UnityEngine;

public class FormationUnit
	{
	private Unit unit;
	//private int unitSlot; // slot di unit list biar gampang bagi exp
	private bool isUnlocked = false;

				public FormationUnit (Unit u)
				{
					isUnlocked = false;
					this.unit = u;		
				}

	//
	public void SetUnit(int id, Unit u){
		unit.CopyStats(id,u);

	}

	public Unit Unit {
		get {
			return unit;
		}
		set {
			unit = value;
		}
	}

	public void Save (int slot)
	{
		PlayerPrefs.SetInt ("formationSlot" + slot, isUnlocked ? 1 : 0);
		unit.Save ();
	}

	public void Load(int slot){
	    IsUnlocked = (PlayerPrefs.GetInt ("formationSlot" + slot)!= 0);
		Unit.Load();
	}

/*	public int UnitSlot {
		get {
			return unitSlot;
		}
		set {
			unitSlot = value;
		}
	}
*/
	public bool IsUnlocked {
		get {
			return isUnlocked;
		}
		set {
			isUnlocked = value;
		}
	}
	}


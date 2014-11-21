using UnityEngine;

public class FormationUnit
	{
	private Unit unit;
	//private int unitSlot; // slot di unit list biar gampang bagi exp
	private bool isUnlocked = false;
	private int unitHeroId;

	public FormationUnit (int id,Unit u)
	{
		isUnlocked = false;
		this.unit = u;		 // 99
		this.unitHeroId = id; // 99
	}

	//
	public void SetUnit(int id, Unit u){
		unit.CopyStats(u);
		unitHeroId = id;
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
		PlayerPrefs.SetInt ("formationSlotHeroId" + slot, unitHeroId);
//		unit.Save ();
	}

	public void Load(int slot){
//		Debug.Log("Load 1 " + slot );
		IsUnlocked = (PlayerPrefs.GetInt ("formationSlot" + slot)!= 0);
//		Debug.Log("Load 2 " + slot );
		unitHeroId = PlayerPrefs.GetInt	("formationSlotHeroId" + slot);
//		Debug.Log("Load 3 " + slot );
		SetUnit(unitHeroId,GameData.profile.unitList[unitHeroId == 99 ? 0 : unitHeroId]);
//		Debug.Log("Load 4 " + slot );
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

	public int UnitHeroId {
		get {
			return unitHeroId;
		}
		set {
			unitHeroId = value;
		}
	}
	}


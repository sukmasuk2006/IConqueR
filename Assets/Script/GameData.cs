using UnityEngine;
using System.Collections.Generic;

public class GameData : MonoBehaviour {

	public static float INITIAL_RAID_TIME_POS = 2.3f;
	public static float gameSpeed = 1f;

	public static bool isFirstPlay = true;
	public static string gameState = "LOGO";

	public static int gold = 0;
	public static int crystal = 0;
	public static float raidTime = 5;
	public static Vector3 raidTimePos = new Vector3 (2.3f, 5f, -0.5f);

	//public static 
	public static List<Item> inventoryList;
	public static List<string> heroesList;

	public static int activatedHeroes = 1;
	public static int availableHeroes = 1;
	public static int currentLevel = 1;
	public static int index = 1;

	// Use this for initialization
	void Start () {
		//LoadData ();
		/*unlockedHeroes = new List<bool> ();
		selectedHeroes = new List<bool> ();
		//basecampActiveHeroesList = new List<string> ();
		heroesList = new List<string> ();

		for (int i = 0; i < 10; i++) {
			unlockedHeroes.Add(true);		
		}
		selectedHeroes.Add(true);		
		selectedHeroes.Add(false);	
		selectedHeroes.Add(false);		
		selectedHeroes.Add(false);	
		selectedHeroes.Add(false);		
		selectedHeroes.Add(false);	
		selectedHeroes.Add(false);		
		selectedHeroes.Add(false);
		selectedHeroes.Add(false);	
		selectedHeroes.Add(false);	
		
		heroesList.Add ("knight");
		heroesList.Add ("warrior");
		heroesList.Add ("archer");
		heroesList.Add ("tribe");
		heroesList.Add ("thief");
		heroesList.Add ("monk");
		heroesList.Add ("assasin");
		heroesList.Add ("hunter");
		*/
	}

	void LoadData(){
		if (PlayerPrefs.HasKey ("gold")) {
			gold = PlayerPrefs.GetInt ("gold");
			Debug.Log("ada gold");
		} else {
			isFirstPlay = true;				
			Debug.Log("first play");
		}
		if (PlayerPrefs.HasKey ("crystal")) {
			crystal = PlayerPrefs.GetInt("crystal");	
		}

		/*
		inventoryList = new List<Item> ();
		inventoryList.Add (new Item ("items", 1, 1, 1, 1, 1));
		inventoryList.Add (new Item ("items", 1, 1, 1, 1, 1));
		inventoryList.Add (new Item ("items", 1, 1, 1, 1, 1));
		inventoryList.Add (new Item ("items", 1, 1, 1, 1, 1));
		inventoryList.Add (new Item ("items", 1, 1, 1, 1, 1));
	*/
	}

	public static void UpdateRaidTime(){
		raidTime -= (Time.deltaTime * gameSpeed);
	}
	
	// Update is called once per frame
	void Update () {

	}
}

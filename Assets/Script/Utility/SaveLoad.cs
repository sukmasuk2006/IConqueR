using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

	//it's static so we can call it from anywhere
	public static void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/t18.gd"); //you can call it anything you want
		bf.Serialize(file, GameData.profile);
		file.Close();
//		Debug.Log ("gamesaved!");
	}	
	
	public static void Load() {
		if (File.Exists (Application.persistentDataPath + "/t18.gd")) {	
						BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/t18.gd", FileMode.Open);
						GameData.profile = (ProfileData)bf.Deserialize (file);
						Debug.Log ("gameloaded!");
//						Debug.Log ("army defeated " + GameData.profile.DefeatedArmy);
						file.Close ();
			GameData.isFirstPlay = false;
				} else {
			GameData.isFirstPlay = true;
			Debug.Log("GK BISA DI LOAD");		
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using PluginUnityWP;

using System.Xml.Serialization;
using System.Xml;

public static class SaveLoad {


	private static string gamepath = Application.persistentDataPath + "/t20.gd";
	//it's static so we can call it from anywhere
	public static void Save() {
		// SaveController.SaveAndSerialize (SaveController.PrefEnum.GAMESTATE, GameData.profile);
//		Serializer.Save<ProfileData> (gamepath,GameData.profile);

		//Serializer.Save<ProfileData>(gamepath,GameData.profile);

		//	Build Winphone
		byte[] bytes = PluginUnityWP.Class1.SerializeObject<ProfileData> (GameData.profile);
		File.WriteAllBytes (gamepath, bytes);

		/*BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create(gamepath); //you can call it anything you want
		bf.Serialize(file, GameData.profile);
		file.Close();
//		Debug.Log ("gamesaved!");*/
	

	}	

	
	public static void Load() {
		//GameData.profile = SaveController.LoadAndDeserialize<ProfileData> (SaveController.PrefEnum.GAMESTATE);
			GameData.profile =  Serializer.Load<ProfileData> (gamepath);

/*			Build Winphone
		byte[] bytes = File.ReadAllBytes (gamepath);        
		GameData.profile = (ProfileData)PluginUnityWP.Class1.DeserializeObject<ProfileData>(bytes);
	//	*/
		/*
		if (File.Exists (gamepath)) {	
						BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (gamepath, FileMode.Open);
						GameData.profile = (ProfileData)bf.Deserialize (file);
						Debug.Log ("gameloaded!");
//						Debug.Log ("army defeated " + GameData.profile.DefeatedArmy);
						file.Close ();
			GameData.isFirstPlay = false;
				} else {
			GameData.isFirstPlay = true;
			Debug.Log("GK BISA DI LOAD");		
		}*/
	}

}

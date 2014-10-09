using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using System.Xml;
using System.Xml.Serialization;

public class Serializer
{
	public static T Load<T>(string filename) where T: class
	{
		if (File.Exists(filename))
		{
			try
			{
				using (Stream stream = File.OpenRead(filename))
				{
					
					BinaryFormatter formatter = new BinaryFormatter();
					return formatter.Deserialize(stream) as T;
				}
			}
			catch (Exception e)
			{
				Debug.Log(e.Message);
			}
		}
		return default(T);
	}
	
	public static void Save<T>(string filename, T data) where T: class
	{
		Debug.Log("game saved 1");
	using (Stream stream = File.OpenWrite(filename))
		{   
			Debug.Log ("gamesaved2");
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, data);
		}
	}

}
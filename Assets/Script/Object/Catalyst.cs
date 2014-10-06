//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Catalyst : Item
{
		
		public Catalyst (int id,string name) : base (id,name)
		{
			
		InitializeCatalyst ();
	}
	
	private void InitializeCatalyst(){
		string[] linesFromFile = null;
		TextAsset txt = (TextAsset)Resources.Load ("Data/Catalyst/"+ name.Trim(), typeof(TextAsset));
		//Debug.Log ("added catalyst " + name);
		string content = txt.text;
		linesFromFile = content.Split ("\n" [0]);
		desc = linesFromFile [0];
		PriceType = 0;
		Price = int.Parse(linesFromFile[1]);
		SuccessRate = float.Parse (linesFromFile [2]);
	}


}

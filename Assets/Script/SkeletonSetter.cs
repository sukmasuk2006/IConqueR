using UnityEngine;
using System.Collections;

public class SkeletonSetter : MonoBehaviour {

	public SkeletonAnimation animator;
	public SkeletonDataAsset knightSkeleton;
	SkeletonDataAsset playerData;
	AtlasAsset atlasdata;
	string name = "skeleton";

	void Start(){

				atlasdata = ScriptableObject.CreateInstance<AtlasAsset> ();
				playerData = ScriptableObject.CreateInstance<SkeletonDataAsset> ();
				playerData.fromAnimation = new string[0];
				playerData.toAnimation = new string[0];
				playerData.duration = new float[0];
				playerData.scale = 0.01f;
				playerData.defaultMix = 0.15f;
	
	
				atlasdata.atlasFile = (TextAsset)Resources.Load ("Sprite/knight/"+name + ".atlas", typeof(TextAsset));
				Material[] materials = new Material[1];
				materials [0] = (Material)Resources.Load("Sprite/knight/skeletonmaterial",typeof(Material));
				Texture aa = (Texture)Resources.Load ("Sprite/knight/"+name, typeof(Texture2D));
				materials [0].mainTexture = aa;
	
				atlasdata.materials = materials;
	
				playerData.atlasAsset = atlasdata;
				playerData.skeletonJSON = (TextAsset)Resources.Load ("Sprite/knight/"+name +".json", typeof(TextAsset));
	
				
				//animator.skeletonDataAsset = playerData;

				animator.skeletonDataAsset = knightSkeleton;
				animator.calculateNormals = true;
	
				animator.loop = true;
		}
}

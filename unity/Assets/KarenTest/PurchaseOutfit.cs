using UnityEngine;
using System.Collections;

public class PurchaseOutfit : MonoBehaviour {
	
	
	public static SkinnedMeshRenderer characterSkin;
	public static Material[] materials;
	// Materials[0] = Eyes
	// Materials[1] = Face
	// Materials[2] = Hair
	// Materials[3] = Pants
	// Materials[4] = Shoes
	// Materials[5] = Tops
	
	
	static void PurchaseSelectedOutfit () {
		
		GameObject characterObj = null;
		if (characterObj == null) {
			characterObj = GameObject.Find("female");
		} else {
			characterObj = GameObject.Find("male");
		}
		
		if (characterObj != null) {
			characterSkin = characterObj.GetComponent<SkinnedMeshRenderer>();
			materials = characterSkin.materials;
		}
		
		
		
		Debug.Log(characterSkin.materials.Length);
		Debug.Log(characterSkin.materials[0].name);
		Debug.Log(characterSkin.materials[1].name);
		Debug.Log(characterSkin.materials[2].name);
		Debug.Log(characterSkin.materials[3].name);
		Debug.Log(characterSkin.materials[4].name);
		Debug.Log(characterSkin.materials[5].name);
	}
	
		
	void OnGUI () {
		if(GUI.Button(new Rect(Screen.width/3,Screen.height/3,80,20), "TEst")) {
			PurchaseSelectedOutfit();
		};
	}
}

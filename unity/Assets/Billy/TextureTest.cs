using UnityEngine;
using System.Collections;

public class TextureTest : MonoBehaviour {
	
	public SkinnedMeshRenderer characterSkin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject characterObj = null;
		if (characterObj == null) {
			characterObj = GameObject.Find("female");
		} else {
			characterObj = GameObject.Find("male");
		}
		
		if (characterObj != null)
			characterSkin = characterObj.GetComponent<SkinnedMeshRenderer>();
	

		
	}
	
	void OnGUI () {
		if(GUI.Button(new Rect(Screen.width/3,Screen.height/3,80,20), "Set Material")) {
			Debug.Log(characterSkin.materials[0].name);
			characterSkin.materials[0] = (Material)Resources.Load("female_eyes_green");
			Debug.Log("Clicked");
			Debug.Log((Material)Resources.Load("female_eyes_green.mat"));
		};
	}
}

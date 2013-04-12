using UnityEngine;
using System.Collections;

public class KarenTextureTest : MonoBehaviour {
	
	public SkinnedMeshRenderer characterSkin;
	public Material material1;

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
	
}

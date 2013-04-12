using UnityEngine;
using System.Collections;

public class CloneCharacter : MonoBehaviour {
	
	private GameObject character = null;
	private GameObject newCharacter = null;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Clone() {
		character = GameObject.Find("male");
		if (character == null) {
			character = GameObject.Find("female");
		} 
		
		newCharacter =(GameObject) Instantiate(character, new Vector3 (4,-0.8f,22), character.transform.rotation);
		newCharacter.AddComponent<CloneWalk>();	
		
		Debug.Log(newCharacter.animation);
		//newCharacter.animation.p
		
        newCharacter.animation.Play("walk");
        newCharacter.animation["walk"].wrapMode = WrapMode.Loop;
	}
	
	void OnGUI () {
		if (GUI.Button(new Rect (Screen.width - 80, 0, 80, 40), "Walk")){
			Camera mainCamera = (Camera)GameObject.Find("Main Camera").GetComponent<Camera>();
			mainCamera.enabled=false;
			
			Camera walkCamera = (Camera)GameObject.Find("Camera").GetComponent<Camera>();
			walkCamera.enabled = true;
			
			Clone();
		}
	}
}

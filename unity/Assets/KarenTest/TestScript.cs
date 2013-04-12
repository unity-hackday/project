using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {
	public bool enabled = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		if (enabled) {
			Debug.Log("Enabled");
			GUI.Box(new Rect(200,200,100,90), "Loader Menu");
		} else {
			
			Debug.Log("Disabled");
		}
	}
}

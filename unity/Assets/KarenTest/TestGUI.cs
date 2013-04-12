using UnityEngine;
using System.Collections;

public class TestGUI : MonoBehaviour {

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Loader Menu");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Button 1")) {
			GameObject testObj = GameObject.Find("GameObject2");
			TestScript testScript =  testObj.GetComponent<TestScript>();
			testScript.enabled = false;
			
		}

		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Button 2")) {
		}
	}
}
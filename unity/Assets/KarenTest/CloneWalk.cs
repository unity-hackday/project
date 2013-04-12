using UnityEngine;
using System.Collections;

public class CloneWalk : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate()
	{
	    // Move the character a bit each frame.
	    transform.position += transform.forward * .02f;
	}
}

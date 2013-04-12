using UnityEngine;
using System.Collections;

public class Unlock : MonoBehaviour {
	
	public bool UnlockEnabled = false;
	
	string priceOfTop = null;
	string priceOfPants = null;
	string priceOfShoes = null;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {	
	
	}

		
	void OnGUI()
	{	
		if (UnlockEnabled)
		{
			int spacing = 150;
			int anchorX = Screen.width/2-((20*4+spacing*3)/2);
			int anchorY = 30;

			GUI.Box(new Rect(0,0,Screen.width,90), "Select product(s) for unlocking:");
			
			if(GUI.Button(new Rect(anchorX,anchorY,120,40), "Unlock Body $1"))
			{
				UnlockEnabled = false;
				UnlockConfirm checkout = GameObject.Find("UnlockConfirm").GetComponent<UnlockConfirm>();
				checkout.packNumber = 1;
			}
			if(GUI.Button(new Rect(anchorX+spacing,anchorY,120,40), "Unlock Legs $1"))
			{
				UnlockEnabled = false;
				UnlockConfirm checkout = GameObject.Find("UnlockConfirm").GetComponent<UnlockConfirm>();
				checkout.packNumber = 2;
			}
			if(GUI.Button(new Rect(anchorX+spacing*2,anchorY,120,40), "Unlock Shoes $1"))
			{
				UnlockEnabled = false;
				UnlockConfirm checkout = GameObject.Find("UnlockConfirm").GetComponent<UnlockConfirm>();
				checkout.packNumber = 3;
			}
			
			if(GUI.Button(new Rect(Screen.width-90,Screen.height-70,80,60), "Cancel"))
			{
				UnlockEnabled = false;
				Main back = GameObject.Find("GameObject").GetComponent<Main>();
				back.stage = 1;
			}
		}
	}
}

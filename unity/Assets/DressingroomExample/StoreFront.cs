using UnityEngine;
using System.Collections;

public class StoreFront : MonoBehaviour {
	
	public bool StoreFrontEnabled = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {	
	
	}
		void OnGUI()
	{
		if (StoreFrontEnabled)
		{
			int spacing = 120;
			int anchorX = Screen.width/2-((20*4+spacing*3)/2);
			int anchorY = 40;

			GUI.Box(new Rect(0,0,Screen.width,90), "Select product(s) for purchasing:");
			
			if(GUI.Button(new Rect(anchorX,anchorY,60,40), "Top"))
			{
				StoreFrontEnabled = false;
				OrderConfirm checkout = GameObject.Find("OrderConfirm").GetComponent<OrderConfirm>();
				checkout.packNumber = 1;
			}
			if(GUI.Button(new Rect(anchorX+spacing,anchorY,60,40), "Pant"))
			{
				StoreFrontEnabled = false;
				OrderConfirm checkout = GameObject.Find("OrderConfirm").GetComponent<OrderConfirm>();
				checkout.packNumber = 2;
			}
			if(GUI.Button(new Rect(anchorX+spacing*2,anchorY,60,40), "Shoes"))
			{
				StoreFrontEnabled = false;
				OrderConfirm checkout = GameObject.Find("OrderConfirm").GetComponent<OrderConfirm>();
				checkout.packNumber = 3;
			}
			if(GUI.Button(new Rect(anchorX+spacing*3,anchorY,60,40), "All"))
			{
				StoreFrontEnabled = false;
				OrderConfirm checkout = GameObject.Find("OrderConfirm").GetComponent<OrderConfirm>();
				checkout.packNumber = 4;
			}
			
			if(GUI.Button(new Rect(Screen.width-80,Screen.height-80,80,80), "Cancel"))
			{
				StoreFrontEnabled = false;
				Main back = GameObject.Find("GameObject").GetComponent<Main>();
				back.stage = 1;
			}
		}
		
	}
}

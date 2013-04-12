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
			GUI.Box(new Rect(0,0,Screen.width,Screen.height/4+50), "Storefront");
			
			if(GUI.Button(new Rect(290,30,80,80), "Top"))
			{
				StoreFrontEnabled = false;
				OrderConfirm checkout = GameObject.Find("OrderConfirm").GetComponent<OrderConfirm>();
				checkout.packNumber = 1;
			}
			if(GUI.Button(new Rect(400,30,80,80), "Pant"))
			{
				StoreFrontEnabled = false;
				OrderConfirm checkout = GameObject.Find("OrderConfirm").GetComponent<OrderConfirm>();
				checkout.packNumber = 2;
			}
			if(GUI.Button(new Rect(510,30,80,80), "Shoes"))
			{
				StoreFrontEnabled = false;
				OrderConfirm checkout = GameObject.Find("OrderConfirm").GetComponent<OrderConfirm>();
				checkout.packNumber = 3;
			}
			if(GUI.Button(new Rect(620,30,80,80), "All"))
			{
				StoreFrontEnabled = false;
				OrderConfirm checkout = GameObject.Find("OrderConfirm").GetComponent<OrderConfirm>();
				checkout.packNumber = 3;
			}
			
			if(GUI.Button(new Rect(Screen.width-80,Screen.height-80,80,80), "Exit Store"))
			{
				StoreFrontEnabled = false;
				Main back = GameObject.Find("GameObject").GetComponent<Main>();
				back.stage = 1;
			}
		}
		
	}
}
